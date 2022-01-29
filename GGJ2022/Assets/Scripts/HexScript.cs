using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexScript : MonoBehaviour
{

    [SerializeField] MeshRenderer HexRenderer;
    private BuildMaster BM;

    void Start()
    {
        BM = GameObject.FindGameObjectWithTag("BuildManager").GetComponent<BuildMaster>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("hi");
        HexRenderer.enabled = true;
        if (Input.GetKeyDown("b"))
        {
            print("b");
            BM.openMenu();
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        HexRenderer.enabled = false;

    }
}
