using UnityEngine;

public class Shovel : MonoBehaviour
{
    private GrabManagerStatic grabManager; 
    public Vector3 grabbedRotation = new Vector3(-24f,221f, -170f); 
     
    public float transitionSpeed = 5f; 

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool wasGrabbed = false;
    Rigidbody rb;
    private float speed;
    private Vector3 lastPos;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        grabManager = FindAnyObjectByType<GrabManagerStatic>();

        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        lastPos = transform.localPosition;
    }

    void Update()
    {
        GameObject grabbed = grabManager.WhatGrab();

        
        bool isGrabbed = (grabbed == gameObject);

        if (isGrabbed )
        {
            
            wasGrabbed = isGrabbed;
        }

        if (isGrabbed)
        {
            rb.isKinematic = true;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(grabbedRotation), Time.deltaTime * transitionSpeed);
        }
        else
        {
            if (wasGrabbed)
            {
                if ((transform.localPosition - initialPosition).magnitude < 0.01f)
                {
                    rb.isKinematic = false;
                    transform.localPosition = initialPosition;
                    wasGrabbed = false;
                }
                else transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * transitionSpeed);


                if (Quaternion.Angle(transform.localRotation, initialRotation) < 0.01f)
                {
                    transform.localRotation = initialRotation;
                    wasGrabbed = false;
                }
                else transform.localRotation = Quaternion.Lerp(transform.localRotation, initialRotation, Time.deltaTime * transitionSpeed);
            }
        }


        ShovelSpeed();

    }

    void ShovelSpeed()
    {
        speed = (transform.localPosition - lastPos).magnitude / Time.deltaTime;
        lastPos = transform.localPosition;

    }

    public float GetSpeed() { return speed; }
}