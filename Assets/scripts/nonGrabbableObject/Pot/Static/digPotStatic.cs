using UnityEngine;

public class DigPotStatic : MonoBehaviour
{
    public static bool estCreuse = false;
    public GameObject pot_creuse;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip sfx;
    public float SpeedLimit = 20f;


    public ModeManager ModeManager;


    private void OnTriggerEnter(Collider other)
    {
        if (estCreuse) return;




        int layer = other.gameObject.layer;
        if (layer == 8) // ShovelTip
        {
            float speed = other.GetComponentInParent<Shovel>().GetSpeed();
       

            Debug.Log("Vitesse détectée : " + speed);

            if (speed > SpeedLimit)
            {
                GameObject potInstance = Instantiate(pot_creuse, transform.position, transform.rotation);
                estCreuse = true;
                source.PlayOneShot(sfx);

                ModeManager.SetCamStatic(potInstance.transform.Find("PotView").transform);
                ModeManager.SetMode(true);


                Destroy(gameObject);
            }
        }
    }
}