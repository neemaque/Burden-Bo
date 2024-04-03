using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private Collider2D collider;
    private Transform target;
    private string currentState;
    void Start()
    {
        currentState = "stopped";
    }

    void Update()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x) < 3f && Mathf.Abs(transform.position.y - player.transform.position.y) < 0.5f)
        {
            currentState = "chasing";
        }
        else
        {
            currentState = "stopped";
        }


        // STATES
        if(currentState == "chasing")
        {
            target = player.transform;
        }

        //TARGET FOLLOW
        if(currentState != "stopped")
        {
            if(target.position.x > transform.position.x)
            {
                //rb.velocity = new Vector2(speed, 0);
                transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }

    }
    //DEATH
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Panda")
        {
            int hitDirection = 0;
            if(collision.gameObject.transform.position.x > transform.position.x)hitDirection = -1;
            else hitDirection = 1;
            Death(hitDirection);
        }
    }

    private void Death(int hitDirection)
    {
        Debug.Log("IM DEAD LOL");
        currentState = "stopped";
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(new Vector2(hitDirection*2f,8f), ForceMode2D.Impulse);
        rb.AddTorque(100f);
        collider.enabled = false;
    }
}
