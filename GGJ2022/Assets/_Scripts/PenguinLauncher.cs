using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinLauncher : MonoBehaviour
{
    public Transform firePoint;

    public Rigidbody projectilePrefab;

    public float launchForce = 10f;

    public Animator gorillaAnim;
    
    
    public void LaunchProjectile()
    {
        Invoke("HitChest", 1.0f);

        //gorillaAnim.SetBool("isChestHit", true);

        //Invoke("CancelChestHit", 3.0f);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HitChest()
    {
        var projectileInstance = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation);

        projectileInstance.AddForce(firePoint.forward * launchForce);
    }
    
    void CancelChestHit()
    {
        //gorillaAnim.SetBool("isChestHit", false);
    }
}
