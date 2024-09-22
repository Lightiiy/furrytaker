using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public float respawnTimer;

    public int PlayerScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
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
}
