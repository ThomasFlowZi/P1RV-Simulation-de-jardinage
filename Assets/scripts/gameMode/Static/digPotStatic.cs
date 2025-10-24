using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class digPotStatic : MonoBehaviour
{
    public static bool estCreuse = false;
    public GameObject pot_creuse;
    [Header("Audio")]
    public AudioSource source;  
    public AudioClip sfx;

    private void OnTriggerEnter(Collider other)
    {
        if (!estCreuse)
        {
            int layer = other.gameObject.layer;
            if (layer == 7)
            {
                Instantiate(pot_creuse, transform.position, transform.rotation);
                estCreuse = true;
                Debug.Log("Pot creusée avec : " + other.name);
                source.PlayOneShot(sfx);

                Destroy(gameObject);
            }

        }
        



    }

}