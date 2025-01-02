using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class opossumProjectileHandler : MonoBehaviour
{
    public GameObject[] oposssumProjectileArray;
    public float projectileSpeed;
    public Sprite[] projectileSpriteArray;

    public void Start()
    {
        foreach (var projectile in oposssumProjectileArray)
        {
            projectile.SetActive(false);
        }
    }

    public void throwThreeProjectiles(Vector2 direction)
    {
        reanableProjectiles();
        int throwVariation = 0;
        foreach (var projectile in oposssumProjectileArray)
        {
            Rigidbody2D projectileRigidBody2d = projectile.GetComponent<Rigidbody2D>();
            Vector2 throwDirection = new Vector2(direction.x, (direction.y + throwVariation * Mathf.Abs(direction.x))).normalized;
            projectileRigidBody2d.velocity = throwDirection * projectileSpeed;
            throwVariation++;
        }
    }

    private void reanableProjectiles()
    {
        foreach (var projectile in oposssumProjectileArray)
        {
            SpriteRenderer projectileSprite = projectile.GetComponent<SpriteRenderer>();
            int projectileSpriteIndex = Random.Range(0, projectileSpriteArray.Length);
            projectileSprite.sprite = projectileSpriteArray[projectileSpriteIndex];
            projectile.transform.position = transform.position;
            projectile.SetActive(true);
        }
    }
}
