using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public float speed;


    //scared var
    public float randomRange;
    public float lengthOfBeingScared;
    Vector3 randomSpot;
    public bool isScared;
    private bool startScared;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject enemyProjectile;



    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isScared = true;
        }
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(playerInSightRange && !playerInAttackRange && !isScared)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange && !isScared)
        {
            AttackPlayer();
        }
        if (isScared)
        {
            Scared();
            print("scared");
        }
    }

    private void ChasePlayer()
    {
        agent.speed = speed;
        agent.SetDestination(player.position);
    }

    private void Scared()
    {
        if (!startScared)
        {
            startScared = true;
            randomSpot = Random.insideUnitCircle * randomRange;
            StartCoroutine(ScaredForTime());
        }
        if(agent.destination == randomSpot)
        {
            randomSpot = Random.insideUnitCircle * randomRange;
        }

        agent.speed = speed * 2;
        agent.SetDestination(randomSpot);
    }
    private IEnumerator ScaredForTime()
    {
        yield return new WaitForSeconds(lengthOfBeingScared);
        isScared = false;
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // put attack animation here
            Rigidbody rb = Instantiate(enemyProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            Destroy(rb.gameObject, 3);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), .5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gorilla")
        {
            Debug.Log("Player Hit");
            TakeDamage(3);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
