using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

    // MUST BE ASSIGNED TO WAVE NOTIFIER PREFAB
    public GameObject waveNotifier;
    // array of the different enemies that can spawn in this scene
    public GameObject[] enemies;
    // array of the spawn ratio between enemies in the scene
    public int[] enemySpawnRatios;
    // The duration that a new wave notifier is displayed for
    public float waveNotifierDisplayTime;
    // Used to keep track of how many monsters spawn on the first wave and how many are spawning on the current wave
    public float spawnNumber;
    // Used to assign the rate of growth in spawn number of monsters per round
    public float spawnGrowthRate;
    // Used to keep track of the delay between monster spawns in a given wave and assign the spawn rate for the first wave.
    public float spawnDelay;
    // Used to assign the rate at which the spawnDelay decreases each wave
    public float spawnDelayDecay;
    // The current wave of enemies you are on
    public int currentWave { get; private set; }


    private List<GameObject> enemiesInScene = new List<GameObject>();
    private bool waveSpawnDone = false;
    private Transform[] spawnPoints;
	
	void Start ()
    {
        currentWave = 1;
        StartCoroutine(displayWaveNumber());
        spawnPoints = transform.Find("SpawnPoints").GetComponentsInChildren<Transform>();
	}
	
	
	void Update ()
    {
        // scan through our list of enemies and remove null pointers (dead enemies)
        for (int i = 0; i < enemiesInScene.Count; i++)
        {
            if (enemiesInScene[i] == null)
            {
                enemiesInScene.RemoveAt(i);
            }
        }

        // If we have finished spawning enemies and there are no remaining enemies, start next wave
        if (waveSpawnDone && enemiesInScene.Count == 0) 
        {
            waveSpawnDone = false;
            spawnNumber *= spawnGrowthRate;
            spawnDelay *= spawnDelayDecay;
            currentWave++;
            StartCoroutine(displayWaveNumber());
        }
	}

    // A simple routine to display the wave number before a wave
    IEnumerator displayWaveNumber()
    {
        GameObject c = Instantiate(waveNotifier);
        Text t = c.GetComponentInChildren<Text>();

        Color color = t.color;
        color.a = 0;
        t.color = color;
        t.text = "Wave " + currentWave.ToString();

        float delayTime = waveNotifierDisplayTime / 200;

        for (float i = 0; i < 100; i++)
        {
            color.a = i/100;
            t.color = color;
            yield return new WaitForSeconds(delayTime);
        }

        for (float i = 100; i >= 0; i--)
        {
            color.a = i/100;
            t.color = color;
            yield return new WaitForSeconds(delayTime);
        }

        Destroy(c);

        StartCoroutine(spawnEnemies());
    }

    // The main routine for spawning enemies
    IEnumerator spawnEnemies()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject c = Instantiate(enemies[0]);
            enemiesInScene.Add(c);

            int spawnPoint = Random.Range(0, spawnPoints.Length - 1);
            c.transform.localPosition = spawnPoints[spawnPoint].localPosition;

            yield return new WaitForSeconds(spawnDelay);
        }

        waveSpawnDone = true;
    }
}
