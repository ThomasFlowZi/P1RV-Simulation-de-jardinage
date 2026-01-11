using UnityEngine;

public class TomatoGrab : MonoBehaviour
{


    
    public AudioClip grabSound;
    private AudioSource audioSource;

    private Vector3 initPos;

    private Renderer _renderer;

    void Start()
    {
        initPos = transform.localPosition;

        audioSource = GetComponent<AudioSource>();
        
}

    public void OnGrabStart()
    {
        if (audioSource != null && grabSound != null)
            audioSource.PlayOneShot(grabSound);

       transform.parent.SetParent(null,true);

    }
}