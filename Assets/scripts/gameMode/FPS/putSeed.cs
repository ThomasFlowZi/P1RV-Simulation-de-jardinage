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

        if (player.transform.Find("seedBag") && !possedeGraine)

        {
            GameObject parent = transform.parent.gameObject;
            Instantiate(seed, parent.transform.Find("positionSeed").position, parent.transform.Find("positionSeed").rotation);
            possedeGraine = true;
        }
        
    }
}  