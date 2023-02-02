using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIStayScared : MonoBehaviour
{
    private Animator enemyAnim;
    public NavMeshAgent agent;

    public float health;
    private float speed = 1;
    public float speedMultipler = 2;

     int xPos1, xPos2;
    int zPos1, zPos2;

    public GameObject emptty;

    //scared var
    public float randomRange;
    public float newPosTimer;
    Vector3 randomSpot;
    public bool isScared;
    private bool startScared;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        xPos1 = gm.xPos1;
        xPos2 = gm.xPos2;
        zPos1 = gm.zPos1;
        zPos2 = gm.zPos2;
        print($"{xPos1} {xPos1} {xPos2} {zPos1} {zPos2}");
    }

    void Update()
    {
        Scared();

        //print(randomSpot.x + " " +randomSpot.z);
    }


    private void Scared()
    {
        if (!startScared)
        {
            startScared = true;
            randomSpot.x = Random.Range(xPos1, xPos2);
            randomSpot.z = Random.Range(zPos1, zPos2);
            StartCoroutine(NewPosScared());
        }
        if (agent.transform.position.x == randomSpot.x && agent.transform.position.z == randomSpot.z)
        {

            randomSpot.x = Random.Range(xPos1, xPos2);
            randomSpot.z = Random.Range(zPos1, zPos2);
        }

        agent.speed = speed * speedMultipler;
        agent.SetDestination(randomSpot);
    }
    private IEnumerator NewPosScared()
    {
        yield return new WaitForSeconds(newPosTimer);

        randomSpot.x = Random.Range(xPos1, xPos2);
        randomSpot.z = Random.Range(zPos1, zPos2);

        StartCoroutine(NewPosScared());
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            enemyAnim.enabled = false;
            gameObject.GetComponent<EnemyAIStayScared>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            //Invoke(nameof(DestroyEnemy), .5f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gdmg1")
        {
            //Debug.Log("Player Hit RUN");
            TakeDamage(1);
        }
        if (other.tag == "Gdmg2")
        {
            //Debug.Log("Player Hit WRIST");
            TakeDamage(3);
        }
        if (other.tag == "Missile")
        {
            TakeDamage(1);
        }
    }
}
