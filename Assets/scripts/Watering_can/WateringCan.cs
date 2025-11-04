using UnityEngine;

public class WateringPot : MonoBehaviour
{
    private GrabManagerStatic grabManager;


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

        if (isGrabbed) wasGrabbed = isGrabbed;


        if (isGrabbed)
        {
            if (!rb.isKinematic) rb.isKinematic = true;
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

                }
                else transform.localRotation = Quaternion.Lerp(transform.localRotation, initialRotation, Time.deltaTime * transitionSpeed);
            }
        }


        WPSpeed();

    }

    void WPSpeed()
    {
        speed = (transform.localPosition - lastPos).magnitude / Time.deltaTime;
        lastPos = transform.localPosition;

    }

    public float GetSpeed() { return speed; }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10) //snap zone
        {
            grabManager.Snap = true;
            transform.localPosition = other.transform.position;
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.gameObject.layer == 10) //snap zone
        {
        grabManager.Snap = false;
        }
    }



}