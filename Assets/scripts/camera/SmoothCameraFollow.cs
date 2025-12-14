using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // positiontete
    public float positionSmooth = 0.005f;
    public float rotationSmooth = 0.05f;

    private Vector3 velocity;

    void Update()
    {
        
        transform.SetPositionAndRotation(
            target.position,target.rotation);
    }
}