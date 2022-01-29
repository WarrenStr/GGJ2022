using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] List<GameObject> startSpawnPoints = new List<GameObject>();
    private int ranNum;

    [SerializeField] GameObject enemyPrefab1, enemyPrefab2, enemyPrefab3, enemyPrefab4, enemyPrefab5, enemyPrefab6;


    private void Start()
    {
        print(spawnPoints[1]); 
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            startSpawnPoints.Add(spawnPoints[i]);
        }
        print(spawnPoints.Count);
    }

    public void SpawnEnemy(int numOfEnemy, GameObject typeOfEnemy)
    {
        
        while(numOfEnemy > 0 )
        {
            if(spawnPoints.Count > 0)
            {
                ranNum = Random.Range(0, spawnPoints.Count);
                print(ranNum);
                InstantiateEnemy(spawnPoints[ranNum], typeOfEnemy);
                spawnPoints.RemoveAt(ranNum);


            }

            numOfEnemy--;
        }


    }

    public void ResetList()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnPoints.RemoveAt(1);
        }
        for (int i = 0; i < startSpawnPoints.Count; i++)
        {
            print(i);
            spawnPoints.Add(startSpawnPoints[i]);
        }
    }

    private void InstantiateEnemy(GameObject whereToSpawn, GameObject whatToSpawn)
    {
        Instantiate(whatToSpawn, whereToSpawn.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy(1, enemyPrefab1);
            SpawnEnemy(1, enemyPrefab2);
            SpawnEnemy(1, enemyPrefab3);
            SpawnEnemy(1, enemyPrefab4);
            SpawnEnemy(1, enemyPrefab5);
            SpawnEnemy(1, enemyPrefab6);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetList();
        }
    }
}
