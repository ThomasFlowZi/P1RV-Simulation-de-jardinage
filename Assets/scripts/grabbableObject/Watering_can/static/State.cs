using UnityEngine;

public class State : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool Snap = false;

    public bool GetSnap() { return Snap; }
    public void SetSnap(bool s) { Snap = s; }
}
