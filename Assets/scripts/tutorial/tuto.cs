using TMPro;
using UnityEngine;

public class tuto : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TMP_Text text;
    public GameObject digged;
    private bool isStatic;
    private float waterLevel;
    public GameObject leaveTutorial;
    void Start()
    {
        text.text = "Bonjour nouveau joueur, je vais t'apprendre les mécaniques les moins faciles de Farming Lands, pour cela utilise le clic gauche de la souris sur le pot";
        
        waterLevel = digged.GetComponent<DryToWetPot>().getWet();

        
    }

    // Update is called once per frame
    void Update()
    {
        isStatic = FindAnyObjectByType<ModeManager>().IsStatic();
        if (isStatic)
        {
            text.text = "Bravo ! maintenant pour apprendre a creuser, clic gauche pour prendre la pelle, utilise la molette pour gérer la distance de la pelle et frappe le pot de haut en bas jusqu'a l'avoir creusé.";
        }
        if (digged.activeInHierarchy)
        {
            text.text = "Bravo ! tu sais désormais comment creuser la terre du pot, maintenant lache la pelle (clic droit), prend le seau (clic gauche), et remplis le en l'immergeant dans le réservoir. (utilise la molette !) \n " +
                "Une fois cela fait, place le au dessus du seau et maintient R pour verser l'eau. ";
            
        }
        waterLevel = digged.GetComponent<DryToWetPot>().getWet();
        if (waterLevel > 0.9)
        {
            text.text = "Bravo ! tu maitrises les deux mécaniques les plus complexes, les mécaniques restantes se réalisent a la première personne en prenant l'objet nécessaire en main et en cliquant sur l'objet cible. \n" +
                "Utiliser le sac de graine sur le pot pour planter une graine. \n " +
                "Utilser la pelle sur le par-terre de terre pour prendre de la terre.\n" +
                "Utiliser la pelle remplit de terre sur le pot pour recouvrir la graine.";
            leaveTutorial.SetActive(true);

        }

        
    }
}
