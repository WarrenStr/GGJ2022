using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Animator enemyAnim;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    private float speed = 1;
    public float speedMultipler = 2;

    //scared var
    public float randomRange;
    public float lengthOfBeingScared;
    Vector3 randomSpot;
    public bool isScared;
    private bool startScared;

    private bool waitingToChase;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject enemyProjectile;
    public GameObject MuzzleFlash;
    public Transform shootPos;



    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
    }

    void Start()
    {
        waitingToChase = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    isScared = true;
        //}
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(playerInSightRange && !playerInAttackRange && !isScared && !waitingToChase)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange && !isScared )
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
        waitingToChase = false;
        enemyAnim.SetBool("isShooting", false);
        enemyAnim.SetBool("isChasing", true);
        agent.speed = speed * (speedMultipler/2);
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

        agent.speed = speed * speedMultipler;
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
        waitingToChase = true;

        if (!alreadyAttacked)
        {
            // put attack animation here
            enemyAnim.SetBool("isShooting", true);

        }
    }

    public void ShootGun()
    {
        MuzzleFlash.SetActive(true);
        Rigidbody rb = Instantiate(enemyProjectile, shootPos.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        StartCoroutine(WaitToChase());
        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
        Destroy(rb.gameObject, 3);
    }

    IEnumerator WaitToChase()
    {
        waitingToChase = true;

        yield return new WaitForSeconds(3);
        MuzzleFlash.SetActive(false);
        ChasePlayer();
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
            enemyAnim.enabled = false;
            gameObject.GetComponent<EnemyAI>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;

            //Invoke(nameof(DestroyEnemy), .5f);
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
        if(other.tag == "Missile")
        {
            TakeDamage(1);
        }
        if (other.tag == "Penguin")
        {
            TakeDamage(2);
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
