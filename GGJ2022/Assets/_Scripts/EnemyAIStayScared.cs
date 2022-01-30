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
    }

    void Update()
    {
        Scared();
    }


    private void Scared()
    {
        if (!startScared)
        {
            startScared = true;
            randomSpot.x = Random.Range(-7, 67);
            randomSpot.z = Random.Range(-35, 11);
            StartCoroutine(NewPosScared());
        }
        if (agent.transform.position.x == randomSpot.x && agent.transform.position.z == randomSpot.z)
        {
            print("Hi");
            randomSpot.x = Random.Range(-7, 67);
            randomSpot.z = Random.Range(-35, 11);
        }

        agent.speed = speed * speedMultipler;
        agent.SetDestination(randomSpot);
    }
    private IEnumerator NewPosScared()
    {
        yield return new WaitForSeconds(newPosTimer);

        randomSpot.x = Random.Range(-7, 67);
        randomSpot.z = Random.Range(-35, 11);

        StartCoroutine(NewPosScared());
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            enemyAnim.enabled = false;
            gameObject.GetComponent<EnemyAIStayScared>().enabled = false;
            //Invoke(nameof(DestroyEnemy), .5f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gorilla")
        {
            Debug.Log("Player Hit");
            TakeDamage(3);
        }
    }
}
