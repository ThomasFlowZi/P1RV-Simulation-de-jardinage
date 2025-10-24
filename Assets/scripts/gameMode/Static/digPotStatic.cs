using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


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
            Instantiate(pot_creuse, transform.position, transform.rotation);
            estCreuse = true;
            Debug.Log("Pot creusée avec : " + other.name);
            source.PlayOneShot(sfx);

            Destroy(gameObject);
           

        }
        



    }

}