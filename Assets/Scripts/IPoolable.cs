using UnityEngine;

// TODO research pros/cons of merging Component and IPoolable<T>
// into a PoolableComponent (or some such) abstract subclass. As it stands,
// the current implementation looks/feels improper
public interface IPoolable<T> where T : Component, IPoolable<T>
{
	void SetPool(GameObjectPool<T> pool);
}
