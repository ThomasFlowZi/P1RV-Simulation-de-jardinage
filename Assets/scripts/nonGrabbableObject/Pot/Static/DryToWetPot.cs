using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class DryToWetPot : MonoBehaviour
{
    private float estWet = 0f;   
    private bool isWatered = false;

    public GameObject Pot_wet;




    public void SetIsWatered(bool iw) {  isWatered = iw; }

    [Header("Audio")]
    public AudioSource source;
    public AudioClip sfx;

    public Color dry;
    public Color wet;

    private void Start()
    {
    }
    void Update()
    {
        if (isWatered)
        {
            estWet = (estWet < 1f) ? estWet + 0.01f : estWet;
            gameObject.GetComponent<Renderer>().material.color = wet * estWet + dry * (1 - estWet);

            if (estWet == 1f)
            {
                source.PlayOneShot(sfx);

            }

        } 


    }

    public float getWet() {  return estWet; }
}
