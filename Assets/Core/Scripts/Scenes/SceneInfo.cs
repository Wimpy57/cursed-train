using System.Collections.Generic;

namespace Core.Scripts.Scenes
{
    public static class SceneInfo
    {
        public static Dictionary<SceneName, string> SceneStringNameDictionary = new()
        {
            { SceneName.NewTrainScene, "NewTrainScene" },
            { SceneName.OldTrainScene, "OldTrainScene" },
        };
    }
}
