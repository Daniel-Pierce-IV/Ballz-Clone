using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolatedMover : MonoBehaviour
{
    [SerializeField] private float lerpDuration = 0.5f;

    // Not configurable unless the way the stage/environment is set up changes
    private const float yMoveAmount = 1.1f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTimestamp;

    public void MoveDown()
    {
        startPosition = transform.localPosition;
        endPosition = startPosition;
        endPosition.y = endPosition.y - yMoveAmount;
        startTimestamp = Time.time;

        StartCoroutine(InterpolateMovement());
    }

    private IEnumerator InterpolateMovement()
    {
        while(transform.localPosition != endPosition && endPosition != null)
        {
            if (Time.time <= startTimestamp + lerpDuration)
            {
                float y = Mathf.Lerp(startPosition.y, endPosition.y, CalculateLerpTime());
                Vector3 newPosition = transform.localPosition;
                newPosition.y = y;
                transform.localPosition = newPosition;

                yield return null; // wait for the next frame
            }
            else
            {
                transform.localPosition = endPosition;
            }
        }
    }

    // Returns 0.0 - 1.0 relative to startTimestamp
    private float CalculateLerpTime()
    {
        return Mathf.Clamp((Time.time - startTimestamp) / lerpDuration, 0, 1);
    }
}
