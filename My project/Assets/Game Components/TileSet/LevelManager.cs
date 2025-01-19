using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public struct SceneSpawnData
{
    public string sceneName;
    public Vector2[] entryPoints;
}


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public float respawnTimer;

    public SceneLoadingZoneDatabase sceneLoadingZoneDataList;
    
    private Dictionary<string, Vector2[]> sceneSpawnDataDictionary;
    
    public Vector2 playerEntryPosition;

    public int PlayerScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSceneData();
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSceneData()
    {
        sceneSpawnDataDictionary = new Dictionary<string, Vector2[]>();

        foreach (var sceneData in sceneLoadingZoneDataList.sceneSpawnDataList)
        {
            if (!sceneSpawnDataDictionary.ContainsKey((sceneData.sceneName)))
            {
                sceneSpawnDataDictionary.Add(sceneData.sceneName, sceneData.entryPoints);
            }
            else
            {
                Debug.LogWarning($"Duplicate scene name detected: {sceneData.sceneName}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        AudioManager.Instance.PlaySFX(8);
        yield return new WaitForSeconds(respawnTimer - (1f/UIController.Instance.fadeSpeed));

        UIController.Instance.fadeToBlack();
        yield return new WaitForSeconds((1f/UIController.Instance.fadeSpeed) + 0.2f);
        UIController.Instance.fadeBackToScene();

        
        PlayerController.Instance.gameObject.SetActive(true);
        PlayerController.Instance.transform.position = CheckpointEntityController.Instance.playerSpawnPoint;

        PlayerHealthController.Instance.currentHealth = PlayerHealthController.Instance.maxHealth;
        UIController.Instance.UpdateHealthDisplay();
    }


    public void LevelEnd()
    {
        Debug.Log("im ending");
    }
    
    public Vector2 GetEntryPoint(string sceneName, int entryIndex)
    {
        if (sceneSpawnDataDictionary.TryGetValue(sceneName, out Vector2[] entryPoints))
        {
            Debug.Log(entryPoints.Length + " " + entryIndex);
            if (entryIndex >= 0 && entryIndex < entryPoints.Length)
            {
                return entryPoints[entryIndex];
            }
            else
            {
                Debug.LogError($"Entry index {entryIndex} out of range for scene {sceneName}");
            }
        }
        else
        {
            Debug.LogError($"Scene {sceneName} not found in SceneData.");
        }

        return Vector2.zero;
    }

    public void SetPlayerEntryPosition(string sceneName, int entryIndex)
    {
        playerEntryPosition = GetEntryPoint(sceneName,entryIndex);
    }
}
