using UnityEngine;

public class GrabVisualFeedback : MonoBehaviour, IGrabbable
{


    public Mesh Mesh;
    public Material Material;
    public AudioClip grabSound;
    private AudioSource audioSource;

    private Renderer _renderer;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }

    public void OnGrabStart()
    {
        if (audioSource != null && grabSound != null)
            audioSource.PlayOneShot(grabSound);

        if (Mesh != null)
        {
            MeshFilter mf = GetComponent<MeshFilter>();
            Renderer r = GetComponent<Renderer>();
            mf.mesh = Mesh;
            r.material = Material;  
            Debug.Log("change le mesh");

        }

        
    }

    public void OnGrabEnd()
    {
       
    }
}