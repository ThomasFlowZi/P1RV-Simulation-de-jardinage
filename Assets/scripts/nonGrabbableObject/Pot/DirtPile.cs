using UnityEngine;

public class DirtPile : MonoBehaviour, IInteractionHUDText
{
    public string HUDInfo(GameObject playerHeldItem = null)
    {
        if (playerHeldItem == null) { return "S'occuper du pot"; };
        return "";
    }
}
