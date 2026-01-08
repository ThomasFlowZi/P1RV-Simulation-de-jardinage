using UnityEngine;

public class ResetPot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetPotState();
        
    }

    // Update is called once per frame
    public void ResetPotState()
    {

        gameObject.transform.Find("DiggedDirt").gameObject.SetActive(false);
        gameObject.transform.Find("SnapZone").gameObject.SetActive(false);
        
        gameObject.transform.Find("Dirt").gameObject.SetActive(true);
        gameObject.transform.Find("DirtPile").gameObject.SetActive(false);
        gameObject.GetComponent<DigPotStatic>().estCreuse = false;


    }
}
