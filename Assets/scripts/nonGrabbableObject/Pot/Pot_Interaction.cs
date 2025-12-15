using UnityEngine;
using UnityEngine.Events;

public class Pot_Interaction : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent handFreeInteraction;
    public UnityEvent seedBagInteraction;


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

        }

        

    }

    public string HUDInfo(GameObject phi)
    {
        if (phi == null) { return "S'occuper du pot"; }
        else if (phi.layer==12)
        {
            return "Placer une graine";
        }
        else { return "Pas d'interaction"; }
    }

    public bool GetOccupied() { return Occupied; }
    
}
