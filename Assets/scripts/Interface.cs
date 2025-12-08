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