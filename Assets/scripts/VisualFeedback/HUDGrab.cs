using TMPro;
using UnityEngine;

public class HUDGrabFPS : MonoBehaviour
{
    private Transform targetHUD;

    private RaycastHit raycastHit;
    public float rayDistance = 100f;


    public GameObject lastHeldObject =null;

    public GameObject HUD;

    public RectTransform rectTransform;

    public InteractFPS interactFPS;

    private TMP_Text Text;




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
        int mask = ~LayerMask.GetMask("Ignore Raycast", "SnapZone", "IgnoreFPS");

        GameObject heldobject = interactFPS.WhatHeldObject();

        if (Physics.Raycast(ray, out raycastHit, rayDistance, mask))
        {

            if (targetHUD != raycastHit.transform || heldobject != lastHeldObject )
            {

                HUDGrabFunc(false);
                lastHeldObject = heldobject;
                targetHUD = raycastHit.transform;
                if (targetHUD.CompareTag("Selectable") || targetHUD.CompareTag("Interactable"))
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
            GameObject targetHUDroot = targetHUD.transform.root.gameObject;

            Text = HUD.transform.GetChild(0).GetComponent<TMP_Text>();

            if (targetHUDroot.GetComponentInChildren<IInteractionHUDText>() != null)
            {

                Text.text += targetHUDroot.GetComponentInChildren<IInteractionHUDText>().HUDInfo(heldobject);
            }

            if (Text.text == "") { Text.text = "Interaction impossible"; }
            else if (Text.text[Text.text.Length - 1] != '!') 
                { Text.text += " [ClicGauche]"; }

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
