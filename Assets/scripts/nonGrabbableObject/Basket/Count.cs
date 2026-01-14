using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Count : MonoBehaviour
{
    public GameObject HUD;
    private TMP_Text Text;
    private string lastVeg;
    public List<int> count;

    public GameObject successUI;
    private TMP_Text textSuccess;
    public int success1, success2;
    private bool suc1, suc2;    
    public GameObject tomatoSeedBag;
    public GameObject secondPot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text = HUD.transform.GetChild(0).GetComponent<TMP_Text>();
        textSuccess = successUI.transform.Find("textSuccess").GetComponent<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("deposé");
        if (other.transform.root.gameObject.GetComponent<Vege>().nameVeg == "le Radis") //turnip
        {
            Debug.Log("radis deposé");
            count[0] += 1;
            lastVeg = "Radis";
            StartCoroutine(HUDCounter());
        }
        if (other.transform.root.gameObject.GetComponent<Vege>().nameVeg == "la Tomate") //tomato
        {
            count[1] += 1;
            lastVeg = "Tomate";
            StartCoroutine(HUDCounter());
        }

    }

    private IEnumerator HUDCounter()
    {
        HUD.SetActive(true);

        Text.text = "+1 " + lastVeg + "\n";

        yield return new WaitForSeconds(3);
        HUD.SetActive(false);


        if (count[0] == success1 && !suc1 ) 
        {
            successUI.SetActive(true);
            textSuccess.text = "Succès 1 obtenu ! \n Vous pouvez désormais planter des tomates";
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
            suc1 = true;
            tomatoSeedBag.SetActive(true);

        }
        else if (count[1] == success2 && !suc2)
        {
            successUI.SetActive(true);
            textSuccess.text = "Succès 2 obtenu ! \n Vous avez désormais deux pots a disposition";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            suc2 = true;
            secondPot.SetActive(true);
        }

        



        yield break;
    }
}
