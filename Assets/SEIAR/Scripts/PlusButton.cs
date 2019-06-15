using UnityEngine;
using System.Collections;

public class PlusButton : MonoBehaviour {
    
    private bool Touched;
    public GameObject MenuUI;
    public GameObject ImagePlus;
    public GameObject ImageMinus;
    public GameObject[] MenuButtons;
    public Animator MenuAnimator;       
    
	public void ButtonUI () {

        Touched = !Touched;

        if (Touched)
        {
            MenuButtons[0].SetActive(true);
            ImagePlus.SetActive(false);
            ImageMinus.SetActive(true);
            MenuAnimator.Play("Up");      
            
        }
        else if (!Touched)
        {
            MenuButtons[0].SetActive(false);
            ImageMinus.SetActive(false);
            ImagePlus.SetActive(true);
            MenuAnimator.Play("Down");

        }
    }
}