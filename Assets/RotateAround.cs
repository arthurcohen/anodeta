using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update() 
    {
        transform.RotateAround(target.position, Vector3.forward, 40 * Time.deltaTime);
    }
}
