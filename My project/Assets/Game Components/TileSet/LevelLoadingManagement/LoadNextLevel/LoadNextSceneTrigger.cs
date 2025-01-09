using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneTrigger : MonoBehaviour
{
    public string nextSceneTitle;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(loadInToNextScene());
        }
    }

    private IEnumerator loadInToNextScene()
    {
        UIController.Instance.fadeToBlack();

        while (UIController.Instance.fadeIn)
        {
            yield return null;
        }
        
        SceneManager.LoadScene(nextSceneTitle);
    }
}
