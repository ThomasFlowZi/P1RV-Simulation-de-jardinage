using UnityEngine;
using System.Collections;

public class WateringPot : MonoBehaviour, IGrabbable
{
    private GrabManagerStatic grabManager;


    public float transitionSpeed = 5f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isGrabbed = false;
    private bool wasGrabbed = false;
    Rigidbody rb;
    private float speed;
    private Vector3 lastPos;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        

        initialPosition = transform.position;
        initialRotation = transform.rotation;
        lastPos = transform.position;
        enabled = false;
    }

    void Update()
    {
        

        if (isGrabbed)
        {
            if (!rb.isKinematic) rb.isKinematic = true;
        }
        else
        {
            // Retour à la position de base
            transform.position = Vector3.Lerp(
                transform.position,
                initialPosition,
                Time.deltaTime * transitionSpeed
            );

            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                initialRotation,
                Time.deltaTime * transitionSpeed
            );

            // Si revenu presque parfaitement : reset
            if (Vector3.Distance(transform.position, initialPosition) < 0.01f &&
                Quaternion.Angle(transform.localRotation, initialRotation) < 0.01f)
            {
                wasGrabbed = false;
                rb.isKinematic = false;
                transform.position = initialPosition;
            }
        }


    WPSpeed();

    }

    // ----------- INTERFACE ------------
    public void OnGrabStart()
    {
        enabled = true;
        isGrabbed = true;
        wasGrabbed = true;
    }



    public void OnGrabEnd()
    {
        
        StartCoroutine(waitWasGrabbed());
        isGrabbed = false;
        // wasGrabbed reste true pour le retour automatique
    }

    public string HUDInfo(GameObject phi)
    {
         return "Prendre Seau";
    }

    private IEnumerator waitWasGrabbed()
    {
        while (true)
        {
            if (wasGrabbed) { yield return null; }
            else { enabled = false; yield break; }
        }
    }
    // ----------------------------------

    void WPSpeed()
    {
        speed = (transform.position - lastPos).magnitude / Time.deltaTime;
        lastPos = transform.position;

    }

    public float GetSpeed() { return speed; }



}