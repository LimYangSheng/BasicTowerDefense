using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler objectPool;

    // Variables for object pooling
    public List<GameObject> pooledObjectsList;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake()
    {
        objectPool = this;
    }


    // Use this for initialization
    void Start () {
        pooledObjectsList = new List<GameObject>();

        // Instantiate all bullet game object and hide them
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject bullet = (GameObject)Instantiate(objectToPool);
            bullet.SetActive(false);
            pooledObjectsList.Add(bullet);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Return a bullet object to caller
    public GameObject GetPooledObject()
    {
        // Loop through all objects in the list
        for (int i = 0; i < pooledObjectsList.Count; i++)
        {
            // Find the first bullet object that is hidden / not active and return it
            if (!pooledObjectsList[i].activeInHierarchy)
            {
                return pooledObjectsList[i];
            }
        }
        // Expand the list when list runs out of hidden bullet objects  
        return ExpandObjectPool();
    }

    // Increase the size of the object pool
    private GameObject ExpandObjectPool()
    {
        // Spawning a new bullet game object
        GameObject bullet = (GameObject)Instantiate(objectToPool);
        bullet.SetActive(false);
        pooledObjectsList.Add(bullet);
        return bullet;
    }
}
