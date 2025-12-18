using UnityEngine;

public class BookSFX : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip BookOpen;
    public AudioClip Swipe;


    public void PlayOpen()
    {
        AudioSource.PlayOneShot(BookOpen);
    }

    public void PlaySwipe()
    {
        AudioSource.PlayOneShot(Swipe);
    }
}
