using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinLauncher : MonoBehaviour
{
    public Transform firePoint;

    public Rigidbody projectilePrefab;

    public float launchForce = 10f;
    
    
    public void LaunchProjectile()
    {
        var projectileInstance = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation);

        projectileInstance.AddForce(firePoint.forward * launchForce);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
