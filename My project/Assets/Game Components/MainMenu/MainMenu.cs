using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public GameObject loadButton;
    
    // Start is called before the first frame update
    void Start()
    {
        if (loadButton != null)
        {
            loadButton.SetActive(SaveSystem.SaveFileExists());
        }
        else
        {
            Debug.LogWarning("Load Button hasn't been assigned in the inspector.");
        }
    }


    public void LoadGameFromSave()
    {
        SaveData saveData = SaveSystem.LoadGame();

        if (saveData != null)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            SceneManager.LoadScene(saveData.sceneName);
            
            void OnSceneLoaded(Scene scene, LoadSceneMode mode)
            {
                if (scene.name == saveData.sceneName)
                {
                    CoroutineRunner.instance.StartCoroutine(WaitForObjectsAndApplyData(saveData));
                    
                    SceneManager.sceneLoaded -= OnSceneLoaded;
                }
            }
        }

    }

    public void StartGame()
    {
        SaveData newSave = new SaveData
        {
            sceneName = newGameScene,
            playerPosition = new Vector3(0, 0, 0),
            playerScore = 0
        };
        
        SaveSystem.SaveGame(newSave);
        
        SceneManager.LoadScene(newGameScene);
    }

    public void QuitGame()
    {
        //Add "are you sure?" sequence
        //Fade out naimation
        Application.Quit();
    }

    private IEnumerator WaitForObjectsAndApplyData(SaveData saveData)
    {
        while (PlayerController.Instance == null || LevelManager.Instance == null)
        {
            yield return null;
        }
        
        PlayerController.Instance.transform.position = saveData.playerPosition;
        LevelManager.Instance.PlayerScore = saveData.playerScore;
    }
}
