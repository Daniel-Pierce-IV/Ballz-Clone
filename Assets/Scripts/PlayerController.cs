using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TrajectoryVisualizer tv;

    private const int MOUSE_BUTTON_PRIMARY = 0;
    private Vector2 startPosition;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Ended) StopDragging();
            else HandleTouchInput();
        }
        else if (Input.GetMouseButton(MOUSE_BUTTON_PRIMARY))
        {
            HandleMouseInput();
        }
        else if (Input.GetMouseButtonUp(MOUSE_BUTTON_PRIMARY))
        {
            StopDragging();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(MOUSE_BUTTON_PRIMARY))
        {
            startPosition = MousePositionVector2();
        }
        else
        {
            tv.UpdateTrajectory(Vector2.zero,
                CalculateLaunchDirection(MousePositionVector2()));
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            startPosition = Input.touches[0].position;
        }
        else
        {
            tv.UpdateTrajectory(Vector2.zero,
                CalculateLaunchDirection(Input.touches[0].position));
        }
    }

    private void StopDragging()
    {
        tv.ResetTrajectory();
    }

    private Vector2 MousePositionVector2()
    {
        return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    // Launch direction is opposite of the drag direction
    private Vector2 CalculateLaunchDirection(Vector2 currentPosition)
    {
        return (startPosition - currentPosition).normalized;
    }
}
