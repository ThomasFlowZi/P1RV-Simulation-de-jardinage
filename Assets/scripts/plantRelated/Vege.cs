using UnityEngine;
using UnityEngine.Events;

public class Vege : MonoBehaviour, IInteractionHUDText, IGrabbable
{
    public string nameVeg;
    public UnityEvent grab;

    public void StartGrab()
    {
        grab.Invoke();
    }
    public string HUDInfo(GameObject phi)
    {
        return "Récolter le "+ nameVeg;
    }

    public void OnGrabStart()
    {
        StartGrab();
    }

    public void OnGrabEnd()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
