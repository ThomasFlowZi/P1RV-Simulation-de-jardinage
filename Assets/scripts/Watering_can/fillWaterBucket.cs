using UnityEngine;

public class fillWaterBucket : MonoBehaviour
{


    public GameObject Bucket_filled;
    private GrabManagerStatic grabManager;
    public static bool estRempli = false;


    private void Start()
    {
        grabManager = FindAnyObjectByType<GrabManagerStatic>();
        
    }
    private void OnTriggerEnter(Collider other) {

        if (estRempli) return; 
        
        if (other.CompareTag("Eau")) { 
        Instantiate(Bucket_filled, transform.position, transform.rotation);
        estRempli = true;
        Destroy(gameObject);
        
        grabManager.setGrabbedObject(GameObject.Find("Bucket_filled(Clone)")); 


        }
    }

 }