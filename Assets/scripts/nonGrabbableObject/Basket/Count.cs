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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text = HUD.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("deposé");
        if (other.transform.root.gameObject.GetComponent<Vege>().nameVeg == "Radis") //turnip
        {
            Debug.Log("radis deposé");
            count[0] += 1;
            lastVeg = "Radis";
            StartCoroutine(HUDCounter());
        }
        if (other.transform.root.gameObject.GetComponent<Vege>().nameVeg == "Tomate") //tomato
        {
            count[1] += 1;
            lastVeg = "Tomate";
        }

    }

    private IEnumerator HUDCounter()
    {
        HUD.SetActive(true);

        Text.text = "+1 " + lastVeg + "\n";

        yield return new WaitForSeconds(3);

        HUD.SetActive(false);
        yield break;
    }
}
