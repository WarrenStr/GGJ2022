using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public int maxPenguin = 3;
    public int currentPenguin;

    public float maxRechargeTimer = 1.0f;
    public float rechargeTimer;

    public HealthBar healthBar;
    public PenguinCount penguinCounter;
    public PenguinCount penguinRecharge;
    public PenguinLauncher penguinLauncher;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentPenguin = maxPenguin;
        penguinCounter.SetPenguin(maxPenguin);
    }

    // Update is called once per frame
    void Update()
    {
        penguinRecharge.PenguinRecharge(rechargeTimer);
        penguinCounter.SetPenguin(currentPenguin);
        
        

        if (Input.GetKeyDown(KeyCode.Z) && (currentPenguin > 0))
        {
            currentPenguin -= 1;
            penguinLauncher.LaunchProjectile();
            //penguinCounter.SetPenguin(currentPenguin);
        }

        if (currentPenguin < 3)
        {
            rechargeTimer -= Time.deltaTime;
        }

        if (rechargeTimer <= 0.0f)
        {
            currentPenguin += 1;
            rechargeTimer = maxRechargeTimer;
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach(ContactPoint contact in collision.contacts)
        {
            if (collision.relativeVelocity.magnitude > 2)
            {
                TakeDamage(5);
            }
                
        }
    }
}
