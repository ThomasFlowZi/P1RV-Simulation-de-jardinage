using UnityEngine;
using UnityEngine.Events;

public class Pot_creuse : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent HandFreeInteraction;
    public UnityEvent seedBagInteraction;
    public AudioSource source;
    public AudioClip sfx;
    void Start()
    {
        source.PlayOneShot(sfx);
    }

    public void OnInteract(GameObject playerHeldObject)
    {
        
        if (playerHeldObject == null)
        {
            HandFreeInteraction.Invoke();
        }
        if (playerHeldObject.layer == 12) {

            seedBagInteraction.Invoke();
        }

    }
 
    
}
