using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaWrist : MonoBehaviour
{
    [SerializeField] Mover moveScript;

    private void Start()
    {
        moveScript = transform.parent.gameObject.GetComponent<Mover>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Building")
        {
            moveScript.wristCollide.isTrigger = false;
            print("hit building");
        }
    }
}
