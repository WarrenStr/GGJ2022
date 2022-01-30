using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;

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
    public BananaSpawner subtractBanana;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

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

    void RestoreHealth(int heal)
    {
        currentHealth += heal;

        healthBar.SetHealth(currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            TakeDamage(5);
        }

        if (collision.gameObject.tag == "Nanner")
        {
            RestoreHealth(10);
            subtractBanana.SubtractBanana(1);
        }
        
    }

}
