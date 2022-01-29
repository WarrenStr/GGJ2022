using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private Camera cam;
    NavMeshAgent playerNavAgent;
    private Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        playerNavAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
        UpdateAnimator();
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
    }
}
