using UnityEngine;

public class fillWaterBucket : MonoBehaviour
{


    public GameObject Wooden_Bucket_filled;
    public static bool estRempli = false;

    private void OnTriggerEnter(Collider other) {

        if (estRempli) return; 
        
        if (other.CompareTag("Eau")) { 
        Instantiate(Wooden_Bucket_filled, transform.position, transform.rotation);
        estRempli = true;
        Destroy(gameObject);
        }
    }

 }