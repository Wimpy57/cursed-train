using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.Achievements
{
    public static class AchievementConfig
    {
        public static readonly Dictionary<Achievement, List<string>> AchievementInfo = new ()
        {
            {Achievement.TrashSearcher, new () { "Искатель", "Старательно изучить мусорный бак"} },
        };
    }
}
