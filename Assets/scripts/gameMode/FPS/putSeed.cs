using UnityEditor;
using UnityEngine;

public class PutSeed : MonoBehaviour
{
    public GameObject seed;
    bool possedeGraine = false;
    GrabManagerStatic grabManager;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnMouseDown()
    {
       
        if (player.transform.Find("positionTete/main/seedBag") && !possedeGraine)

        {

            Debug.Log("coucou");
            GameObject parent = transform.root.gameObject;
            GameObject graine = Instantiate(seed, parent.transform.Find("positionSeed").position, parent.transform.Find("positionSeed").rotation);
            
            graine.transform.parent = parent.transform.Find("positionSeed");
            possedeGraine = true;
        }
        
    }
}  