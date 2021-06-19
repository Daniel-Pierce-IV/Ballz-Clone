using UnityEngine.Events;

public class BrickPool : GameObjectPool<Brick>
{
	public UnityEvent allBricksDestroyed;

	public void OnAllBricksDestroyed()
	{
		if (AllObjectsAreAvailable()) allBricksDestroyed.Invoke();
	}
}
