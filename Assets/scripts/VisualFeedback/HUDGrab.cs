using TMPro;
using UnityEngine;

public class HUDGrab : MonoBehaviour
{
    private Transform targetHUD;

    private RaycastHit raycastHit;
    public float rayDistance = 100f;


    public GameObject HUD;

    public RectTransform rectTransform;

    public InteractFPS interactFPS;




    public void SetRayDistance(float distance)
    { this.rayDistance = distance; }

    public void OnActivate()
    {
        enabled = true;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition; // on crée un vecteur 2D qui prend la position de la souris sur l'écran
        Transform camera = Camera.main.transform;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        int mask = ~LayerMask.GetMask("Ignore Raycast", "SnapZone");

        if (Physics.Raycast(ray, out raycastHit, rayDistance, mask))
        {

            if (targetHUD != raycastHit.transform)
            {

                HUDGrabFunc(false);

                targetHUD = raycastHit.transform;
                if (targetHUD.CompareTag("Selectable"))
                {
                    
                    HUDGrabFunc(true);

                }
                else
                {
                    targetHUD = null;
       
                }
                ;
            }
            if (targetHUD != null) {
            rectTransform.position = Input.mousePosition + new Vector3(0, -50, 0); 
                }



        }
        else
        {
            HUDGrabFunc(false);
        }

    }



    

    private void HUDGrabFunc(bool activate)
    {
        if (activate)
        {

            HUD.SetActive(true);
            GameObject heldobject = interactFPS.WhatHeldObject();

            if (targetHUD.GetComponent<IInteractable>() != null)
            {

                HUD.transform.GetChild(0).GetComponent<TMP_Text>().text += targetHUD.GetComponent<IInteractable>().HUDInfo(heldobject);
            }

            if (targetHUD.GetComponent<IGrabbable>() != null)
            {

                HUD.transform.GetChild(0).GetComponent<TMP_Text>().text += targetHUD.GetComponent<IGrabbable>().HUDInfo(heldobject);
            }

            HUD.transform.GetChild(0).GetComponent<TMP_Text>().text += " [ClicGauche]";

        }
        else
        {
            HUD.SetActive(false);
            HUD.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        }

    }



    public void OnDeactivate()
    {
        HUD.SetActive(false);
      
        enabled = false;
    }

}
