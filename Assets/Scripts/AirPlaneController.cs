using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneController : MonoBehaviour
{
    public float Speed;
    public float Acceleration;

    Rigidbody2D rb;

    public float RotationControl;

    float MovX, MovY = 1;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        MovY = Input.GetAxis("Vertical");
        MovX = Input.GetAxis("Horizontal");
        //Debug.Log("MovY = " + MovY);
    }
    private void FixedUpdate()
    { 
        //Debug.Log("MovY = " + MovY);
        Vector2 Vel = transform.right *  (MovX * Acceleration);
        rb.AddForce(Vel);

        float Dir = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right));
        //Debug.Log("1");
        if ( Acceleration > 0)
        {
            //Debug.Log("2");
            if ( Dir > 0)
            {
                //Debug.Log("3");
                rb.rotation += MovY * RotationControl * (rb.velocity.magnitude / Speed);
            }
            else
            {
                //Debug.Log("4");
                rb.rotation -= MovY * RotationControl * (rb.velocity.magnitude / Speed);
            }
        }

        float thrustForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.down)) * 2.0f;
        Vector2 relForce = Vector2.up * thrustForce;
        //Debug.Log("5");
        if (rb.velocity.magnitude > Speed)
        {
            rb.velocity = rb.velocity.normalized * Speed;
            //Debug.Log("6");
        }
    }
}
