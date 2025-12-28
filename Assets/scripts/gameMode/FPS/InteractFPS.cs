
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InteractFPS : MonoBehaviour
{
    public float rayDistance = 10f;
    

    private Transform selection;
    private Transform rootSelection;
    private Vector3 lastPos;
    private Quaternion lastRot;
    private Transform hitTransform;

    public AudioSource audioSource;
    public AudioClip grabSound;

    Transform cam;

    private Transform main;

    public bool isGrabbing = false;

    private Vector3 grabOffsetPos;
    private Quaternion grabOffsetRot;

    public UnityEvent triggerAnimationObject;
    public UnityEvent triggerInteract;

    public List<GameObject> FixedPositionObject;

    void Start()
    {
        cam = Camera.main.transform;
        main = GameObject.Find("player").transform.Find("positionTete/main");

    }
    void Update()
    {
        Ray ray = new Ray(cam.position, cam.forward);
        RaycastHit hit;

        int mask = ~LayerMask.GetMask("Ignore Raycast", "SnapZone");

        
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(ray, out hit, rayDistance, mask))
                {
                    hitTransform = hit.collider.transform;
                  

                    if (hitTransform.CompareTag("Selectable"))  //un nouvel objet est grabable donc on le prend
                    {
                        if (selection != null)
                        {
                            ResetLastGrabbedObject(FixedPositionObject.Contains(rootSelection.gameObject) );
                        }


                        GrabObject(hitTransform);
                        audioSource.PlayOneShot(grabSound);


                    }
                    else
                    {
                        if (selection != null) // on utilise l'objet qui est dans notre main pour avoir un effet sur un autre objet
                        {
                        triggerAnimationObject.Invoke();
                        triggerInteract.Invoke();
                        }   
                        else // on interragit la main vide
                        {

                        triggerInteract.Invoke();

                        }

                    }
                    
                }
                else if (selection != null) // on fait simplement l'animation dans le vide
                {
                    hitTransform = null;
                    triggerAnimationObject.Invoke();
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && selection != null)
            {
            
                ResetLastGrabbedObject(FixedPositionObject.Contains(rootSelection.gameObject));  // certain objets ont une position unique dans la scène
            
                
            }

        


    }

    void LateUpdate()
    {
        if (!isGrabbing || rootSelection == null) return;

        Vector3 targetPos = main.position + cam.up * 0.2f;
        Quaternion targetRot = main.rotation;

        rootSelection.position = Vector3.Lerp(rootSelection.position, targetPos, Time.deltaTime * 20f);

        rootSelection.rotation = Quaternion.Slerp(rootSelection.rotation, targetRot, Time.deltaTime * 20f);
    }

    void ResetLastGrabbedObject(bool fix)
    {
        isGrabbing = false;
        if (selection == null) return;

        if ( selection.GetComponent<Rigidbody>() != null)
            selection.GetComponent<Rigidbody>().isKinematic = false;

        if ( selection.GetComponent<Collider>() != null)
            selection.GetComponent<Collider>().enabled = true;

        if (selection.GetComponent<GrabVisualFeedback>() != null)
            selection.GetComponent<GrabVisualFeedback>().OnGrabEnd();

        if (fix)
        {
            rootSelection.position = lastPos;
            rootSelection.rotation = lastRot;
        }
        else
        {
            rootSelection.position = cam.position + cam.forward * 1f;
        }
        selection = null;
        rootSelection = null;
    }

    void GrabObject(Transform hitTransform)
    {
        

        selection = hitTransform;
        rootSelection = selection.root;

        lastPos = rootSelection.position;
        lastRot = rootSelection.rotation;

        // Position visuelle initiale
        rootSelection.position = main.position + cam.up * 0.2f;
        rootSelection.rotation = main.rotation;


        if (selection.GetComponent<GrabVisualFeedback>() != null)
            selection.GetComponent<GrabVisualFeedback>().OnGrabStart();

        if (selection.GetComponent<Collider>() != null)
            selection.GetComponent<Collider>().enabled = false;

        if (selection.GetComponent<Rigidbody>() != null)
            selection.GetComponent<Rigidbody>().isKinematic = true;

        isGrabbing = true;
    }

    public Transform WhatTarget()
    {
        return hitTransform;

    }
    public GameObject WhatHeldObject()
    {
        return selection ? selection.gameObject : null;

    }

}