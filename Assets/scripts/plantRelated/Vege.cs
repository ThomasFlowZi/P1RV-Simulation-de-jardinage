using UnityEngine;
using UnityEngine.Events;

public class Vege : MonoBehaviour, IInteractionHUDText
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
}
