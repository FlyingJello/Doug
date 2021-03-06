﻿using System.Collections.Generic;
using System.Linq;
using Doug.Menus.Blocks;
using Doug.Menus.Blocks.Accessories;
using Doug.Menus.Blocks.Text;
using Doug.Models;
using Doug.Models.User;

namespace Doug.Menus
{
    public class StatsMenu
    {
        public List<Block> Blocks { get; set; }

        public StatsMenu(User user, Party party)
        {
            Blocks = new List<Block>
            {
                new Section(new MarkdownText(string.Format(DougMessages.StatsOf, $"<@{user.Id}>"))),
                CreateUserOtherInfo(user),
                new Divider(),
            };

            if (party != null)
            {
                Blocks.Add(CreatePartyFields(party));
                Blocks.Add(new Divider());
            }

            if (user.Effects.Count > 0)
            {
                Blocks.Add(CreateEffectFields(user));
                Blocks.Add(new Divider());
            }

            Blocks.Add(CreateOtherStatsFields(user));
            Blocks.Add(new Divider());

            Blocks.AddRange(CreateStatsFields(user));
            Blocks.Add(new Divider());

            if (user.FreeStatsPoints > 0)
            {
                var freeStatsPoints = string.Format(DougMessages.FreeStatPoints, user.FreeStatsPoints.ToString());
                Blocks.Add(new Context(new List<string> { freeStatsPoints }));
            }
        }

        private Block CreateEffectFields(User user)
        {
            var fields = user.Effects.Select(ef => $"{ef.Effect.Icon} - {ef.Effect.Name} ({ef.GetDurationString()})").ToList();
            return new FieldsSection(fields);
        }

        private Block CreateUserOtherInfo(User user)
        {
            var userMiscInfo = new List<string>
            {
                string.Format(DougMessages.LevelStats, user.Level),
                string.Format(DougMessages.ExperienceStats, user.GetExperienceAdvancement() * 100),
                string.Format(DougMessages.CreditStats, user.Credits)
            };

            return new Context(userMiscInfo);
        }

        private List<Block> CreateStatsFields(User user)
        {
            var buttonDisplayed = user.FreeStatsPoints > 0;

            return new List<Block>
            {
                StatSection(DougMessages.StrengthStats, user.TotalStrength(), Stats.Strength, buttonDisplayed),
                StatSection(DougMessages.AgilityStats, user.TotalAgility(), Stats.Agility, buttonDisplayed),
                StatSection(DougMessages.ConstitutionStats, user.TotalConstitution(), Stats.Constitution, buttonDisplayed),
                StatSection(DougMessages.IntelligenceStats, user.TotalIntelligence(), Stats.Intelligence, buttonDisplayed),
                StatSection(DougMessages.LuckStats, user.TotalLuck(), Stats.Luck, buttonDisplayed)
            };
        }

        private Block StatSection(string message, int stat, string type, bool buttonDisplayed)
        {
            var textBlock = new MarkdownText(string.Format(message, stat));

            if (!buttonDisplayed)
            {
                return new Section(textBlock);
            }

            var buttonBlock = new Button(DougMessages.AddStatPoint, type, Actions.Attribution.ToString());
            return new Section(textBlock, buttonBlock);
        }

        private Block CreateOtherStatsFields(User user)
        {
            var statsFields = new List<string>
            {
                string.Format(DougMessages.HealthStats, $"*{user.Health}*/{user.TotalHealth()}"),
                string.Format(DougMessages.EnergyStats, $"*{user.Energy}*/{user.TotalEnergy()}"),
                string.Format(DougMessages.AttackStat, $"{user.MinAttack()}-{user.MaxAttack()}"),
                string.Format(DougMessages.DefenseStat, user.TotalDefense()),
                string.Format(DougMessages.DodgeStat, user.TotalDodge()),
                string.Format(DougMessages.GambleStat, user.BaseGambleChance()*100),
                string.Format(DougMessages.HitrateStat, user.TotalHitrate()),
                
            };

            return new FieldsSection(statsFields);
        }
        private Block CreatePartyFields(Party party)
        {
            var fields = party.Users.Select(usr => $"<@{usr.Id}>").ToList();
            fields.Insert(0, "In party with :");
            return new Context(fields);
        }
    }
}
