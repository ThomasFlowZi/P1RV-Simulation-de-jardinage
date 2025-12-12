using UnityEngine;
using UnityEngine.Events;

public class Pot_plein : MonoBehaviour,IInteractable
{
    public UnityEvent HandFreeInteraction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnInteract(GameObject playerHeldObject)
    {

        if (playerHeldObject == null)
        {
            HandFreeInteraction.Invoke();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
