using System.Collections.Generic;

namespace Core.Scripts.Achievements
{
    public static class AchievementConfig
    {
        public static readonly Dictionary<Achievement, List<string>> AchievementInfo = new ()
        {
            {Achievement.TrashSearcher, new () { "Искатель", "Старательно изучить мусорный бак"} },
            {Achievement.HoldYourHorses, new () { "Придержи коней!", "Попытаться открыть дверь раньше времени"} },
            {Achievement.ElectroWizard, new () { "Электро-маг", "Почти убить себя розеткой"} },
            {Achievement.SunHater, new () { "Ненавистник Солнца", "Закрыть все шторки на окнах в коридоре, пока из окон ещё идёт свет"} },
            {Achievement.Inadequate, new () { "Н Е А Д Е К В А Т Е Н", "Попытаться убить деда огнетушителем"} },
            {Achievement.HideAndSeek, new () { "Игрок в прятки", "Найти монетку под сиденьем контролёра"} },
            {Achievement.AreYouScared, new () { "Испугался?", "Пережить скример"} },
            {Achievement.SlowGuy, new () { "Спешить некуда", "Потратить на прохождение игры более 20-и минут"} },
            {Achievement.TheFastest, new () { "Бегун", "Потратить на прохождение игры менее 5-и минут"} },
        };
    }
}
