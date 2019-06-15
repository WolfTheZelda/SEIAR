using UnityEngine;
using System.Collections;
namespace SEIAR
{
    public class MenuButton : MonoBehaviour
    {

        static public bool MenuActive = true;

        public GameObject MenuUI;
        public GameObject ApplicationUI;
        private Animator MenuAnimator;        

        void Start()
        {
            MenuAnimator = MenuUI.GetComponent<Animator>();
        }

        public void StartButton()
        {                          
                ApplicationUI.SetActive(true);
                MenuAnimator.Play("LeftMenu");                
                StartCoroutine("LeftMenu");
                InformationButton.Touched = false;            
        }       
        IEnumerator LeftMenu ()
        {
            yield return new WaitForSecondsRealtime (0.75f);
            MenuUI.SetActive(false);            
        }
    }
}