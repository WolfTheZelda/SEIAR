using UnityEngine;
using UnityEngine.UI;

namespace SEIAR
{
    public class LanguageButton : MonoBehaviour
    {       
        public int LanguageNumber = 0;

        private bool LanguageMin, LanguageMax;
        private GameObject LanguageIcon;
        private int LanguageTotal;

        void Awake()
        {
            LanguageNumber = PlayerPrefs.GetInt("LanguageNumber");

            LanguageMin = true;

            LanguageIcon = GameObject.FindGameObjectWithTag("LanguageIcon");

            LanguageTotal = LanguageIcon.transform.childCount - 1;

            if (LanguageNumber > LanguageTotal)
            {
                LanguageNumber = 0;
            }
        }

        void Start()
        {
            ChangeLanguageIcon();
        }

        void ChangeLanguageIcon()
        {
            for (int i = 0; i <= LanguageTotal; i++)
            {

                if (i == LanguageNumber)
                {

                    LanguageIcon.transform.GetChild(i).gameObject.SetActive(true);

                }
                else
                {

                    LanguageIcon.transform.GetChild(i).gameObject.SetActive(false);

                }

            }
        }

        public void ChangeLanguage()
        {
            // Tentativa de evitar que o usuário veja a tradução acontecer em tempo real.  
            /*   
            LanguageText[] AllText = FindObjectsOfType<LanguageText>();
            foreach (LanguageText Go in AllText)
            {
                Text[] TextComponent = Go.transform.gameObject.GetComponents<Text>();
                foreach (Text Go_1 in TextComponent)
                {
                    Go_1.text = "";
                }
            }
            */

            if (LanguageMax && LanguageNumber > 0 && LanguageNumber != 0)
            {

                LanguageNumber = LanguageNumber - 1;
                PlayerPrefs.SetInt("LanguageNumber", LanguageNumber);

            }
            else if (LanguageMin && LanguageNumber < LanguageTotal && LanguageNumber != LanguageTotal)
            {

                LanguageNumber = LanguageNumber + 1;
                PlayerPrefs.SetInt("LanguageNumber", LanguageNumber);

            }

            if (LanguageNumber == 0)
            {

                LanguageMin = true;
                LanguageMax = false;

            }
            else if (LanguageNumber == LanguageTotal)
            {
                LanguageMin = false;
                LanguageMax = true;
            }

            ChangeLanguageIcon();
        }
    }
}