using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
    public int bossHealthMax;
    public float damageVurnabilityPeriod = 5;
    public int bossHealthCurrent;
    public bool isInvurnable = false;
    
    private float invurnabilityCounter;
    // Start is called before the first frame update
    void Start()
    {
        bossHealthCurrent = bossHealthMax;
        invurnabilityCounter = damageVurnabilityPeriod;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvurnable)
        {
            invurnabilityCounter -= Time.deltaTime;

            isInvurnable = invurnabilityCounter < 0 ? false : true;
        }
        
        
        if (bossHealthCurrent <= 0)
        {
            killBoss();
        }
    }

    public void dealDamageToBoss(int damage)
    {
        if (!isInvurnable)
        {
            bossHealthCurrent -= damage;
            isInvurnable = true;
            invurnabilityCounter = damageVurnabilityPeriod;
        }
    }

    private void killBoss()
    {
        //create death effect via animator
        gameObject.SetActive(false);
    }
}
