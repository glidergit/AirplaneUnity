using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 screenPos;
    private Rigidbody2D rb;
    private Rigidbody rb3;
    public Transform itemSubObject;
    public float speed;
    public float rotationSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb3 = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            Vector3 dir = (screenPos - transform.position);

            dir.Normalize();
            if (rb != null)
                rb.AddForce(dir * speed);
            if (rb3 != null)
            {
                //rb3.AddForce(dir * speed);
                //!!!transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            float angle = 0;
            angle = Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg;


            if (!float.IsNaN(angle))
            {
                if (dir.x < 0) angle += 180;
                Debug.Log("angle = " + angle);
                if (rb != null)
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                else if (rb3 != null)
                {
                    //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    //transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), Time.deltaTime * rotationSpeed);

                    itemSubObject.transform.localRotation = Quaternion.Lerp(itemSubObject.transform.localRotation, Quaternion.Euler(new Vector3(0, 90, angle)), Time.deltaTime * rotationSpeed);
                }
            }
        } catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        Vector2 mousePos = context.ReadValue<Vector2>(); //Camera.main.ScreenToWorldPoint( context.ReadValue<Vector2>());
        Vector2 viewPortPos = Camera.main.ScreenToViewportPoint(mousePos);
        screenPos = Camera.main.ScreenToWorldPoint(mousePos);

        Debug.Log("mousePos = "+mousePos);
        Debug.Log("viewPortPos = "+ viewPortPos);

        Debug.Log("screenPos = "+ screenPos);

    }
}
