using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bananner : MonoBehaviour
{
    public Player getHealth;
    
    void OnTriggerEnter(Collider other)
    {
        getHealth.RestoreHealth(20);
    }

}
