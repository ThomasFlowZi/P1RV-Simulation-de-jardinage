using UnityEngine;

public class Turnip : MonoBehaviour, IInteractionHUDText
{
    public string HUDInfo(GameObject phi)
    {
        return "Récolter le radis";
    }
}
