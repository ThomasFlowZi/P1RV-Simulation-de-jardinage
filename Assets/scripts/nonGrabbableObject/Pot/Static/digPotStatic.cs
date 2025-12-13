using UnityEngine;

public class DigPotStatic : MonoBehaviour
{
    public static bool estCreuse = false;

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
                estCreuse = true;


                GameObject diggedDirt = transform.Find("DiggedDirt").gameObject;
                
                GameObject SnapZone = transform.Find("SnapZone").gameObject;

                GameObject Dirt = transform.Find("Dirt").gameObject;
                


                source.PlayOneShot(sfx);

                diggedDirt.SetActive(true);

                SnapZone.SetActive(true);

                Dirt.SetActive(false);

                /*
                ModeManager.SetCamStatic(potInstance.transform.Find("PotView").transform);
                ModeManager.SetMode(true);*/


                //Destroy(gameObject);
            }
        }
    }
}