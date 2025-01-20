using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BonfireSaveProp : MonoBehaviour
{

    public GameObject savePrompt;
    
    public GameObject savedPrompt;

    public float waitTime;
    
    private bool isPlayerNearby = false;

    private bool gameSaved = false;

    private void Update()
    {
        if (savePrompt != null)
        {
            savePrompt.SetActive(isPlayerNearby && !gameSaved);
        }
        
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            SaveGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }

    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            gameSaved = false;
        }

    }

    private void SaveGame()
    {
        // Create the save data
        SaveData saveData = new SaveData
        {
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            playerPosition = PlayerController.Instance.transform.position,
            playerScore = LevelManager.Instance.PlayerScore
        };

        // Save the game using the SaveSystem
        SaveSystem.SaveGame(saveData);
        gameSaved = true;

        StartCoroutine(TextGameSavedAnimation());
    }

    private IEnumerator TextGameSavedAnimation()
    {
        savedPrompt.SetActive(true);

        TMP_Text textColor = savedPrompt.GetComponent<TMP_Text>();

        while (!(textColor.color.a <= 0f))
        {
            textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b,
                Mathf.MoveTowards(textColor.color.a, 0f, waitTime * Time.deltaTime));

            yield return null;
        }
        
        savedPrompt.SetActive(false);
    }
}
