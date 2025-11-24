using UnityEngine;
using UnityEngine.UIElements;

public class rotateBucket : MonoBehaviour, IGrabbable
{

    private Quaternion initRot = Quaternion.Euler(0, 0, 0);
    public GrabManagerStatic GrabManagerStatic;
    public DryToWetPot dryToWetPot;
    public float speedRot = 0.002f;


   

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {




                if (Quaternion.Angle(transform.localRotation, initRot) < 90)
                {

                transform.localRotation = transform.localRotation * Quaternion.Euler(0, 0, -speedRot);

                    if (GrabManagerStatic.Snap != null)
                    {
                        GameObject parent = GrabManagerStatic.Snap.transform.parent.gameObject;
                        GameObject terre = parent.transform.Find("terre").gameObject;

                        

                        terre.GetComponent<Renderer>().material.color = terre.GetComponent<DryToWetPot>().wet * Quaternion.Angle(transform.localRotation, initRot) / 90f + terre.GetComponent<DryToWetPot>().dry * (1 - Quaternion.Angle(transform.localRotation, initRot) / 90f);
                        
                    }
                }
                dryToWetPot.estWet = true; 




        }
        else
        {
            

                if (Quaternion.Angle(transform.localRotation, initRot) > 10)
                {
                    
                    transform.localRotation = transform.localRotation * Quaternion.Euler(0, 0, 2 * speedRot);
                }
                else
                {
                    transform.localRotation = initRot;
                }

            


        }


    }

    public void OnGrabStart()
    {
        initRot = transform.localRotation;
        enabled = true;
    }

    public void OnGrabEnd()
    {
        enabled = false;
    }
}
