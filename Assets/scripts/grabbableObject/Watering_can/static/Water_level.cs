using UnityEngine;

public class Water_level : MonoBehaviour
{
    Material material;

    Transform hautBucket; // point de référence dans le seau
    Transform fondBucket;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip sfx;

    GameObject eauBucket;
    

    public bool estFull = false;

    float seuil; // valeur courante envoyée au shader

    private void Start()
    {
        hautBucket = GameObject.Find("Wooden_Bucket/water/anchor_point_WL").transform;
        fondBucket = GameObject.Find("Wooden_Bucket/water").transform;

        eauBucket = GameObject.Find("Wooden_Bucket/water/water_mesh").gameObject;
        
        material = eauBucket.GetComponent<Renderer>().material;

        eauBucket.GetComponent<Renderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Eau"))
        {
            if (!estFull)
            {
                eauBucket.GetComponent<Renderer>().enabled = true;
                source.PlayOneShot(sfx);
                estFull = true;

                // on initialise le niveau d'eau
                seuil = hautBucket.position.y - fondBucket.position.y;
                material.SetFloat("_seuil", seuil);
            }
        }
    }

    private void Update()
    {

        float newSeuil = hautBucket.position.y - fondBucket.position.y;
        

        // limite la montée d'eau au maximum (évite de descendre de façon bizarre)
        if (newSeuil < seuil)
        {
            if (newSeuil < -0.11f) {
                eauBucket.GetComponent<Renderer>().enabled = false;
                
            }
            else
            {

                if((newSeuil - seuil) < -0.0001) estFull = false;  //si on commence vraiment a vider alors il n'est plus plein

                seuil = newSeuil;
                material.SetFloat("_seuil", seuil);
            }
            
        }
    }
}