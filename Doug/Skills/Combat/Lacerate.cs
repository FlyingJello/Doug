﻿using System.Threading.Tasks;
using Doug.Effects;
using Doug.Items.WeaponType;
using Doug.Models;
using Doug.Models.Combat;
using Doug.Models.User;
using Doug.Repositories;
using Doug.Services;
using Doug.Slack;

namespace Doug.Skills.Combat
{
    public class Lacerate : CombatSkill
    {
        public const string SkillId = "lacerate";

        private readonly ISlackWebApi _slack;
        private readonly IUserService _userService;
        private readonly ICombatService _combatService;
        private readonly IEventDispatcher _eventDispatcher;

        public Lacerate(IStatsRepository statsRepository, ISlackWebApi slack, IUserService userService,
            ICombatService combatService, IEventDispatcher eventDispatcher, IChannelRepository channelRepository) : base(statsRepository, channelRepository, slack, eventDispatcher)
        {
            Id = SkillId;
            Name = "Lacerate";
            EnergyCost = 20;
            Cooldown = 40;
            RequiredWeapon = typeof(Claws);

            _slack = slack;
            _userService = userService;
            _combatService = combatService;
            _eventDispatcher = eventDispatcher;
        }

        public override async Task<DougResponse> Activate(User user, ICombatable target, string channel)
        {
            if (!CanActivateSkill(user, target, channel, out var response))
            {
                return response;
            }

            var message = string.Format(DougMessages.UserActivatedSkill, _userService.Mention(user), Name);
            await _slack.BroadcastMessage(message, channel);

            var damage = 5 * user.TotalAgility() + user.Level * 8;
            var attack = new PhysicalAttack(user, damage, int.MaxValue);
            target.ReceiveAttack(attack, _eventDispatcher);
            await _combatService.DealDamage(user, attack, target, channel);

            return new DougResponse();
        }
    }
}
