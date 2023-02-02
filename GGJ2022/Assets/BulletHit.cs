using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    public Collider sphere;
    public GameObject blood;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            sphere.enabled = false;
            Instantiate(blood, gameObject.transform.position, Quaternion.identity);
        }

    }
}
