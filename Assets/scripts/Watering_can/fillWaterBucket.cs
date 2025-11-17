using UnityEngine;

public class fillWaterBucket : MonoBehaviour
{

    [Header("Audio")]
    public AudioSource source;
    public AudioClip sfx;


    GameObject eauBucket; 
    public static bool estRempli = false;


    private void Start()
    {
        eauBucket = GameObject.Find("Wooden_Bucket/Eau_bucket").gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (estRempli) return;

        if (other.CompareTag("Eau"))
        {
            eauBucket.GetComponent<Renderer>().enabled = true;
            source.PlayOneShot(sfx);
            estRempli = true;

        };
    }
}