﻿using Doug.Models;
using Doug.Repositories;
using Doug.Slack;
using Hangfire;
using System;
using Doug.Items;
using Doug.Services;

namespace Doug.Commands
{
    public interface ICasinoCommands
    {
        DougResponse Gamble(Command command);
        DougResponse GambleChallenge(Command command);
    }

    public class CasinoCommands : ICasinoCommands
    {
        private const string AcceptChallengeWord = "accept";
        private const string DeclineChallengeWord = "decline";
        private readonly IUserRepository _userRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly ISlackWebApi _slack;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IItemEventDispatcher _itemEventDispatcher;
        private readonly IRandomService _randomService;


        private static readonly DougResponse NoResponse = new DougResponse();
        private readonly IStatsRepository _statsRepository;

        public CasinoCommands(IUserRepository userRepository, ISlackWebApi messageSender, IChannelRepository channelRepository, IBackgroundJobClient backgroundJobClient, IItemEventDispatcher itemEventDispatcher, IStatsRepository statsRepository, IRandomService randomService)
        {
            _userRepository = userRepository;
            _slack = messageSender;
            _channelRepository = channelRepository;
            _backgroundJobClient = backgroundJobClient;
            _itemEventDispatcher = itemEventDispatcher;
            _statsRepository = statsRepository;
            _randomService = randomService;
        }

        public DougResponse Gamble(Command command)
        {
            var user = _userRepository.GetUser(command.UserId);
            var amount = int.Parse(command.GetArgumentAt(0));

            if (amount < 0)
            {
                return new DougResponse(DougMessages.InvalidAmount);
            }

            if (!user.HasEnoughCreditsForAmount(amount))
            {
                return new DougResponse(user.NotEnoughCreditsForAmountResponse(amount));
            }

            var cost = (int)Math.Ceiling(amount / 10.0);
            var energy = user.Energy - cost;

            if (energy < 0)
            {
                return new DougResponse(DougMessages.NotEnoughEnergy);
            }

            _statsRepository.UpdateEnergy(command.UserId, energy);

            string baseMessage;
            if (UserCoinFlipWin(user))
            {
                baseMessage = DougMessages.WonGamble;
                _userRepository.AddCredits(command.UserId, amount);
            }
            else
            {
                baseMessage = DougMessages.LostGamble;
                _userRepository.RemoveCredits(command.UserId, amount);
            }

            var message = string.Format(baseMessage, Utils.UserMention(command.UserId), amount);
            _slack.BroadcastMessage(message, command.ChannelId);

            return NoResponse;
        }

        private bool UserCoinFlipWin(User user)
        {
            var userChance = _itemEventDispatcher.OnGambling(user, user.BaseGambleChance());
            return _randomService.RollAgainstOpponent(userChance, 0.5);
        }

        public DougResponse GambleChallenge(Command command)
        {
            if (command.IsUserArgument())
            {
                return SendChallenge(command);
            }

            if (!IsUserChallenged(command.UserId))
            {
                return new DougResponse(DougMessages.NotChallenged);
            }

            if (command.GetArgumentAt(0).ToLower() == AcceptChallengeWord)
            {
                GambleVersus(command);
            }

            if (command.GetArgumentAt(0).ToLower() == DeclineChallengeWord)
            {
                var challenge = _channelRepository.GetGambleChallenge(command.UserId);
                _slack.BroadcastMessage(string.Format(DougMessages.GambleDeclined, Utils.UserMention(command.UserId), Utils.UserMention(challenge.RequesterId)), command.ChannelId);
                _channelRepository.RemoveGambleChallenge(challenge.TargetId);
            }

            return NoResponse;
        }

        private DougResponse SendChallenge(Command command)
        {
            var amount = int.Parse(command.GetArgumentAt(1));
            var targetId = command.GetTargetUserId();

            if (amount <= 0 || command.UserId == targetId)
            {
                return new DougResponse(DougMessages.YouIdiot);
            }

            var challenge = _channelRepository.GetGambleChallenge(targetId);

            if (challenge != null)
            {
                return new DougResponse(DougMessages.AlreadyChallenged);
            }

            _channelRepository.SendGambleChallenge(new GambleChallenge(command.UserId, targetId, amount));

            _backgroundJobClient.Schedule(() => ChallengeTimeout(targetId), TimeSpan.FromMinutes(3));

            _slack.BroadcastMessage(string.Format(DougMessages.ChallengeSent, Utils.UserMention(command.UserId), Utils.UserMention(targetId), amount), command.ChannelId);
            _slack.SendEphemeralMessage(DougMessages.GambleChallengeTip, targetId, command.ChannelId);

            return NoResponse;
        }

        public void ChallengeTimeout(string target)
        {
            _channelRepository.RemoveGambleChallenge(target);
        }

        private bool IsUserChallenged(string userId)
        {
            var challenge = _channelRepository.GetGambleChallenge(userId);
            return challenge != null;
        }

        private void GambleVersus(Command command)
        {
            var challenge = _channelRepository.GetGambleChallenge(command.UserId);
            var requester = _userRepository.GetUser(challenge.RequesterId);
            var target = _userRepository.GetUser(challenge.TargetId);

            if (!requester.HasEnoughCreditsForAmount(challenge.Amount))
            {
                _slack.BroadcastMessage(string.Format(DougMessages.InsufficientCredits, Utils.UserMention(requester.Id), challenge.Amount), command.ChannelId);
                return;
            }

            if (!target.HasEnoughCreditsForAmount(challenge.Amount))
            {
                _slack.BroadcastMessage(string.Format(DougMessages.InsufficientCredits, Utils.UserMention(target.Id), challenge.Amount), command.ChannelId);
                return;
            }

            var winner = target;
            var loser = requester;

            if (VersusCoinFlipWin(requester, target))
            {
                winner = requester;
                loser = target;
            }

            _userRepository.RemoveCredits(loser.Id, challenge.Amount);
            _userRepository.AddCredits(winner.Id, challenge.Amount);

            _channelRepository.RemoveGambleChallenge(challenge.TargetId);

            var message = string.Format(DougMessages.GambleChallenge, Utils.UserMention(winner.Id), challenge.Amount, Utils.UserMention(loser.Id));
            _slack.BroadcastMessage(message, command.ChannelId);
        }

        private bool VersusCoinFlipWin(User caller, User target)
        {
            var callerChance = _itemEventDispatcher.OnGambling(caller, caller.BaseGambleChance());
            var targetChance = _itemEventDispatcher.OnGambling(target, target.BaseGambleChance());

            return _randomService.RollAgainstOpponent(callerChance, targetChance);
        }
    }
}
