using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent launchDirectionChanged;
    public UnityEvent ballLaunched;

    public static Vector2 launchDirection { get; private set; }

    private bool inputEnabled = true;
    private const int MOUSE_BUTTON_PRIMARY = 0;
    private Vector2 startPosition;

    private void Update()
    {
        if (!inputEnabled) return;

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
            UpdateLaunchDirection(MousePositionVector2());
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
            UpdateLaunchDirection(Input.touches[0].position);
        }
    }

    private void UpdateLaunchDirection(Vector3 newPosition)
    {
        CalculateLaunchDirection(newPosition);
        launchDirectionChanged.Invoke();
    }

    private void StopDragging()
    {
        if (launchDirection == Vector2.zero) return;

        inputEnabled = false;
        ballLaunched.Invoke();
    }

    public void EnableInput()
    {
        inputEnabled = true;
    }

    private Vector2 MousePositionVector2()
    {
        return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    // Launch direction is opposite of the drag direction
    private void CalculateLaunchDirection(Vector2 currentPosition)
    {
        launchDirection = (startPosition - currentPosition).normalized;

        if (launchDirection.y <= 0) launchDirection = Vector2.zero;
    }
}
