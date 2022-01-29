using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    [Header("Building statistics")]
    [SerializeField] int numOfHitsToDestroy;


    [Header("Which enemies to spawn and how many")]
    [SerializeField] bool spawnCoward;
    [SerializeField] int numberOfCoward;
    [SerializeField] bool spawnMelee;
    [SerializeField] int numberOfMelee;
    [SerializeField] bool spawnBow;
    [SerializeField] int numberOfBow;
    [SerializeField] bool spawnMusket;
    [SerializeField] int numberOfMusket;



    [Header("List of enemy prefabs")]
    [SerializeField] List<GameObject> enemyCoward = new List<GameObject>();
    [SerializeField] List<GameObject> enemyMelee = new List<GameObject>();
    [SerializeField] List<GameObject> enemyBow = new List<GameObject>();
    [SerializeField] List<GameObject> enemyMusket = new List<GameObject>();

    [Header("Drag in SpawnEnemies")]
    [SerializeField] EnemySpawner spawner;

    private int ranNum;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildingHit();
        }
    }

    public void BuildingHit()
    {

        chooseEnemy(enemyCoward, spawnCoward, numberOfCoward);
        chooseEnemy(enemyMelee, spawnMelee, numberOfMelee );
        chooseEnemy(enemyBow, spawnBow, numberOfBow);
        chooseEnemy(enemyMusket, spawnMusket, numberOfMusket);
        numOfHitsToDestroy--;
        if(numOfHitsToDestroy < 1)
        {

        }
    }


    private void chooseEnemy(List<GameObject> enemyList, bool enemyBool, int enemyNum)
    {
        if (enemyBool)
        {
            while (enemyNum > 0)
            {
                ranNum = Random.Range(0, enemyList.Count);
                spawner.SpawnEnemy(1, enemyList[ranNum]);
                enemyNum--;
            }
        }


    }
}
