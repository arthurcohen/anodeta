using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target;
    public float targetVelocity = 40f;

    // Update is called once per frame
    void Update() 
    {
        transform.RotateAround(target.position, Vector3.forward, targetVelocity * Time.deltaTime);
    }
}
