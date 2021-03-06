﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Doug.Effects;

namespace Doug.Items
{
    public class EquipmentItem : Item
    {

        [NotMapped]
        public EquipmentEffect Effect { get; set; }

        public string EffectId { get; set; }
        public EquipmentSlot Slot { get; set; }

        public int LuckRequirement { get; set; }
        public int AgilityRequirement { get; set; }
        public int StrengthRequirement { get; set; }
        public int IntelligenceRequirement { get; set; }
        public int ConstitutionRequirement { get; set; }
        public int LevelRequirement { get; set; }

        public int Health { get; set; }
        public int Energy { get; set; }
        public int MaxAttack { get; set; }
        public int MinAttack { get; set; }
        public int Defense { get; set; }
        public int Dodge { get; set; }
        public int Hitrate { get; set; }
        public int AttackSpeed { get; set; }
        public int CooldownReduction { get; set; }
        public int Pierce { get; set; }
        public int Resistance { get; set; }
        public int Luck { get; set; }
        public int Agility { get; set; }
        public int Strength { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int HealthRegen { get; set; }
        public int FlatHealthRegen { get; set; }
        public int EnergyRegen { get; set; }
        public int FlatEnergyRegen { get; set; }

        public int LuckFactor { get; set; }
        public int AgilityFactor { get; set; }
        public int StrengthFactor { get; set; }
        public int ConstitutionFactor { get; set; }
        public int IntelligenceFactor { get; set; }
        public int HealthFactor { get; set; }
        public int EnergyFactor { get; set; }
        public int DefenseFactor { get; set; }
        public int HitRateFactor { get; set; }
        public int DodgeFactor { get; set; }
        public int PierceFactor { get; set; }
        public int AttackSpeedFactor { get; set; }
        public int CriticalHitChanceFactor { get; set; }
        public int CriticalDamageFactor { get; set; }

        public EquipmentItem()
        {
            MaxStack = 1;
            Rarity = 0;
        }

        public override bool IsEquipable()
        {
            return true;
        }

        public void CreateEffect(IEquipmentEffectFactory equipmentEffectFactory)
        {
            Effect = equipmentEffectFactory.CreateEffect(EffectId);
        }

        public virtual IEnumerable<string> GetDisplayAttributeList()
        {
            var attributes = new List<string>
            {
                string.Format(DougMessages.ItemLevel, LevelRequirement),
                DisplayRequirement(DougMessages.Luck, LuckRequirement),
                DisplayRequirement(DougMessages.Agi, AgilityRequirement),
                DisplayRequirement(DougMessages.Str, StrengthRequirement),
                DisplayRequirement(DougMessages.Int, IntelligenceRequirement),
                DisplayRequirement(DougMessages.Con, ConstitutionRequirement),
            };
            var requirements = attributes.Where(attr => !string.IsNullOrEmpty(attr)).ToList();
            return requirements.Concat(ToStringList());
        }

        public IEnumerable<string> ToStringList()
        {
            var attributes = new List<string>
            {
                MaxAttack == 0 ? string.Empty : string.Format(DougMessages.ItemAttack, MinAttack, MaxAttack),
                DisplayAttribute(DougMessages.ItemDefense, Defense),
                DisplayAttribute(DougMessages.ItemResistance, Resistance),
                Math.Abs(AttackSpeed) <= 1 ? string.Empty : string.Format(DougMessages.AttackSpeed, AttackSpeed),
                DisplayAttribute(DougMessages.HealthRegen, HealthRegen),
                DisplayAttribute(DougMessages.EnergyRegen, EnergyRegen),
                DisplayAttribute(DougMessages.HealthFactor, HealthFactor),
                DisplayAttribute(DougMessages.EnergyFactor, EnergyFactor),
                DisplayAttribute(DougMessages.DefenseFactor, DefenseFactor),
                DisplayAttribute(DougMessages.CriticalHitChanceFactor, CriticalHitChanceFactor),
                DisplayAttribute(DougMessages.CriticalDamageFactor, CriticalDamageFactor),
                DisplayAttribute(DougMessages.ItemHitrate, Hitrate),
                DisplayAttribute(DougMessages.ItemDodge, Dodge),
                DisplayAttribute(DougMessages.ItemHealth, Health),
                DisplayAttribute(DougMessages.ItemEnergy, Energy),
                DisplayAttribute(DougMessages.ItemStrength, Strength),
                DisplayAttribute(DougMessages.ItemAgility, Agility),
                DisplayAttribute(DougMessages.ItemIntelligence, Intelligence),
                DisplayAttribute(DougMessages.ItemConstitution, Constitution),
                DisplayAttribute(DougMessages.ItemLuck, Luck)
            };

            return attributes.Where(attr => !string.IsNullOrEmpty(attr));
        }

        private string DisplayAttribute(string text, int attribute)
        {
            return attribute == 0 ? string.Empty : string.Format(text, attribute);
        }

        private string DisplayRequirement(string text, int requirement)
        {
            return requirement == 0 ? string.Empty : $"*{requirement} {text}*";
        }

        public bool IsHandSlot()
        {
            return Slot == EquipmentSlot.LeftHand || Slot == EquipmentSlot.RightHand;
        }
    }
}
