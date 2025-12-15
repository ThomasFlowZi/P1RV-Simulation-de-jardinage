using UnityEngine;

public interface IGrabbable
{
    void OnGrabStart();
    void OnGrabEnd();

    string HUDInfo(GameObject playerHeldItem = null);
}

public interface IInteractable
{
    void OnInteract(GameObject playerHeldItem);

    string HUDInfo(GameObject playerHeldItem = null);
}


