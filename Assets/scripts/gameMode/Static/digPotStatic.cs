using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class digPotStatic : MonoBehaviour
{
    public static bool estCreuse = false;
    public GameObject pot_creuse;

    private void OnTriggerEnter(Collider other)
    {
        if (!estCreuse)
        {
            Instantiate(pot_creuse, transform.position, transform.rotation);
            estCreuse = true;
            Debug.Log("Pot creusée avec : " + other.name);
            Destroy(gameObject);
        }
    }
}
