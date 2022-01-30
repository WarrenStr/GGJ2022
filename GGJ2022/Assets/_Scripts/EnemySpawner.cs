//Created by Sam Kreimer 1/29/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] List<GameObject> startSpawnPoints = new List<GameObject>();
    private int ranNum;

    private void Start()
    {
        //saves original spawnPoints for future use
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            startSpawnPoints.Add(spawnPoints[i]);
        }
        
    }

    /// <summary>
    /// if there is still available spawnpoints, chooses random spawnpoint and passes it to InstantiateEnemy(). removes that spawnpoint for future use
    /// </summary>
    /// <param name="numOfEnemy">how many enemies you want to spawn</param>
    /// <param name="typeOfEnemy">what enemy prefab you want to spawn</param>
    public void SpawnEnemy(int numOfEnemy, GameObject typeOfEnemy)
    {
        while(numOfEnemy > 0 )
        {
            if(spawnPoints.Count > 0)
            {
                ranNum = Random.Range(0, spawnPoints.Count);
                InstantiateEnemy(spawnPoints[ranNum], typeOfEnemy);
                spawnPoints.RemoveAt(ranNum);


            }
            numOfEnemy--;
        }
    }

    public IEnumerator SpawnEnemyTimed(GameObject typeOfEnemy)
    {
        yield return new WaitForSeconds(Random.Range(8f, 16f));
        if(spawnPoints.Count > 0)
        {
            ranNum = Random.Range(0, spawnPoints.Count);
            InstantiateEnemy(spawnPoints[ranNum], typeOfEnemy);
            spawnPoints.RemoveAt(ranNum);
        }
        else
        {
            ResetList();
        }

        StartCoroutine(SpawnEnemyTimed(typeOfEnemy));
    }

    /// <summary>
    /// sets SpawnPoints to its original self
    /// </summary>
    public void ResetList()
    {
        for (int i = 0; i < spawnPoints.Count; i++)//clears spawnpoints list
        {
            spawnPoints.RemoveAt(1);
        }
        for (int i = 0; i < startSpawnPoints.Count; i++)//reverts spawnpoints to original
        {
            spawnPoints.Add(startSpawnPoints[i]);
        }
    }

    /// <summary>
    /// Instantiates the chosen enemy type at the chosen spawnPoint
    /// </summary>
    /// <param name="whereToSpawn">empty gameobject with transform</param>
    /// <param name="whatToSpawn">chosen enemyPrefab</param>
    private void InstantiateEnemy(GameObject whereToSpawn, GameObject whatToSpawn)
    {
        Instantiate(whatToSpawn, whereToSpawn.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SpawnEnemy(1, enemyPrefab1);
        //    SpawnEnemy(1, enemyPrefab2);
        //    SpawnEnemy(1, enemyPrefab3);
        //    SpawnEnemy(1, enemyPrefab4);
        //    SpawnEnemy(1, enemyPrefab5);
        //    SpawnEnemy(1, enemyPrefab6);
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ResetList();
        //}
    }
}
