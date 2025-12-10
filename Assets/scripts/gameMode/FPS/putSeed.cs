using UnityEditor;
using UnityEngine;

public class PutSeed : MonoBehaviour
{
    public GameObject seed;
    bool possedeGraine = false;
  
    public void OnActivate()
    {
       
        if (!possedeGraine)

        {
            
            GameObject parent = transform.root.gameObject;
            Transform PositionSeed = parent.transform.Find("positionSeed");
            GameObject graine = Instantiate(seed, PositionSeed.position, PositionSeed.rotation);
            
            graine.transform.parent = PositionSeed;
            possedeGraine = true;
        }
        
    }
}  