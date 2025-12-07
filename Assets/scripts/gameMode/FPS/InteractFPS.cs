
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
    public UnityEvent triggerUseObject;
    public UnityEvent triggerHandInteraction;

    void Start()
    {
        main = GameObject.Find("player").transform.Find("positionTete/main");

    }
    void Update()
    {
        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, camera.forward);
        RaycastHit hit;

        int mask = ~LayerMask.GetMask("Ignore Raycast");

        
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(ray, out hit, rayDistance, mask))
                {
                    hitTransform = hit.collider.transform;
                  

                    if (hitTransform.CompareTag("Selectable"))  //un nouvel objet est grabable donc on le prend
                    {
                        if (selection != null)
                        {
                            ResetLastGrabbedObject();
                        }


                        GrabObject(hitTransform);


                    }
                    else
                    {
                        if (selection != null) // on utilise l'objet qui est dans notre main pour avoir un effet sur un autre objet
                        {
                        triggerAnimationObject.Invoke();
                        triggerUseObject.Invoke();
                        }   
                        else // on interragit la main vide
                        {
                            triggerHandInteraction.Invoke();

                        }

                    }
                    
                }
                else if (selection != null) // on fait simplement l'animation dans le vide
                {
                triggerAnimationObject.Invoke();
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && selection != null)
            {
                ResetLastGrabbedObject();
    
            }

        


    }

    void ResetLastGrabbedObject()
    {

        if (selection != null && selection.GetComponent<Rigidbody>() != null) selection.GetComponent<Rigidbody>().isKinematic = false;
        if (selection.GetComponent<Collider>() != null) selection.GetComponent<Collider>().enabled = true;

        selection.position = lastPos;
        selection.rotation = lastRot;
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