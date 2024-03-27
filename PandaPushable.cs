using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaPushable : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Panda")
        {
            rb.isKinematic = false;
        }
    }
    void FixedUpdate()
    {
        if(rb.IsSleeping() && !rb.isKinematic)
        {
            rb.isKinematic = true;
        }
    }
}
