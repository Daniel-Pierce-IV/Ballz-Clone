using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryVisualizer : MonoBehaviour
{
    [SerializeField] private float lineLength = 5f;

    private LineRenderer line;
    private int numOfPositions = 2;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    //public void UpdateTrajectory(Vector2 start, Vector2 direction)
    public void UpdateTrajectory()
    {
        // TODO creating so many Vectors per second could be a memory/performance issue.
        // Look into caching line points

        Vector3[] positions = new Vector3[numOfPositions];

        positions[0] = BallManager.launchPosition;
        positions[1] = BallManager.launchPosition
            + (Vector3) PlayerController.launchDirection * lineLength;

        line.positionCount = numOfPositions;
        line.SetPositions(positions);
    }

    public void ResetTrajectory()
    {
        line.positionCount = 0;
    }
}
