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
    // The level this waveManager is inside of
    public int currentLevel = 1;
    // the target wave to unlock the next level
    public int waveToUnlockNextLevel = 99999;


    private List<GameObject> enemiesInScene = new List<GameObject>();
    private bool waveSpawnDone = false;
    private List<Transform> spawnPoints;
	
	void Start ()
    {
        currentWave = 1;
        StartCoroutine(displayWaveNumber(false));

        // populate the spawn points list and remove the spawn point at 0,0,0
        spawnPoints = new List<Transform>(transform.Find("SpawnPoints").GetComponentsInChildren<Transform>());
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i].localPosition == Vector3.zero)
            {
                spawnPoints.RemoveAt(i);
                break;
            }
        }

        upgradeLoader.data.lastLevelPlayed = currentLevel;
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
            bool displayLevelUnlocked = false;
            // if this is our new highest wave on this level, record it!
            if (upgradeLoader.data.highestWave[currentLevel - 1] < currentWave)
            {
                upgradeLoader.data.highestWave[currentLevel - 1] = currentWave;
                upgradeLoader.Instance.SaveData();
                displayLevelUnlocked = currentWave == waveToUnlockNextLevel;
            }

            // set up for the next even harder wave
            waveSpawnDone = false;
            spawnNumber *= spawnGrowthRate;
            spawnDelay *= spawnDelayDecay;
            currentWave++;
            StartCoroutine(displayWaveNumber(displayLevelUnlocked));

            // tell the HUD to display the new current wave
            HUDManager.Instance.setCurrentWaveText(currentWave);
        }
	}

    // A simple routine to display the wave number before a wave
    IEnumerator displayWaveNumber(bool displayLevelUnlocked)
    {
        GameObject c = Instantiate(waveNotifier);
        Text t = c.GetComponentInChildren<Text>();

        if (displayLevelUnlocked)
        {
            Color colorUnlocked = t.color;
            colorUnlocked.a = 0;
            t.color = colorUnlocked;
            t.text = "Level " + (currentLevel + 1).ToString() + " Unlocked";

            float delayTimeUnlocked = waveNotifierDisplayTime / 200;

            for (float i = 0; i < 100; i++)
            {
                colorUnlocked.a = i / 100;
                t.color = colorUnlocked;
                yield return new WaitForSeconds(delayTimeUnlocked);
            }

            for (float i = 100; i >= 0; i--)
            {
                colorUnlocked.a = i / 100;
                t.color = colorUnlocked;
                yield return new WaitForSeconds(delayTimeUnlocked);
            }
        }

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
        int spawnIndex = 0;
        int spawnCounter = 0;

        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject c = Instantiate(enemies[spawnIndex]);
            enemiesInScene.Add(c);

            int spawnPoint = Random.Range(0, spawnPoints.Count - 1);
            c.transform.localPosition = spawnPoints[spawnPoint].localPosition;

            yield return new WaitForSeconds(spawnDelay);

            // increment spawn counter, and check if the ratio at the current index is satisfied
            spawnCounter += 1;
            if (spawnCounter == enemySpawnRatios[spawnIndex])
            {
                // if the ratio is satisfied, move the index up one and reset the spawn counter
                spawnCounter = 0;
                spawnIndex += 1;
            }
            // if the index is beyond the enemies length, reset the index and begin at the beginning of the ratio
            if (spawnIndex == enemies.Length)
            {
                spawnIndex = 0;
            }
        }

        waveSpawnDone = true;
    }
}
