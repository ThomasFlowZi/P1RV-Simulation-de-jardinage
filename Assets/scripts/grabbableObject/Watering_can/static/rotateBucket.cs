using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class rotateBucket : MonoBehaviour, IGrabbable
{

    private Quaternion initRot ;

    private State state;
    private DryToWetPot dryToWetPot;
    private Water_level waterLevel;



    public float speedRot = 0.002f;

    public Material Material;
    public Color colorS;
    public Color colorE;

    private LineRenderer lineRenderer;
    private Transform hautBucket;
    private LayerMask layermask;

    void Start()
    {
        LayerMask layermask = ~LayerMask.GetMask("SnapZone");
        hautBucket = GameObject.Find("Wooden_Bucket/water/anchor_point_WF").transform;

        state = gameObject.GetComponent<State>();
        waterLevel = gameObject.GetComponent<Water_level>();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = Material;

        lineRenderer.startColor =  colorS;
        lineRenderer.endColor =  colorE;
        lineRenderer.enabled = false;
    }
   

    void Update()
    {

        lineRenderer.SetPosition(0, hautBucket.position);
        LayerMask layermask = ~LayerMask.GetMask("SnapZone");
        RaycastHit hit;
        Physics.Raycast(hautBucket.position, Vector3.down, out hit, 5f, layermask);
        lineRenderer.SetPosition(1, hit.point);

        if (state.GetSnap())
        {
            if (dryToWetPot == null )dryToWetPot = hit.collider.transform.root.Find("DiggedDirt").GetComponent<DryToWetPot>();
        }
        else
        {
            if (dryToWetPot != null) dryToWetPot.SetIsWatered(false);
            dryToWetPot = null;
        }



        if (Input.GetKey(KeyCode.R))
        {

            

            if (Quaternion.Angle(transform.localRotation, initRot) < 90)
            {

                

                transform.localRotation = transform.localRotation * Quaternion.Euler(0, 0, -speedRot);

                if (Quaternion.Angle(transform.localRotation, initRot) > 15)
                {
                    if ( Mathf.Cos((Quaternion.Angle( initRot, transform.localRotation) - 15) / 75f) < waterLevel.waterPercent){
                        waterLevel.waterPercent = Mathf.Cos((Quaternion.Angle(initRot, transform.localRotation) - 15) / 75f);
                        if (state.GetSnap()) dryToWetPot.SetIsWatered(true);
                        if (lineRenderer.enabled == false && !waterLevel.estVide) lineRenderer.enabled = true;
                    }

                    
                }
                if (Quaternion.Angle(transform.localRotation, initRot) > 89)
                {
                    if (lineRenderer.enabled == true) lineRenderer.enabled = false;
                    if (state.GetSnap()) dryToWetPot.SetIsWatered(false);
                }



            }



        }
        else
        {
            if (state.GetSnap()) dryToWetPot.SetIsWatered(false);
            if (lineRenderer.enabled == true) lineRenderer.enabled = false;

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
        if (lineRenderer.enabled == true) lineRenderer.enabled = false;
        if (dryToWetPot != null) dryToWetPot.SetIsWatered(false);
        dryToWetPot = null;
    }

   
}
