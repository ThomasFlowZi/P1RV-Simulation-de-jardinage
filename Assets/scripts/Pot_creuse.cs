using UnityEngine;

public class Pot_creuse : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public AudioSource source;
    public AudioClip sfx;
    void Start()
    {
        source.PlayOneShot(sfx);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
