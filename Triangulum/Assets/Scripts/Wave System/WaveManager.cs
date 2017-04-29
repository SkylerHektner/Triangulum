using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

    /// <summary>
    /// MUST BE ASSIGNED TO WAVE NOTIFIER PREFAB
    /// </summary>
    public GameObject waveNotifier;

    /// <summary>
    /// MUST BE ASSIGNED TO THE ENEMY PREFAB
    /// </summary>
    public GameObject enemy;

    /// <summary>
    /// The duration that a new wave notifier is displayed for
    /// </summary>
    public float waveNotifierDisplayTime;

    /// <summary>
    /// Used to keep track of how many monsters spawn on the first wave and how many are spawning on the current wave
    /// </summary>
    public float spawnNumber;

    /// <summary>
    /// Used to assign the rate of growth in spawn number of minions per round
    /// </summary>
    public float spawnGrowthRate;

    /// <summary>
    /// Used to keep track of the delay between monster spawns in a given wave and assign the spawn rate for the first wave.
    /// </summary>
    public float spawnDelay;

    /// <summary>
    /// Used to assign the rate at which the spawnDelay decreases each wave
    /// </summary>
    public float spawnDelayDecay;

    /// <summary>
    /// The current wave of enemies you are on
    /// </summary>
    public int currentWave { get; private set; }


    private List<GameObject> enemies = new List<GameObject>();
    private bool waveSpawnDone = false;
	
	void Start () {
        currentWave = 1;
        StartCoroutine(displayWaveNumber());
	}
	
	
	void Update ()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }

        if (waveSpawnDone && enemies.Count == 0) // sign to start next wave
        {
            waveSpawnDone = false;
            spawnNumber *= spawnGrowthRate;
            spawnDelay *= spawnDelayDecay;
            currentWave++;
            StartCoroutine(displayWaveNumber());
        }
	}

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

    IEnumerator spawnEnemies()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject c = Instantiate(enemy);
            enemies.Add(c);

            int spawnSide = Random.Range(0, 3);
            if (spawnSide == 0) // top of map
            {
                c.transform.localPosition = new Vector2(BoardInfo.top, Random.Range(BoardInfo.left, BoardInfo.right));
            }

            else if (spawnSide == 1) // bottom of map
            {
                c.transform.localPosition = new Vector2(BoardInfo.bottom, Random.Range(BoardInfo.left, BoardInfo.right));
            }

            else if (spawnSide == 2) // right of map
            {
                c.transform.localPosition = new Vector2(Random.Range(BoardInfo.bottom, BoardInfo.top), BoardInfo.right);
            }

            else if (spawnSide == 3) // left of map
            {
                c.transform.localPosition = new Vector2(Random.Range(BoardInfo.bottom, BoardInfo.top), BoardInfo.left);
            }

            yield return new WaitForSeconds(spawnDelay);
        }

        waveSpawnDone = true;
    }
}
