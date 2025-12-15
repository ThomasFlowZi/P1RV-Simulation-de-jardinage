using UnityEngine;

public interface IGrabbable
{
    void OnGrabStart();
    void OnGrabEnd();

    
}

public interface IInteractable
{
    void OnInteract(GameObject playerHeldItem);

}

public interface IInteractionHUDText
{
    string HUDInfo(GameObject playerHeldItem = null);
}

