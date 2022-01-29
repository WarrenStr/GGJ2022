//created by Sam Kreimer 1/28/22
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    [Header("Building statistics")]
    [SerializeField] int numOfHitsToDestroy;
    [SerializeField] GameObject UndamagedBuilding;
    [SerializeField] GameObject Rubble;
    [SerializeField] GameObject Trees;

    [SerializeField] int timeTillTreesSpawn = 5;

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
    [SerializeField] GameObject hitParticle;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildingHit();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Gorilla")
        {
            BuildingHit();
            Instantiate(hitParticle, other.transform.position, Quaternion.identity);
        }
    }

    public void BuildingHit()
    {
        chooseEnemy(enemyCoward, spawnCoward, numberOfCoward);
        chooseEnemy(enemyMelee, spawnMelee, numberOfMelee );
        chooseEnemy(enemyBow, spawnBow, numberOfBow);
        chooseEnemy(enemyMusket, spawnMusket, numberOfMusket);
        numOfHitsToDestroy--;
        if(numOfHitsToDestroy < 1) //switches building with rubble when hits is 0
        {
            UndamagedBuilding.SetActive(false);
            Rubble.SetActive(true);
            gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(RubbleDespawn());
        }
    }

    public IEnumerator RubbleDespawn() //after set time, the rubble will despawn and trees will apear
    {
        yield return new WaitForSeconds(timeTillTreesSpawn);
        Trees.SetActive(true);
        Rubble.SetActive(false);
    }

    /// <summary>
    /// Takes input and sends that info to the EnemySpawner to instantiate
    /// </summary>
    /// <param name="enemyList">List of the enemy you want to spawn</param>
    /// <param name="enemyBool">true = spawn this type of enemy, false = dont spawn</param>
    /// <param name="enemyNum">number of enemies that will spawn</param>
    private void chooseEnemy(List<GameObject> enemyList, bool enemyBool, int enemyNum)
    {
        if (enemyBool)
        {
            while (enemyNum > 0)
            {
                ranNum = Random.Range(0, enemyList.Count);
                spawner.SpawnEnemy(1, enemyList[1]);
                enemyNum--;
            }
        }
    }
}
