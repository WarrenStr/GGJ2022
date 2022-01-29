using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform camAnchor;
    [SerializeField] private float  speed;
    [SerializeField] private float  scrollSpeed;
    public Camera mainCam;
    public float zoomSpeed;
    float zoomSpeedRef;


    private void Start()
    {
        zoomSpeedRef = zoomSpeed;
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


       /* mainCam.transform.LookAt(target);

        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        Vector3 pos = mainCam.transform.localPosition;
        pos.z = Mathf.Clamp(mainCam.transform.localPosition.z, .5f, 30);
        mainCam.transform.localPosition = pos;

        float dist = Vector3.Distance(target.localPosition, pos);
        if (dist > 15)
        {
            zoomSpeed = zoomSpeedRef;
            mainCam.transform.localPosition = Vector3.MoveTowards(pos, target.localPosition, speed * Time.deltaTime);
            Debug.Log("Dist is greater than 15");
        }
        else if (dist < 5)
        {
            zoomSpeed = 0;
            mainCam.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, .6f);
            
            Debug.Log("Dist is less than 15");
        }

        mainCam.transform.Translate(0, 0, zoom);*/
    }
}
