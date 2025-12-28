using UnityEngine;

public class Dirt : MonoBehaviour, IInteractionHUDText
{

    public AudioSource audioSource;
    public AudioClip clip;
    public string HUDInfo(GameObject playerHeldItem = null)
    {
        if (playerHeldItem.layer == 7)
        {
            return "récuperer de la terre";

        }
        return "";
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dsdsfghdsflkgjhsfd");
        Transform root = other.transform.root;
        if (other.transform.gameObject.layer == 8 && !root.Find("DirtPile").gameObject.activeInHierarchy)//shovel tip
        {
            
            audioSource.PlayOneShot(clip);
            root.Find("DirtPile").gameObject.SetActive(true);

        }
        
    }
}
