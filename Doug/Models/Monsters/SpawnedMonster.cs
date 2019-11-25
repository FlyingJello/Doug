﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Doug.Effects;
using Doug.Models.Combat;

namespace Doug.Models.Monsters
{
    public class SpawnedMonster : ICombatable
    {
        public int Id { get; set; }
        public string MonsterId { get; set; }
        public string Channel { get; set; }
        public int Health { get; set; }
        public DateTime AttackCooldown { get; set; }
        public List<MonsterAttacker> MonsterAttackers { get; set; }
        public Monster Monster { get; set; }

        public void LoadMonster()
        {
            Monster.Health = Health;
        }

        public bool IsAttackOnCooldown()
        {
            return DateTime.UtcNow <= AttackCooldown;
        }

        public Attack AttackTarget(ICombatable target, IEventDispatcher eventDispatcher)
        {
            Attack attack = new PhysicalAttack(this, Monster.MinAttack, Monster.MaxAttack, Monster.Hitrate, Monster.GetCriticalHitChance());

            if (Monster.DamageType == DamageType.Magical)
            {
                attack = new MagicAttack(this, Monster.MaxAttack);
            }

            return target.ReceiveAttack(attack, eventDispatcher);
        }

        public Attack ReceiveAttack(Attack attack, IEventDispatcher eventDispatcher)
        {
            if (attack is PhysicalAttack physicalAttack)
            {
                return ApplyPhysicalDamage(physicalAttack);
            }

            ApplyMagicalDamage(attack.Damage);
            return attack;
        }

        private PhysicalAttack ApplyPhysicalDamage(PhysicalAttack attack)
        {
            var missChance = (Monster.Dodge - attack.AttackersHitrate) * 0.01;
            if (new Random().NextDouble() < missChance)
            {
                attack.Status = AttackStatus.Missed;
                attack.Damage = 0;
                return attack;
            }

            var reducedDamage = attack.Damage - (int)Math.Ceiling(attack.Damage * Monster.Resistance * 0.01 + Monster.Defense);
            reducedDamage = reducedDamage <= 0 ? 1 : reducedDamage;

            attack.Damage = reducedDamage;
            if (attack.Status == AttackStatus.Critical)
            {
                attack.Damage *= 2;
            }

            Health -= attack.Damage;

            return attack;
        }

        private void ApplyMagicalDamage(int damage)
        {
            Health -= damage;
        }
    }
}
