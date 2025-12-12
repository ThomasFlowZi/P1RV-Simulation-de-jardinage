
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


    private Transform main;

    public UnityEvent triggerAnimationObject;
    public UnityEvent triggerInteract;

    public List<GameObject> FixedPositionObject;

    void Start()
    {
        main = GameObject.Find("player").transform.Find("positionTete/main");

    }
    void Update()
    {
        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, camera.forward);
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

    void ResetLastGrabbedObject(bool fix)
    {

        if (selection != null && selection.GetComponent<Rigidbody>() != null) selection.GetComponent<Rigidbody>().isKinematic = false;
        if (selection.GetComponent<Collider>() != null) selection.GetComponent<Collider>().enabled = true;
        if (selection.GetComponent<GrabVisualFeedback>() != null) selection.GetComponent<GrabVisualFeedback>().OnGrabEnd();

        if (fix)
        {
            selection.position = lastPos;
            selection.rotation = lastRot;
        }
        rootSelection.SetParent(null);
        selection = null;
        rootSelection = null;


    }

    void GrabObject(Transform hitTransform)
    {
        Transform camera = Camera.main.transform;
        selection = hitTransform;
        lastPos = selection.position;
        lastRot = selection.rotation;

        selection.position = main.position + camera.up * 0.2f;
        selection.rotation = camera.rotation;
        if (selection.GetComponent<GrabVisualFeedback>() != null) selection.GetComponent<GrabVisualFeedback>().OnGrabStart();
        if (selection.GetComponent<Collider>() != null) selection.GetComponent<Collider>().enabled = false;
        if (selection.GetComponent<Rigidbody>() != null) selection.GetComponent<Rigidbody>().isKinematic = true;

        rootSelection = selection.root;

        rootSelection.SetParent(main);

    }

    public Transform WhatTarget()
    {
        return hitTransform;

    }

}