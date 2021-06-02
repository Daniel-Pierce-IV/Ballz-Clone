using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameObjectPool<T> : MonoBehaviour where T : Component, IPoolable<T>
{
    [SerializeField] private T prefab;

    // TODO research pros/cons of making this a singleton

    private List<T> objects = new List<T>();
    private Queue<T> objectQueue = new Queue<T>();

    void CreateObject()
    {
        T newObject = Instantiate(prefab, transform);
        newObject.SetPool(this);
        objects.Add(newObject);
        objectQueue.Enqueue(newObject);
    }

    public T TakeObject()
    {
        if(objectQueue.Count == 0) CreateObject();

        return objectQueue.Dequeue();
    }

    public void ReturnObject(T returnedObject)
    {
        objectQueue.Enqueue(returnedObject);
    }

    public List<T> GetObjectList()
    {
        return objects;
    }
}
