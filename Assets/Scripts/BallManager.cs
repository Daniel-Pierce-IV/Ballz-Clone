using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExtensions;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject ballContainer;
    [SerializeField] private Transform killZone;
    [SerializeField] private Text ballCounterText;
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private float ballSpeed = 5f;
    [SerializeField] private float ballLaunchDelay = 0.2f;
    [SerializeField] private float ballReturnDuration = 0.25f;
    [SerializeField] private float ballFastForwardDelay = 10f;
    [SerializeField] private float ballFastForwardFactor = 2f;

    public static Vector3 launchPosition { get; private set; }

    private Vector2 launchVelocity;
    private float baselineY;
    private List<Ball> balls = new List<Ball>();
    private int totalBalls = 0;
    private int returnedBalls = 0;
    private int ballsToAdd = 0;
    private bool isFastForwarding = false;

    // Start is called before the first frame update
    void Start()
    {
        InitializeLaunchPosition();
        CreateBall();
        UpdateBallCounterText();
    }

    private void UpdateBallCounterText()
    {
        ballCounterText.text = totalBalls + "x";
    }

    public void LaunchBalls()
    {
        UpdateLaunchVelocity();
        Invoke("StartFastForwarding", ballFastForwardDelay);
        StartCoroutine(LaunchBallsWithDelay());
    }

    private IEnumerator LaunchBallsWithDelay()
    {
        foreach (Ball ball in balls)
        {
            ball.rb.velocity = launchVelocity;
            totalBalls--;
            UpdateBallCounterText();

            // Launch balls at a faster rate if we're fast forwarding
            if (!isFastForwarding) yield return new WaitForSeconds(ballLaunchDelay);
            else yield return new WaitForSeconds(ballLaunchDelay / ballFastForwardFactor);
        }
    }

    private void InitializeLaunchPosition()
    {
        Vector3 position = killZone.position;
        baselineY = position.y + ballPrefab.transform.localScale.y / 2 + 0.05f;
        position.y = baselineY;
        UpdateLaunchPosition(position);
    }

    public void UpdateLaunchPosition(Vector3 newPosition)
    {
        launchPosition = newPosition;
    }

    private void CreateBall()
    {
        Ball ball = Instantiate(
            ballPrefab,
            launchPosition,
            Quaternion.identity,
            ballContainer.transform);

        ball.SetManager(this);
        balls.Add(ball);
        totalBalls++;
    }

    private void CreatePowerupBalls()
    {
        for (int i = 0; i < ballsToAdd; i++)
        {
            CreateBall();
        }

        ballsToAdd = 0;
    }

    public void ReturnBall(Ball ball)
    {
        returnedBalls++;
        ball.transform.SetPositionY(baselineY);

        if (returnedBalls == 1) UpdateLaunchPosition(ball.transform.position);
        else ball.MoveToLaunchPosition(launchPosition, ballReturnDuration);

        if (returnedBalls == balls.Count)
        {
            isFastForwarding = false;
            CancelInvoke("StartFastForwarding");
            totalBalls = balls.Count;
            returnedBalls = 0;
            CreatePowerupBalls();
            UpdateBallCounterText();
            FindObjectOfType<GameController>().DeactivateBalls();
        }
    }

    public void IncrementBallCount()
    {
        ballsToAdd++;
    }

    private void StartFastForwarding()
    {
        isFastForwarding = true;
        UpdateLaunchVelocity();

        foreach (Ball ball in balls)
        {
            ball.rb.velocity *= ballFastForwardFactor;
        }
    }

    private void UpdateLaunchVelocity()
    {
        launchVelocity = PlayerController.launchDirection * ballSpeed;

        if (isFastForwarding) launchVelocity *= ballFastForwardFactor;
    }
}
