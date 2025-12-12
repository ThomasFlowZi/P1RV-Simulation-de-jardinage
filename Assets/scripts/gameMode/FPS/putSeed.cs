using UnityEditor;
using UnityEngine;

public class PutSeed : MonoBehaviour
{
    public GameObject seed;
    bool possedeGraine = false;
  
    public void OnActivate()
    {
        Debug.Log("coucoucoucoiduoiuoiu");
        if (!possedeGraine)

        {
            
            Transform PositionSeed = transform.Find("positionSeed");
            GameObject graine = Instantiate(seed, PositionSeed.position, PositionSeed.rotation);
            
            graine.transform.parent = PositionSeed;
            possedeGraine = true;
        }
        
    }
}  