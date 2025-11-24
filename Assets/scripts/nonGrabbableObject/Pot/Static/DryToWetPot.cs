using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class DryToWetPot : MonoBehaviour
{
    public bool estWet = false;
    public GameObject Pot_wet;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip sfx;

    public Color dry;
    public Color wet;
    void Update()
    {



    }
}
