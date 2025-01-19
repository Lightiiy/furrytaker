using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSpawnDatabase", menuName = "Game Data/Scene Spawn Database")]
public class SceneLoadingZoneDatabase : ScriptableObject
{
    [System.Serializable]
    public class SceneSpawnData
    {
        public string sceneName;
        public Vector2[] entryPoints;
    }

    public List<SceneSpawnData> sceneSpawnDataList;
}