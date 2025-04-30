using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.Quests
{
    public class QuestInfo : MonoBehaviour
    {
        private const string COUPE_STATE_QUEST = "Направляйтесь в туалет";
        private const string DARK_NEW_TRAIN_QUEST = "Вернитесь в купе";
        private const string OLD_MAN_SPEECH_QUEST = "Выслушайте старика";
        private const string FIND_LEVER_QUEST = "Воспользуйтесь стоп-краном";
        private const string OLD_TRAIN_QUEST = "Найдите ребёнка";
        private const string CHILD_DEFENCE_QUEST = "Защищайте ребёнка";
        private const string FINAL_QUEST = "Всё нахуй";
        
        public static readonly Dictionary<States.State, string> QuestByState = new()
        {
            { States.State.CoupeState, COUPE_STATE_QUEST},
            { States.State.DarkNewTrain, DARK_NEW_TRAIN_QUEST },
            { States.State.OldManSpeech, OLD_MAN_SPEECH_QUEST },
            { States.State.FindLever, FIND_LEVER_QUEST },
            { States.State.OldTrain, OLD_TRAIN_QUEST },
            { States.State.ChildDefence, CHILD_DEFENCE_QUEST },
            { States.State.Final, FINAL_QUEST }
        };
    }
}
