using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneTrigger : MonoBehaviour
{
    public string nextSceneTitle;

    public int indexOfEntryPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Triggered by: {other.gameObject.name} at frame {Time.frameCount} , Trigger Object: {gameObject.name}, Index: {indexOfEntryPoint}");

            other.gameObject.SetActive(false);
            StartCoroutine(loadInToNextScene());
        }
    }

    private IEnumerator loadInToNextScene()
    {
        UIController.Instance.fadeToBlack();
        
        LevelManager.Instance.SetPlayerEntryPosition(nextSceneTitle, indexOfEntryPoint);
        
        while (UIController.Instance.fadeIn)
        {
            yield return null;
        }
        
        
        SceneManager.LoadScene(nextSceneTitle);
    }
}
