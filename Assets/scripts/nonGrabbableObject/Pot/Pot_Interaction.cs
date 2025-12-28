using UnityEngine;
using UnityEngine.Events;

public class Pot_Interaction : MonoBehaviour, IInteractable, IInteractionHUDText
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent handFreeInteraction;
    public UnityEvent seedBagInteraction;
    public UnityEvent addDirtInteraction;


    bool Occupied = false;

    private void Update()
    {
        if (transform.Find("positionSeed").childCount == 0)
        {
            Occupied = false;
        }
    }


    public void OnInteract(GameObject playerHeldObject)
    {
        if (playerHeldObject == null)
        {
            handFreeInteraction.Invoke();
        }
          
        if (transform.Find("DiggedDirt").gameObject.activeInHierarchy)
        {
            if (playerHeldObject?.layer == 12)
            {
                if (!Occupied && transform.Find("DiggedDirt").gameObject.activeInHierarchy)
                {
                    Occupied = true;
                    seedBagInteraction.Invoke();
                }
            }
            if (playerHeldObject?.layer == 7 && playerHeldObject.transform.root.Find("DirtPile").gameObject.activeInHierarchy)
            {
                addDirtInteraction.Invoke();
            }

        }

        

    }

    public string HUDInfo(GameObject phi)
    {
        if (phi == null) { return "S'occuper du pot"; }
        else if (phi.layer==12 && transform.Find("DiggedDirt").gameObject.activeInHierarchy)
        {
            return "Placer une graine";
        }
        else if(phi.layer == 7 && transform.Find("DiggedDirt").gameObject.activeInHierarchy && phi.transform.root.Find("DirtPile").gameObject.activeInHierarchy)
        {
            return "Deposer la terre dans le pot";
        }
        else { return ""; }
    }

    public bool GetOccupied() { return Occupied; }
    
}
