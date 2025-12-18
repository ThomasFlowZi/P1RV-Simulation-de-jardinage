using UnityEngine;

public class Moveplayer : MonoBehaviour
{
    public float playerMoveSpeed = 5f;
    public float addSpeed = 0f;

    private Rigidbody rb;
    private Transform cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        cam = Camera.main.transform;

    }

    void FixedUpdate()
    {
        Vector3 forward = new Vector3(cam.forward.x, 0, cam.forward.z).normalized;
        Vector3 right = new Vector3(cam.right.x, 0, cam.right.z).normalized;

        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) move += forward;
        if (Input.GetKey(KeyCode.S)) move -= forward;
        if (Input.GetKey(KeyCode.D)) move += right;
        if (Input.GetKey(KeyCode.A)) move -= right;

        if (Input.GetKey(KeyCode.LeftShift)) addSpeed = 3f;
        else addSpeed = 0f;

        rb.linearVelocity = move.normalized * (playerMoveSpeed + addSpeed);
    }
}