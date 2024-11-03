using UnityEngine;

[CreateAssetMenu(fileName = "SceneBGMData", menuName = "ScriptableObjects/SceneBGMData", order = 1)]
public class SceneBGMData : ScriptableObject
{
    [System.Serializable]
    public struct SceneBGM
    {
        public string sceneName;
        public AudioClip bgm;
    }

    public SceneBGM[] sceneBGMs;
}
