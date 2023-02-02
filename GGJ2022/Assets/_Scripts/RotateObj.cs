using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Rotate(0, 0.1f, 0, Space.Self);
    }
}
