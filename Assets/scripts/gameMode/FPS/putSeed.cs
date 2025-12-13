using UnityEditor;
using UnityEngine;

public class PutSeed : MonoBehaviour
{
    public GameObject seed;
    
  
    public void OnActivate()
    {
        Transform PositionSeed = transform.Find("positionSeed");
        GameObject graine = Instantiate(seed, PositionSeed.position, PositionSeed.rotation);

        graine.transform.parent = PositionSeed;
       


    }
}  