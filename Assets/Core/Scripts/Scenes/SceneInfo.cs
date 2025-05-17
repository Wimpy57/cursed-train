using System.Collections.Generic;

namespace Core.Scripts.Scenes
{
    public static class SceneInfo
    {
        public static readonly Dictionary<SceneName, string> SceneStringNameDictionary = new()
        {
            { SceneName.NewTrainScene, "NewTrainScene" },
            { SceneName.DarkNewTrainScene, "DarkNewTrainScene" },
            { SceneName.OldTrainScene, "OldTrainScene" },
        };
    }
}
