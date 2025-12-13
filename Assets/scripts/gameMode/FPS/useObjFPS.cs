using UnityEditor.Rendering;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class UseObjFPS : MonoBehaviour
{
    public float dureeAnim = 60f;
    private float frameAnim = 0f;
    private bool Animation  = false;

    Transform  main;

    void Start()
    {
        
        main = GameObject.Find("player").transform.Find("positionTete/main");

    }

    public void StartAnimation()
    {
        Animation = true;
        StartCoroutine(PlayAnimation());
        
    }

    public void StartInteraction()
    {
        // Si tu veux ignorer l’event et demander directement au manager :
        
        Transform target = FindAnyObjectByType<InteractFPS>().WhatTarget();
        Debug.Log(target);

        Interact(target);
    }




    public IEnumerator PlayAnimation()
    {
        Transform camera = Camera.main.transform;
        Transform selection = main.transform.GetChild(0);
        Animation = true;

        while (true)
        {

            frameAnim += 1f;


            // Si on a fini l'aller on repart dans l'autre sens
            if (frameAnim > dureeAnim)
                frameAnim = -dureeAnim;

            // Quand on revient pile à 0  fin de l’animation
            if (frameAnim == 0)
            {
                Animation = false;
                yield break;
            }


            selection.RotateAround(
                main.position,
                camera.right,
                Mathf.Sign(frameAnim) * (90f / dureeAnim)
            );

            

            yield return null;
        }
    }

    private void Interact(Transform target) 
    {
        Transform camera = Camera.main.transform;
        GameObject heldObject;

        if (main.transform.childCount > 0)
        {

            
            heldObject = main.transform.GetChild(0).gameObject;
        }
        else
        {
            heldObject = null;
        }
        Transform root = target.root;
        if (heldObject != null) Debug.Log("held object : " + heldObject.name);
        if (root.GetComponent<IInteractable>() != null) root.GetComponent<IInteractable>().OnInteract(heldObject);



    }

}