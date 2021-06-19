using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Brick : MonoBehaviour, IPoolable<Brick>
{
    [SerializeField] private Text hitpointText;

    private int hitpoints;
    private BrickPool pool;
    private InterpolatedMover mover;

    private void Awake()
    {
        mover = GetComponent<InterpolatedMover>();
        mover.MoveComplete = OnMoveComplete;
    }

    public void Move(bool shouldLerpMovement)
    {
        if (shouldLerpMovement) mover.MoveDown();
        else transform.position -= Vector3.up * LevelManager.yMoveAmount;
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
        if(this.pool == null) this.pool = (BrickPool) pool;
    }

    private void DeactivateSelf()
    {
        if (!gameObject.activeSelf) return;

        this.gameObject.SetActive(false);
        pool.ReturnObject(this);
        pool.OnAllBricksDestroyed();
    }

    private void OnMoveComplete()
    {
        PlayerController.EnableInput();

        // Game over, restart the game
        if (transform.position.y < -4) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
