using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{
    public Transform[] movingStopPoints;
    public float moveSpeed;
    public float pasueInPosition;

    private int currentTargetStopPoint;

    private Transform platform;
    
    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Transform>();
        currentTargetStopPoint = 1;
    }

    // Update is called once per frame
    void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, movingStopPoints[currentTargetStopPoint].position,
            moveSpeed * Time.deltaTime);

        if (Vector3.Distance(platform.position, movingStopPoints[currentTargetStopPoint].position) < .05f)
        {
            currentTargetStopPoint = (currentTargetStopPoint + 1)% movingStopPoints.Length;
            StartCoroutine(pauseAtMovementStopPoint());
        }

    }

    private IEnumerator pauseAtMovementStopPoint()
    {
        float previousPlatformSpeed = moveSpeed;
        moveSpeed = 0;
        yield return new WaitForSeconds(pasueInPosition);
        moveSpeed = previousPlatformSpeed;
    }
}
