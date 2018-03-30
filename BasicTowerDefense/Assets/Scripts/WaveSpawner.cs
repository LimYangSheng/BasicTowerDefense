using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    // Variable to help with spawning enemy
    public Transform[] enemyPrefab;
    private Transform[] spawnLocations;

    private float countdown = 2f;
    public static int waveNumber = 0;
    private float timeBetweenWaves = 10f;

    // UI variables
    public Text CountdownText;

    void Awake()
    {
        // Store transform of all spawn points
        spawnLocations = SpawnPoints.spawnPoints;
    }

    // Update is called once per frame
    void Update () {
        // Set the count down text every update
        CountdownText.text = Mathf.Round(countdown).ToString();

        // Is count down reached?
        if (countdown <= 0f)
        {
            // Yes! Spawn enemy
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        // Decrement the count down timer
        countdown -= Time.deltaTime;   
	}

    // Spawn enemy wave
    public IEnumerator SpawnWave()
    {
        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {
            // Spawn batch of enemies every 0.2s
            SpawnEnemies();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void SpawnEnemies()
    {
        int spawnIndex;

        // Spawn 1 of each type of enemy
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            // Randomize the spawn location to use
            spawnIndex = Random.Range(0, spawnLocations.Length -1);

            // Spawn the enemy object
            Instantiate(enemyPrefab[i], spawnLocations[spawnIndex].position, spawnLocations[spawnIndex].rotation);
        }
    }

    // Returns value of the current wave
    public static int GetCurrentWave()
    {
        return waveNumber;
    }

    // Allow reset of static wave number
    public static void ResetCurrentWave()
    {
        waveNumber = 0;
    }
}
