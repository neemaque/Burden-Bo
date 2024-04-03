using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panda : MonoBehaviour
{
    [SerializeField] private float maxDistance = 1.5f;
    [SerializeField] private SpringJoint2D springJoint2D; 
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody2D rb;
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance (transform.position, playerTransform.position);
        if(distanceFromPlayer < maxDistance)
        {
            springJoint2D.enabled = false;
        }
        else
        {
            springJoint2D.enabled = true;
        }
    }
    public Vector2 getVelocity()
    {
        return rb.velocity;
    }
}
