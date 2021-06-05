using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;

public class InterpolatedMover : MonoBehaviour
{
    [SerializeField] private float lerpDuration = 0.5f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTimestamp;

    public void MoveDown()
    {
        startPosition = transform.position;
        endPosition = startPosition;
        endPosition.y = endPosition.y - LevelManager.yMoveAmount;
        startTimestamp = Time.time;

        StartCoroutine(InterpolateMovement());
    }

    private IEnumerator InterpolateMovement()
    {
        while(transform.position != endPosition)
        {
            transform.LerpPositionY(
                startPosition.y,
                endPosition.y,
                (Time.time - startTimestamp) / lerpDuration);

            yield return null; // wait for the next frame
        }
    }
}
