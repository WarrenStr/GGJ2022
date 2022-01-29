using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float  speed;

    private void Start()
    {
    
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;

        if (Input.GetMouseButton(2))
        {
            transform.LookAt(target);

            Vector3 mousePos = Input.mousePosition.normalized;
            Vector3 rotateAxis = new Vector3(0, mousePos.y, 0);
            if(mousePos.y > 0.5f)
            {
                transform.Rotate(rotateAxis * speed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(rotateAxis * -speed * 2 *Time.deltaTime);
            }
            

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(Input.mousePosition.normalized);
        }
    }
}
