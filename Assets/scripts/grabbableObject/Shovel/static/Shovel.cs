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

        initialPosition = transform.position;
        initialRotation = transform.rotation;
        lastPos = transform.position;
        enabled = false;
    }





    void Update()
    {
        if (isGrabbed)
        {
            // Rotation pendant le grab
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(grabbedRotation),
                Time.deltaTime * transitionSpeed
            );
        }
        else if (wasGrabbed)
        {
            // Retour à la position de base
            transform.position = Vector3.Lerp(
                transform.position,
                initialPosition,
                Time.deltaTime * transitionSpeed
            );

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                initialRotation,
                Time.deltaTime * transitionSpeed
            );

            // Si revenu presque parfaitement : reset
            if (Vector3.Distance(transform.position, initialPosition) < 0.01f &&
                Quaternion.Angle(transform.rotation, initialRotation) < 0.01f)
            {
                wasGrabbed = false;
                rb.isKinematic = false;
                transform.rotation = initialRotation;  

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

    public string HUDInfo(GameObject phi)
    {
        return "Prendre Pelle";
        
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
        speed = -(transform.position.y - lastPos.y) / Time.deltaTime;
        lastPos = transform.position;
    }



    

    public float GetSpeed() => speed;
}