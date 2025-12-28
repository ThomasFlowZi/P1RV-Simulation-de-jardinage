using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class DryToWetPot : MonoBehaviour
{
    private float estWet = 0f;   
    private bool isWatered = false;


    public void SetIsWatered(bool iw) {  isWatered = iw; }
    public bool GetIsWatered() { return isWatered; }

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
            estWet = (estWet < 1f) ? estWet + 0.005f : estWet;
            

            if (estWet == 1f)
            {
                source.PlayOneShot(sfx);

            }

        }
        gameObject.GetComponent<Renderer>().material.color = wet * estWet + dry * (1 - estWet);


    }

    public float getWet() {  return estWet; }
    public void setWet(float wet) { estWet = wet; }
}
