using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TrajectoryVisualizer tv;

    private Vector2 startPosition;
    private bool isDragging = false;

    private void Update()
    {
        if (isDragging)
        {
            tv.UpdateTrajectory(Vector2.zero, CalculateLaunchDirection());
        }
    }

    public void OnPress()
    {
        isDragging = true;
        startPosition = Pointer.current.position.ReadValue();
    }

    public void OnRelease()
    {
        isDragging = false;
        tv.ResetTrajectory();
    }

    // Launch direction is opposite of the drag direction
    private Vector2 CalculateLaunchDirection()
    {
        return (startPosition - Pointer.current.position.ReadValue()).normalized;
    }
}
