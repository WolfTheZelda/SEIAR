using UnityEngine;

public class TouchDetector : MonoBehaviour {

    public static bool Touched;

   public void OnMouseEnter()
    {
        Touched = true;
    }

    public void OnMouseExit()
    {
        Touched = false;
    }
}
