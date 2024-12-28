using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
    public int bossHealthMax;
    
    
    public int bossHealthCurrent;
    // Start is called before the first frame update
    void Start()
    {
        bossHealthCurrent = bossHealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealthCurrent <= 0)
        {
            killBoss();
        }
    }

    public void dealDamageToBoss(int damage)
    {
        Debug.Log("whoa im damaged");
        bossHealthCurrent -= damage;
    }

    private void killBoss()
    {
        //create death effect via animator
        gameObject.SetActive(false);
    }
}
