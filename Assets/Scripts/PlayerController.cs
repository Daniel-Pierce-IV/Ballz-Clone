using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TrajectoryVisualizer tv;
    [SerializeField] private Rigidbody2D Ball;
    [SerializeField] private float ballSpeed = 5f;

    private const int MOUSE_BUTTON_PRIMARY = 0;
    private Vector2 startPosition;
    private Vector2 launchDirection;

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
            CalculateLaunchDirection(MousePositionVector2());
            tv.UpdateTrajectory(Ball.position, launchDirection);
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
            CalculateLaunchDirection(Input.touches[0].position);
            tv.UpdateTrajectory(Ball.position, launchDirection);
        }
    }

    private void StopDragging()
    {
        tv.ResetTrajectory();
        Ball.velocity = launchDirection * ballSpeed;
    }

    private Vector2 MousePositionVector2()
    {
        return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    // Launch direction is opposite of the drag direction
    private void CalculateLaunchDirection(Vector2 currentPosition)
    {
        launchDirection = (startPosition - currentPosition).normalized;
    }
}
