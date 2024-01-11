using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] float cooldownTime = 5f;
    float timer;

    float camHeight;
    float camWidth;
    [SerializeField] float range = 0.8f; // Screen's range, where 1 is full-screen and 0 is nothing

    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountToPool;

    List<GameObject> pooledObjects = new List<GameObject>();

    private void Awake()
    {
        pooledObjects = objectPool.SetPool(objectToPool, amountToPool);
    }
    void Start()
    {
        timer = cooldownTime;

        var cam = Camera.main;
        camHeight = cam.orthographicSize * range;
        camWidth = cam.orthographicSize * cam.aspect * range;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            float x, y;
            x = Random.Range(-camWidth, camWidth);
            y = Random.Range(-camHeight, camHeight);
            
            GameObject objectToSpawn = objectPool.GetPooledObject(pooledObjects);
            if (objectToSpawn != null )
            {
                objectToSpawn.transform.position = new Vector2(x, y);
                objectToSpawn.transform.rotation = Quaternion.identity;
                objectToSpawn.SetActive(true);
            }
            timer = cooldownTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
