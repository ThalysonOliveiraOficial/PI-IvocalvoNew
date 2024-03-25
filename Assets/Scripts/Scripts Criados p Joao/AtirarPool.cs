using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarPool : MonoBehaviour
{
    public static AtirarPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    [SerializeField] float _timer;
    [SerializeField] float _timerValue = 10;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                pooledObjects[i].SetActive(false);
            }

            _timer = _timerValue;
        }
    }

}
