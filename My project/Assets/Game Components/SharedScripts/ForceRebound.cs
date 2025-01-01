using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceRebound : MonoBehaviour
{

    public void ApplyReboundForce(Rigidbody2D rigidbody, Vector2 collsionNormal, float reboundForce)
    {
        Vector2 reboundVelocity = collsionNormal * reboundForce;
        rigidbody.AddForce(reboundVelocity, ForceMode2D.Impulse);
    }
}
