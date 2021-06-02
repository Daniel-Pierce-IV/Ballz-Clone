using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour, IPoolable<Brick>
{
    [SerializeField] private float lerpDuration = 0.5f;
    [SerializeField] private Text hitpointText;

    // Not configurable unless the way the stage/environment is set up changes
    private const float yMoveAmount = 1.1f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTimestamp;

    private int hitpoints;

    private GameObjectPool<Brick> pool;

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition != endPosition && endPosition != null)
        {
            if (Time.time <= startTimestamp + lerpDuration)
            {
                float y = Mathf.Lerp(startPosition.y, endPosition.y, CalculateLerpTime());
                Vector3 newPosition = transform.localPosition;
                newPosition.y = y;
                transform.localPosition = newPosition;
            }
            else
            {
                transform.localPosition = endPosition;
            }
        }
    }

    public void MoveDown()
    {
        startPosition = transform.localPosition;
        endPosition = startPosition;
        endPosition.y = endPosition.y - yMoveAmount;
        startTimestamp = Time.time;
    }

    // Returns 0.0 - 1.0 relative to startTimestamp
    private float CalculateLerpTime()
    {
        return Mathf.Clamp((Time.time - startTimestamp) / lerpDuration, 0, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage();
    }

    private void TakeDamage()
    {
        hitpoints--;
        RefreshHitpointText();
        if (hitpoints <= 0) DeactivateSelf();
    }

    public void RefreshHitpointText()
    {
        hitpointText.text = hitpoints.ToString();
    }

    public void SetHitpoints(int value)
    {
        hitpoints = value;
        RefreshHitpointText();
    }

    public void SetPool(GameObjectPool<Brick> pool)
    {
        if(this.pool == null) this.pool = pool;
    }

    private void DeactivateSelf()
    {
        hitpoints = 0;
        pool.ReturnObject(this);
        this.gameObject.SetActive(false);
    }
}
