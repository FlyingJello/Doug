﻿using System;

namespace Doug
{ 
    public static class Utils
    {
        public static string UserMention(string userId)
        {
            return $"<@{userId}>";
        }

        public static bool IsInTimespan(DateTime currentTime, TimeSpan targetTime, int tolerance)
        {
            TimeSpan start = targetTime.Subtract(TimeSpan.FromMinutes(tolerance));
            TimeSpan end = targetTime.Add(TimeSpan.FromMinutes(tolerance));

            return (currentTime.TimeOfDay > start) && (currentTime.TimeOfDay < end);
        }
    }
}
