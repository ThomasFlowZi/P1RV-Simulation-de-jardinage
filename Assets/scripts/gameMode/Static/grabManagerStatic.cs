using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class GrabManagerStatic : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float rayDistance = 10f;

    private Ray _ray;
    private RaycastHit _hit;

    private bool grabActive = false;
    private GameObject grabbedObject;
    private Plane grabPlane;


    private Vector3 hitPoint;

    [Header("Grab Settings")]
    public List<int> SnapableObject = new List<int>();

    public UnityEvent triggerStartGrab;
    public UnityEvent triggerStopGrab;




    private void Start()
    {
        StopGrab(); 
    }

    private void OnDisable()
    {
        

        StopGrab();
          



    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        _ray = Camera.main.ScreenPointToRay(mousePos);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_ray, out _hit, rayDistance))
            {
                GameObject hitObj = _hit.collider.gameObject;
                if (hitObj.CompareTag("Selectable"))
                {
                    grabbedObject = hitObj;
                    
                    StartGrab();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            StopGrab();
            
        }

        if (grabActive && grabbedObject != null)
        {
            MoveGrabbedObject(mousePos);
        }
    }

    void MoveGrabbedObject(Vector2 mousePos)
    {
        if (grabbedObject == null) return;

        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        if (!grabPlane.Raycast(mouseRay, out float distance)) return;

        hitPoint = mouseRay.GetPoint(distance);

        //  Si l'objet n'est PAS snapable mouvement normal
        if (!SnapableObject.Contains(grabbedObject.layer))
        {
            
            grabbedObject.transform.position = hitPoint;
            return;
        }

        // L'objet est snapable  on cherche une snap zone
        int snapMask = LayerMask.GetMask("SnapZone");
        if (!Physics.Raycast(mouseRay, out RaycastHit hit, distance, snapMask))
        {
            grabbedObject.transform.position = hitPoint;

            if (grabbedObject.GetComponent<State>().GetSnap() != false) grabbedObject.GetComponent<State>().SetSnap(false);
            return;
        }

        // On vérifie si le point est bien DANS le box collider
        BoxCollider box = hit.collider.GetComponent<BoxCollider>();
        bool inside = IsPointInsideBoxCollider(box, hitPoint);

        if (inside)
        {

           
            grabbedObject.transform.position = hit.collider.transform.position;
            if(grabbedObject.GetComponent<State>().GetSnap() != true) grabbedObject.GetComponent<State>().SetSnap(true) ;
        }
        else
        {

            
            grabbedObject.transform.position = hitPoint;
            if (grabbedObject.GetComponent<State>().GetSnap() != false) grabbedObject.GetComponent<State>().SetSnap(false);
        }
    }

    void StartGrab()
    {


        Transform camera = Camera.main.transform;


        if (grabbedObject.GetComponent<Rigidbody>() != null) grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

        ////////////////// ici on active tous les scripts généraux du mode grab ////////////////////////////////
        Debug.Log("passage en mode grab !");
        grabActive = true;
        triggerStartGrab.Invoke();



        ///////////////////////// ici on active tous les scripts dédiés a l'objet grab ///////////////////////////////


        foreach (var g in grabbedObject.GetComponents<IGrabbable>())
      
            g.OnGrabStart();

 
         

        grabPlane = new Plane(new Vector3(camera.forward.x, 0, camera.forward.z), grabbedObject.transform.position);

    }

    void StopGrab()
    {

        ////////////////// ici on desactive tous les scripts généraux du mode grab ////////////////////////////////

        grabActive = false;
        triggerStopGrab.Invoke();


        ////////////////// ici on desactive tous les scripts dédiés a l'objet grab ////////////////////////////////

        if (grabbedObject != null)
        {
            foreach (var g in grabbedObject.GetComponents<IGrabbable>())
                g.OnGrabEnd();
        }

        grabbedObject = null;

    }

    public static bool IsPointInsideBoxCollider(BoxCollider box, Vector3 point)
    {
        Vector3 localPoint = box.transform.InverseTransformPoint(point);

        Vector3 halfSize = box.size / 2f;

        return (localPoint.x >= box.center.x - halfSize.x && localPoint.y >= box.center.y - halfSize.y && localPoint.z >= box.center.z - halfSize.z) &&
            (localPoint.x <= box.center.x + halfSize.x && localPoint.y <= box.center.y + halfSize.y && localPoint.z <= box.center.z + halfSize.z);


    }

    public bool IsGrabbing() => grabActive;

    public GameObject WhatGrab()
    {
        return grabbedObject;
    }

    public void SetPlane(Plane p)
        { grabPlane = p; }


    public void setGrabbedObject(GameObject objet)
    {

        grabbedObject = objet; 


    }
}