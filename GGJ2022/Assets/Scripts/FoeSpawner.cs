using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject Enemy_One;

    [SerializeField]
    private float enemyInterval = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, Enemy_One));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), 1, Random.Range(-6f, 6f)), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
