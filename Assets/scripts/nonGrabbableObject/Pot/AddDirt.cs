using UnityEngine;

public class AddDirt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnActivate()
    {
        if (!gameObject.transform.Find("DirtPile").gameObject.activeInHierarchy)
        {
            gameObject.transform.Find("DirtPile").gameObject.SetActive(true);

        }
        

    }
}
