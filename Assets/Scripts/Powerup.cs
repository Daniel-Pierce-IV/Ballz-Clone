using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, IPoolable<Powerup>
{
    private GameObjectPool<Powerup> pool;

    public void SetPool(GameObjectPool<Powerup> pool)
    {
        if (this.pool == null) this.pool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pool.ReturnObject(this);
        gameObject.SetActive(false);
    }
}
