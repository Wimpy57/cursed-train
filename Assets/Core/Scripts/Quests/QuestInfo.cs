using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Quests
{
    public static class QuestInfo
    {
        private const string MENU_STATE_QUEST = "";
        private const string COUPE_STATE_QUEST = "Направляйтесь в туалет";
        private const string DARK_NEW_TRAIN_QUEST = "Вернитесь в купе";
        private const string OLD_MAN_SPEECH_QUEST = "Выслушайте старика";
        private const string FIND_LEVER_QUEST = "Воспользуйтесь стоп-краном";
        private const string FIND_THE_KEY_QUEST = "Найдите ключ от купе";
        private const string OPEN_THE_DOOR_QUEST = "Откройте дверь в старое купе";
        private const string OLD_TRAIN_QUEST = "Найдите ребёнка";
        private const string CHILD_DEFENCE_QUEST = "Защищайте ребёнка";
        private const string FINAL_QUEST = "Всё нахуй";
        
        public static readonly Dictionary<States.State, string> QuestByState = new()
        {
            { States.State.Menu, MENU_STATE_QUEST},
            { States.State.CoupeState, COUPE_STATE_QUEST},
            { States.State.DarkNewTrain, DARK_NEW_TRAIN_QUEST },
            { States.State.OldManSpeech, OLD_MAN_SPEECH_QUEST },
            { States.State.FindLever, FIND_LEVER_QUEST },
            { States.State.OldTrain, OLD_TRAIN_QUEST },
            { States.State.FindTheKey, FIND_THE_KEY_QUEST },
            { States.State.OpenTheDoor, OPEN_THE_DOOR_QUEST },
            { States.State.ChildDefence, CHILD_DEFENCE_QUEST },
            { States.State.Final, FINAL_QUEST }
        };

        // public static readonly Dictionary<States.State, Image> QuestIconByState = new()
        // {
        //     { States.State.Menu, MENU_STATE_QUEST },
        //     { States.State.CoupeState, COUPE_STATE_QUEST },
        //     { States.State.DarkNewTrain, DARK_NEW_TRAIN_QUEST },
        //     { States.State.OldManSpeech, OLD_MAN_SPEECH_QUEST },
        //     { States.State.FindLever, FIND_LEVER_QUEST },
        //     { States.State.OldTrain, OLD_TRAIN_QUEST },
        //     { States.State.ChildDefence, CHILD_DEFENCE_QUEST },
        //     { States.State.Final, FINAL_QUEST }
        // };
    }
}
