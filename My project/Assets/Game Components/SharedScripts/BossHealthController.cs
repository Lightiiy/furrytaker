using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
    [SerializeField] private GameObject[] healthBarUI;
    
    
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
        isInvurnable = invurnabilityCounter < 0 ? false : true;
        
        if (isInvurnable)
        {
            invurnabilityCounter -= Time.deltaTime;
        }

    }

    public void dealDamageToBoss(int damage)
    {
        if (!isInvurnable)
        {
            bossHealthCurrent -= damage;
            isInvurnable = true;
            invurnabilityCounter = damageVurnabilityPeriod;
            GameObject heartIcon = healthBarUI[bossHealthCurrent];
            heartIcon.SetActive(false);
            if (bossHealthCurrent <= 0)
            {
                killBoss();
            }
            
        }
    }

    private void killBoss()
    {
        //create death effect via animator
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
