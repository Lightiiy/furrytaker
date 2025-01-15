using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneTrigger : MonoBehaviour
{
    public string nextSceneTitle;
    public GameObject SpawnPlayerInLoadingZone;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
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
        PlayerController.Instance.transform.position = SpawnPlayerInLoadingZone.transform.position;
    }
}
