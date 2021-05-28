using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    // Threshold of vertical movement between bounces before applying downward force
    [SerializeField] private float minimumDeltaY = 0.05f;
    [SerializeField] private int bouncesBeforeIntervention = 5;

    private Rigidbody2D rb;
    private Vector2 startPosition;
    private float lastBounceY;
    private int numOfSameYBounces = 0;
    
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
        startPosition = new Vector2(rb.position.x, startPosition.y);
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
