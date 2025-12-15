using UnityEngine;

public class SeedBag : MonoBehaviour, IGrabbable, IInteractionHUDText
{
    public void OnGrabStart() { }
    public void OnGrabEnd() { }
    public string HUDInfo(GameObject gameObject)
    {
        return "prendre Sac de Graine";
    }

}
