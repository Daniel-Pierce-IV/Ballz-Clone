using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject launcher;
    [SerializeField] private GameObject line;

    public void EnableTrajectory()
    {
        transform.position = BallManager.launchPosition;
        launcher.SetActive(true);
    }

    public void UpdateTrajectory()
    {
        line.transform.up = PlayerController.launchDirection;
        line.SetActive(true);
    }

    public void DisableTrajectory()
    {
        launcher.SetActive(false);
        line.SetActive(false);
    }
}
