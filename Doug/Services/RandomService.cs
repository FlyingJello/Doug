﻿using System;
using System.Collections.Generic;
using System.Linq;
using Doug.Models;
using Doug.Models.User;

namespace Doug.Services
{
    public interface IRandomService
    {
        bool RollAgainstOpponent(double userChances, double opponentChances);
        LootItem RandomFromWeightedTable(DropTable table);
        IEnumerable<LootItem> RandomTableDrop(DropTable table, double modifier);
        User DrawLotteryWinner(List<User> users);
    }

    public class RandomService : IRandomService
    {
        public bool RollAgainstOpponent(double userChances, double opponentChances)
        {
            var total = userChances + opponentChances;
            var rollResult = new Random().NextDouble() * total;

            if (rollResult < 0)
            {
                return false;
            }

            return rollResult < userChances;
        }

        public LootItem RandomFromWeightedTable(DropTable table)
        {
            var roll = new Random().NextDouble();
            var sum = 0.0;

            foreach (var item in table.Items)
            {
                sum += item.Probability;
                if (sum >= roll)
                {
                    return item;
                }
            }

            return table.Items.Last();
        }

        public IEnumerable<LootItem> RandomTableDrop(DropTable table, double modifier)
        {
            var random = new Random();
            return table.Items.Where(elem => random.NextDouble() < elem.Probability + modifier);
        }

        public User DrawLotteryWinner(List<User> users)
        {
            var totalTickets = users.Sum(user => user.LotteryTickets);
            var result = new Random().Next(0, totalTickets);

            var total = 0;

            foreach (var user in users)
            {
                total += user.LotteryTickets;
                if (result < total)
                {
                    return user;
                }
            }

            return users.Last();
        }
    }
}
