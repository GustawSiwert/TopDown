using UnityEngine;

using System.Collections;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab; // Prefab på fienden
        public int enemyCount;         // Antal fiender i vågen
        public float spawnRate;        // Hur snabbt de spawnar
    }

    public Wave[] waves;
    public Transform[] spawnPointsMonster;
    public float timeBetweenWaves = 3f;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool spawning = false;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }
    public GameObject heartPrefab;
    public float heartSpawnChance = 0.1f; // 10% chans
    public Transform[] spawnPoint;
    //[SerializeField] win winScreen;


    void SpawnWave()
    {
        // spawn enemies...

        // spawn heart med chans
        if (Random.value < heartSpawnChance)
        {
            Transform randomSpot = spawnPointsMonster[Random.Range(0, spawnPointsMonster.Length)];
            Instantiate(heartPrefab, randomSpot.position, Quaternion.identity);
        }
    }
    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            if (!spawning)
            {
                spawning = true;
                yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                spawning = false;

                // Vänta tills alla fiender i vågen är döda


                // Kort paus innan nästa våg
                yield return new WaitForSeconds(timeBetweenWaves);

                currentWaveIndex++;
            }
        }

        Debug.Log("?? Alla vågor klara!");
        //winScreen.Show();

    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Startar våg: " + (currentWaveIndex + 1));
        enemiesAlive = wave.enemyCount;

        SpawnWave();
        for (int i = 0; i <= wave.enemyCount; i++)
        {
            Transform spawnPoint = spawnPointsMonster[Random.Range(0, spawnPointsMonster.Length)];
            GameObject enemy = Instantiate(wave.enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Fienden berättar för spawnern när den dör
            //Enemy enemyScript = enemy.AddComponent<Enemy>();
            //enemyScript.spawner = this;

            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
    }

    // Kallas från fiender när de dör
    public void EnemyDied()
    {
        enemiesAlive--;
    }
}
