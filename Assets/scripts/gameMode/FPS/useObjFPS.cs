using UnityEngine;

public class UseObjFPS : MonoBehaviour
{
    private Animator animator;
    private Transform main;

    void Start()
    {
        main = GameObject.Find("player").transform.Find("positionTete/main");
        animator = main.GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        if (animator != null)
        {
            Debug.Log("coucou");
            animator.SetTrigger("Hit");
        }
    }

    public void StartInteraction()
    {
        Transform target = FindAnyObjectByType<InteractFPS>().WhatTarget();
        Debug.Log(target);

        Interact(target);
    }

    private void Interact(Transform target)
    {
        GameObject heldObject = FindAnyObjectByType<InteractFPS>().WhatHeldObject();
        Transform root = target.root;

        if (heldObject != null) Debug.Log("held object : " + heldObject.name);
        if (root.GetComponent<IInteractable>() != null)
            root.GetComponent<IInteractable>().OnInteract(heldObject);
    }
}