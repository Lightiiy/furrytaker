using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Image heart1, heart2, heart3;

    public Sprite heartFull, heartEmpty;

    public GameObject GemCounter;
    public GameObject PauseScreen;
    public Image fadeOutScreen;
    public bool fadeIn, fadeOut;
    public bool isPaused = false;
    public float fadeSpeed;
    
    public string optionsMenu, mainMenu;

    private void Awake()
    {
        Instance = this;
        fadeBackToScene();
    }

    void Start()
    {
        UpdateGemCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            
            fadeOutScreen.color = new Color(fadeOutScreen.color.r, fadeOutScreen.color.g, fadeOutScreen.color.b,
                Mathf.MoveTowards(fadeOutScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeOutScreen.color.a >= 1f)
            {
                fadeIn = false;
            }
        }
        
        if (fadeOut)
        {
            fadeOutScreen.color = new Color(fadeOutScreen.color.r, fadeOutScreen.color.g, fadeOutScreen.color.b,
                Mathf.MoveTowards(fadeOutScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeOutScreen.color.a <= 0f)
            {
                fadeOut = false;
            }
            
        }

        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
        
    }

    public void UpdateHealthDisplay()
    {
        switch (PlayerHealthController.Instance.currentHealth)
        {
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;
            case 1:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }

    public void UpdateGemCounter()
    {
        GemCounter.GetComponentInChildren<TMP_Text>().text = LevelManager.Instance.PlayerScore.ToString();
    }

    public void togglePause()
    {
        isPaused = !isPaused;
        PauseScreen.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene(optionsMenu);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void fadeToBlack()
    {
        fadeIn = true;
        fadeOut = false;
    }
    
    public void fadeBackToScene()
    {
         fadeOut = true;
         fadeIn = false;
    }
}

