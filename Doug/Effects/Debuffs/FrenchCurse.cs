﻿using Doug.Models;
using Doug.Models.User;
using Doug.Services;
using Doug.Slack;

namespace Doug.Effects.Debuffs
{
    public class FrenchCurse : Buff
    {
        public const string EffectId = "french_curse";

        private readonly ISlackWebApi _slack;
        private readonly IUserService _userService;

        public FrenchCurse(ISlackWebApi slack, IUserService userService)
        {
            _slack = slack;
            _userService = userService;
            Id = EffectId;
            Name = "French Blood";
            Description = "You have French blood, vive la république!";
            Rank = Rank.Common;
            Icon = ":fr:";
        }

        public override string OnGettingFlamed(Command command, string slur)
        {
            _slack.BroadcastMessage(string.Format(DougMessages.Surrendered, _userService.Mention(new User { Id = command.GetTargetUserId()})), command.ChannelId);

            return base.OnGettingFlamed(command, slur);
        }
    }
}
