﻿using System;
using System.Collections.Generic;
using System.Linq;
using Doug.Items;
using Doug.Models.Coffee;
using Doug.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Doug.Repositories
{
    public interface ICoffeeRepository
    {
        CoffeeBreak GetCoffeeBreak();
        void AddToRoster(string userId);
        void RemoveFromRoster(string userId);
        void SkipUser(string userId);
        void ConfirmUserReady(string userId);
        ICollection<User> GetReadyParticipants();
        ICollection<User> GetMissingParticipants();
        void ResetRoster();
        void EndCoffeeBreak();
        void StartCoffeeBreak();
        string GetRemindJob();
        void SetRemindJob(string jobId);
    }

    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly DougContext _db;
        private readonly IEquipmentEffectFactory _equipmentEffectFactory;

        public CoffeeRepository(DougContext dougContext, IEquipmentEffectFactory equipmentEffectFactory)
        {
            _db = dougContext;
            _equipmentEffectFactory = equipmentEffectFactory;
        }

        public CoffeeBreak GetCoffeeBreak()
        {
            return _db.CoffeeBreak.Single();
        }

        public void AddToRoster(string userId)
        {
            if (!_db.Roster.Any(user => user.Id == userId))
            {
                _db.Roster.Add(new Roster() { Id = userId });
                _db.SaveChanges();
            }
        }

        public void ConfirmUserReady(string userId)
        {
            var user = _db.Roster.SingleOrDefault(usr => usr.Id == userId);
            if (user != null)
            {
                user.IsReady = true;
                _db.SaveChanges();
            }
        }

        public ICollection<User> GetMissingParticipants()
        {
            var userIds = _db.Roster.Where(user => !user.IsSkipping && !user.IsReady).Select(user => user.Id).ToList();

            var users = _db.Users.Where(usr => userIds.Contains(usr.Id))
                .Include(usr => usr.InventoryItems)
                .Include(usr => usr.Loadout)
                .ToList();

            users.ForEach(user => user.LoadItems(_equipmentEffectFactory));
            return users;
        }

        public ICollection<User> GetReadyParticipants()
        {
            var userIds = _db.Roster.Where(user => !user.IsSkipping && user.IsReady).Select(user => user.Id).ToList();

            var users = _db.Users.Where(usr => userIds.Contains(usr.Id))
                .Include(usr => usr.InventoryItems)
                .Include(usr => usr.Loadout)
                .ToList();

            users.ForEach(user => user.LoadItems(_equipmentEffectFactory));
            return users;
        }

        public void RemoveFromRoster(string userId)
        {
            var user = _db.Roster.SingleOrDefault(usr => usr.Id == userId);
            if (user != null)
            {
                _db.Roster.Remove(user);
                _db.SaveChanges();
            }
        }

        public void ResetRoster()
        {
            var participants = _db.Roster.ToList();
            participants.ForEach(participant =>
            {
                participant.IsReady = false;
                participant.IsSkipping = false;
            });
            _db.SaveChanges();
        }

        public void EndCoffeeBreak()
        {
            var channel = _db.CoffeeBreak.Single();
            channel.IsCoffeeBreak = false;

            _db.SaveChanges();
        }

        public void StartCoffeeBreak()
        {
            var channel = _db.CoffeeBreak.Single();
            var coffee = _db.CoffeeBreak.Single();
            channel.IsCoffeeBreak = true;
            coffee.LastCoffee = DateTime.UtcNow;

            _db.SaveChanges();
        }

        public string GetRemindJob()
        {
            return _db.CoffeeBreak.Single().CoffeeRemindJobId;
        }

        public void SetRemindJob(string jobId)
        {
            _db.CoffeeBreak.Single().CoffeeRemindJobId = jobId;
            _db.SaveChanges();
        }

        public void SkipUser(string userId)
        {
            var user = _db.Roster.SingleOrDefault(usr => usr.Id == userId);
            if (user != null)
            {
                user.IsSkipping = true;
                _db.SaveChanges();
            }
        }
    }
}
