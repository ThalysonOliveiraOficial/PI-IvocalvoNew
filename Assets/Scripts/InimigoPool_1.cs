using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPool_1 : MonoBehaviour
{
    public static InimigoPool_1 SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    public InimigoContrPool _IniCtrlPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        _IniCtrlPool = Camera.main.GetComponent<InimigoContrPool>();

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


}
