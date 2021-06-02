using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Threshold of vertical movement between bounces before applying downward force
    [SerializeField] private float minimumDeltaY = 0.05f;
    [SerializeField] private int bouncesBeforeIntervention = 5;

    private Rigidbody2D rb;
    private Vector2 startPosition;
    private float lastBounceY;
    private int numOfSameYBounces = 0;
    private BallManager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
        lastBounceY = rb.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleBallStalling();
    }

    private void HandleBallStalling()
    {
        float deltaY = Mathf.Abs(lastBounceY - rb.position.y);

        if (deltaY < minimumDeltaY) numOfSameYBounces++;
        else numOfSameYBounces = 0;

        if (numOfSameYBounces >= bouncesBeforeIntervention)
        {
            rb.AddForce(-transform.up, ForceMode2D.Impulse);
        }

        lastBounceY = rb.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Powerup"))
        {
            manager.IncrementBallCount();
        }
        else // Collided with kill zone
        {
            startPosition = new Vector2(rb.position.x, startPosition.y);
            rb.velocity = Vector2.zero;
            transform.position = startPosition;
            manager.ReturnBall(this);
        }
    }

    public void SetManager(BallManager manager)
    {
        if (this.manager == null) this.manager = manager;
    }

    public void MoveToLaunchPosition(Vector3 launchPosition)
    {
        transform.position = launchPosition;
    }
}










//void Update()
//{
//    if (transform.localPosition != endPosition && endPosition != null)
//    {
//        if (Time.time <= startTimestamp + lerpDuration)
//        {
//            float y = Mathf.Lerp(startPosition.y, endPosition.y, CalculateLerpTime());
//            Vector3 newPosition = transform.localPosition;
//            newPosition.y = y;
//            transform.localPosition = newPosition;
//        }
//        else
//        {
//            transform.localPosition = endPosition;
//        }
//    }
//}

//public void MoveDown()
//{
//    startPosition = transform.localPosition;
//    endPosition = startPosition;
//    endPosition.y = endPosition.y - yMoveAmount;
//    startTimestamp = Time.time;
//}

//// Returns 0.0 - 1.0 relative to startTimestamp
//private float CalculateLerpTime()
//{
//    return Mathf.Clamp((Time.time - startTimestamp) / lerpDuration, 0, 1);
//}
