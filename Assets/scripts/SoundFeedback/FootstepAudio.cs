using Unity.VisualScripting;
using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips;

    public Moveplayer PlayerMovement;

    public float stepInterval = 0.6f; // temps entre pas
    public float minVelocity = 0.3f;

    private float stepTimer;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (PlayerMovement.actualSpeed == PlayerMovement.runSpeed) stepInterval = 0.3f;
        else stepInterval = 0.5f;
        if (rb.linearVelocity.magnitude > minVelocity && IsGrounded())
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioSource.PlayOneShot(clip);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}