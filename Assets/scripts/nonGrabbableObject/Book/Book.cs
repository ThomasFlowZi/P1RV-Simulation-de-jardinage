using UnityEngine;
using UnityEngine.Events;

public class Book : MonoBehaviour, IInteractable, IInteractionHUDText
{
    public UnityEvent ReadBook;

    public string HUDInfo(GameObject playerHeldItem = null)
    {
        if (playerHeldItem == null) { return "Lire le livre"; }
        else { return ""; }
    }

    public void OnInteract(GameObject playerHeldItem)
    {
        if (playerHeldItem == null) {

            ReadBook.Invoke();
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
