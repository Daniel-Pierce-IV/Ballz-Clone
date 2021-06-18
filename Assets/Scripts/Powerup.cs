using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, IPoolable<Powerup>
{
    private GameObjectPool<Powerup> pool;
    private InterpolatedMover mover;

    private void Awake()
    {
        mover = GetComponent<InterpolatedMover>();
    }

    public void Move(bool shouldLerpMovement)
    {
        if (shouldLerpMovement) mover.MoveDown();
        else transform.position -= Vector3.up * LevelManager.yMoveAmount;
    }

    public void SetPool(GameObjectPool<Powerup> pool)
    {
        if (this.pool == null) this.pool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pool.ReturnObject(this);
        gameObject.SetActive(false);
        AudioController.instance.QueueAudioClip(AudioController.AudioType.pickup);
    }
}
