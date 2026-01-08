using UnityEngine;

public class GrabVisualFeedback : MonoBehaviour, IGrabbable
{


    public Mesh Mesh;
    public Material Material;
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

        if (Mesh != null)
        {
            MeshFilter mf = GetComponent<MeshFilter>();
            Renderer r = GetComponent<Renderer>();

            MeshCollider mc = GetComponent<MeshCollider>();
            if (mc == null)
                mc = gameObject.AddComponent<MeshCollider>();


            transform.localPosition = initPos;
            // Applique le nouveau mesh
            mf.mesh = Mesh;
            mc.sharedMesh = Mesh;
            mc.convex = true;

            transform.localPosition = initPos;
            transform.localRotation = Quaternion.identity;

            r.material = Material;

        }

        if (gameObject.transform.root.GetComponent<GrowStep1>().twoStepsGrowth == false)
        {
            gameObject.transform.root.GetComponent<GrowStep1>().AssociatedPot.GetComponent<ResetPot>().ResetPotState();
        }
        else
        {
            gameObject.transform.root.GetComponent<GrowStep2>().AssociatedPot.GetComponent<ResetPot>().ResetPotState();


        }
    }

    public string HUDInfo(GameObject phi) //rien a faire ici
    {
        return "";
    }

    public void OnGrabEnd()
    {
       
    }
}