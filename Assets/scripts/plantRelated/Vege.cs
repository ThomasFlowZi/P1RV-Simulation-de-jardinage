using UnityEngine;

public class Vege : MonoBehaviour, IInteractionHUDText
{
    public string nameVeg;
    public string HUDInfo(GameObject phi)
    {
        return "Récolter le "+ nameVeg;
    }
}
