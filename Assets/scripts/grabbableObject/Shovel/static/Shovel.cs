using UnityEngine;
using System.Collections;

public class Shovel : MonoBehaviour, IGrabbable
{
    private GrabManagerStatic grabManager; 
    public Vector3 grabbedRotation = new Vector3(0,0, 0); 
     
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

        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        lastPos = transform.localPosition;
        enabled = false;
    }





    void Update()
    {
        if (isGrabbed)
        {
            // Rotation pendant le grab
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.Euler(grabbedRotation),
                Time.deltaTime * transitionSpeed
            );
        }
        else if (wasGrabbed)
        {
            // Retour à la position de base
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                initialPosition,
                Time.deltaTime * transitionSpeed
            );

            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                initialRotation,
                Time.deltaTime * transitionSpeed
            );

            // Si revenu presque parfaitement : reset
            if (Vector3.Distance(transform.localPosition, initialPosition) < 0.01f &&
                Quaternion.Angle(transform.localRotation, initialRotation) < 0.01f)
            {
                wasGrabbed = false;
                rb.isKinematic = false;
                transform.localPosition = initialPosition;
                transform.localRotation = initialRotation;  

            }
        }

        ShovelYRelativeSpeed();
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

    private IEnumerator waitWasGrabbed()
    {
        while (true)
        {
            if (wasGrabbed) { yield return null; }
            else { enabled = false; yield break; }
        }
    }
    // ----------------------------------

    void ShovelYRelativeSpeed()
    {
        speed = -(transform.localPosition.y - lastPos.y) / Time.deltaTime;
        lastPos = transform.localPosition;
    }



    

    public float GetSpeed() => speed;
}