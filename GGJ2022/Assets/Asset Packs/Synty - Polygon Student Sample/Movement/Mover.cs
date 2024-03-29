﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private Camera cam;
    NavMeshAgent playerNavAgent;
    private Animator animator;

    public GameManager GM;

    [SerializeField] GameObject mainMusic;
    [SerializeField] GameObject deathMusic;

    [SerializeField]
    private float runSpeed = 10.0f;
    [SerializeField]
    private float walkSpeed = 5.0f;

    public float runMeter = 5.0f;
    public float runMeterTimer = 5.0f;
    public float runMeterRegenTime = 5.0f;
    private bool isRunning = false;

    public bool attack;
    public SphereCollider wristCollide;
    public SphereCollider wristCollideEnemyDmg;
    public CapsuleCollider runeCollide;

    bool isPlayerDead;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerDead = false;
        playerNavAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        wristCollide.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        KillPlayer(isPlayerDead);

        Attacking();
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && !attack)
        {
            MoveToCursor();
        }
        UpdateAnimator();
        Running();
    }

    void MoveToCursor()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            playerNavAgent.destination = hit.point;
        }
        
        
    }

    void UpdateAnimator()
    {
        Vector3 navVelocity = playerNavAgent.velocity;
        Vector3 localNavVelocity = transform.InverseTransformDirection(navVelocity);
        float speed = localNavVelocity.z;
        //animator.SetFloat("Blend", speed);

        if (speed > 0.1f && !isRunning)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else if(speed > 0.1f && isRunning)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }


    }

    void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            runeCollide.isTrigger = true;
            playerNavAgent.speed = runSpeed;
        }
        else
        {
            isRunning = false;
            runeCollide.isTrigger = false;
            playerNavAgent.speed = walkSpeed;
        }

        if (isRunning)
        {
            //playerNavAgent.speed = runSpeed;
            //runMeterTimer -= 1 * Time.deltaTime;
        }
        

    }

    

    IEnumerator RunMeterRegen()
    {
        Debug.Log("Coroutine Started");
        yield return new WaitForSeconds(runMeterRegenTime);
        runMeterTimer = runMeter;
    }
    
    
    void Attacking()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !attack)
        {
            //if(transform.position == playerNavAgent.destination)



            animator.SetTrigger("isSwinging");

            StartCoroutine(AttackTimer());
            playerNavAgent.destination = transform.position;
            animator.SetBool("isAttacking", true);
        }
        else
        {
            //attack = false;
            animator.SetBool("isAttacking", false);
            
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Building")
        {
            wristCollide.isTrigger = false;
        }
    }


    IEnumerator AttackTimer()
    {
        attack = true;
        yield return new WaitForSeconds(0.4f);
        wristCollide.isTrigger = true;
        wristCollideEnemyDmg.isTrigger = true;
        yield return new WaitForSeconds(1.0f);
        attack = false;
        animator.ResetTrigger("isSwinging");
        wristCollide.isTrigger = false;
        wristCollideEnemyDmg.isTrigger = false;
    }



    public void KillPlayer(bool playerDead)
    {
       int playerHealth =  gameObject.GetComponent<Player>().currentHealth;

        if(playerHealth <= 0)
        {
            GM.loose();
            playerDead = true;
            playerNavAgent.destination = transform.position;
            animator.SetBool("isDead", true);
            mainMusic.SetActive(false);
            deathMusic.SetActive(true);

            return;
        }
        playerDead = false;
        return;
    }
}
