using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {

    public static Transform[] spawnPoints;

	void Awake () {
        // initialize size of spawn points
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Store transform of each spawn point in game
            spawnPoints[i] = transform.GetChild(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
