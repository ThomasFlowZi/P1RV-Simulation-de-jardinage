using UnityEngine;

public class ChangeDistanceStatic : MonoBehaviour
{


    private GameObject grabbedObject;
    private GrabManagerStatic GrabManagerStatic;

    private Transform selection;

    private void Start()
    {
        if (GrabManagerStatic == null)
        {
            GrabManagerStatic = FindAnyObjectByType<GrabManagerStatic>();
            if (GrabManagerStatic == null) return;
        }

    }


    void Update()
    {
        grabbedObject = GrabManagerStatic.WhatGrab();
        if (grabbedObject == null) return;

        Vector2 scroll = Input.mouseScrollDelta;

        if (scroll.y != 0)
        {
            Transform camTransform = Camera.main.transform;

           
            Vector3 objPos = grabbedObject.transform.position;

            // Direction entre la caméra et l'objet
            Vector3 direction = (objPos - camTransform.position).normalized;

            
            float currentDistance = Vector3.Distance(camTransform.position, objPos);

            
            float newDistance = currentDistance + scroll.y;

            // Limiter la distance entre 1 et 10 unités
            newDistance = Mathf.Clamp(newDistance, 2f, 20f);

           
            Vector3 newPosition = camTransform.position + direction * newDistance;

            // Mettre à jour le plan de grab
            GrabManagerStatic.SetPlane(new Plane(camTransform.forward, newPosition));
        }
    }




}
