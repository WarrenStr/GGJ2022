//created by Sam Kreimer 1/28/22
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    [Header("What happens after the building is destoryed")]
    [SerializeField] int numOfHitsToDestroy;
    [SerializeField] GameObject UndamagedBuilding;
    [SerializeField] GameObject Rubble;
    [SerializeField] List<GameObject> naturePrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> natureSpawns = new List<GameObject>();

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
    private bool stopNatureSpawn;
    private GameManager GM;
    [SerializeField] GameObject hitParticle;


    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
            StartCoroutine(RubbleSpawn());
            GM.DestroyedABuilding();
        }
    }
    public IEnumerator RubbleSpawn() //turns off building, and turns on rubble. after set time trees will appear
    {
        UndamagedBuilding.SetActive(false);
        Rubble.SetActive(true);
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(timeTillTreesSpawn);
        StartCoroutine(NatureSpawn());
    }

    public IEnumerator NatureSpawn() //after set time, trees and nature will apear
    {
        
        yield return new WaitForSeconds(0.5f);
        ranNum = Random.Range(0, natureSpawns.Count);

        Instantiate(naturePrefabs[Random.Range(0, naturePrefabs.Count)], natureSpawns[ranNum].transform.position, Quaternion.identity);
        natureSpawns.RemoveAt(ranNum);

        ranNum = Random.Range(0, naturePrefabs.Count);
        print(ranNum);
        if (ranNum == 0)
        {
            stopNatureSpawn = true;
        }
        if (natureSpawns.Count < 1)
        {
            stopNatureSpawn = true;
        }
        if (!stopNatureSpawn)
        {
            StartCoroutine(NatureSpawn());
        }
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
                spawner.SpawnEnemy(1, enemyList[Random.Range(0, enemyList.Count)]);
                enemyNum--;
            }
        }
    }
}
