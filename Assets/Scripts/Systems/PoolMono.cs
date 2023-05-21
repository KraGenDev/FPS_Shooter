using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMono <T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool AutoExpand { get; set; }
    public Transform Container { get; }

    private List<T> _pool;

    public PoolMono(T prefab, int count)
    {
        Prefab = prefab;
        Container = null;

        CreatePool(count);
    }    
    
    public PoolMono(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;

        CreatePool(count);
    }

    public List<T> GetAllObjects() => _pool;
    
    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }
    
    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(Prefab, Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);
        
        return createdObject;
    }

    private bool HasFreeElement(out T element)
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            var poolObject = _pool[i];
            if (!poolObject.gameObject.activeInHierarchy)
            {
                element = poolObject;
                poolObject.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        if (AutoExpand)
        {
            return CreateObject(true);
        }

        throw new Exception("There is no free elements in pool");
    }
}
