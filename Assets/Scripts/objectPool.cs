using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPool : MonoBehaviour
{
    // Now I know that there is a actual Pool library
    // But I already did it, so...

    static public List<GameObject> SetPool(GameObject objectToPool, int amountToPool)
    {
        List<GameObject> pooledObjects = new List<GameObject>();
        GameObject temp;

        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(objectToPool);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }

        return pooledObjects;
    }

    static public GameObject GetPooledObject(List<GameObject> pooledObjects)
    {
        int amount = pooledObjects.Count;
        for(int i = 0; i < amount; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
