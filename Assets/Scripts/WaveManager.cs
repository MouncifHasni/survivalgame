using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves; // List of waves
    public Transform[] spawnPoints; // Array of spawn points
    public float timeBetweenWaves = 10f; // Time between waves

    private int currentWaveIndex = 0; // Index of the current wave
    private bool isWaveInProgress = false; // Flag to track if a wave is currently in progress
    private bool isWaveComplete = false; // Flag to track if the current wave is complete
    private float timeLeftInWave; // Time left in the current wave

    // Start is called before the first frame update
    void Start()
    {
        // Start wave progression
        StartCoroutine(StartWaveProgression());
    }

    private void Update() {
        // Update time left in the wave
        if(isWaveInProgress) timeLeftInWave -= Time.deltaTime;
    }

    IEnumerator StartWaveProgression()
    {
        // Start with the first wave
        currentWaveIndex = 0;

        // Start wave progression loop
        while (currentWaveIndex < waves.Count)
        {
            // Start the wave
            StartWave(waves[currentWaveIndex]);

            // Wait until the wave is complete
            while (!isWaveComplete)
            {
                yield return null;
            }

            // Reset wave completion flag
            isWaveComplete = false;

            // Move to the next wave
            currentWaveIndex++;

            // Wait for the time between waves
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("All waves completed!");
    }

    void StartWave(Wave wave)
    {
        Debug.Log("Starting Wave: " + (currentWaveIndex + 1));

        // Set wave time duration
        timeLeftInWave = wave.duration;

        // Start wave spawning coroutine
        StartCoroutine(SpawnEnemies(wave));

        // Set wave in progress flag
        isWaveInProgress = true;
    }

    IEnumerator SpawnEnemies(Wave wave)
    {
        
        while (timeLeftInWave > 0)
        {
            foreach (GameObject enemy in wave.enemies)
            {
                // Randomly select a spawn point
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                
                // Instantiate the enemy prefab at the spawn point
                Instantiate(enemy, spawnPoint.position, Quaternion.identity);

                // Wait for the specified interval before spawning the next enemy
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            // Wait for the next frame to update time left in the wave
            yield return null;
            Debug.Log(timeLeftInWave);
        }

        Debug.Log("Wave "+(currentWaveIndex+1)+" complete!");
        isWaveInProgress = false;
        isWaveComplete = true;
    }
}
