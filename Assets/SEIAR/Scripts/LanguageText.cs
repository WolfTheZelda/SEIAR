using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


namespace SEIAR
{

    public class LanguageText : MonoBehaviour
    {

        private Text LanguageUI;

        [TextArea(1, 5)]
        public string[] LanguageTexts;

        void Start()
        {

            if (GetComponent<Text>() != null)
            {
                LanguageUI = GetComponent<Text>();
            }

        }

        void FixedUpdate()
        {

            if (LanguageTexts[PlayerPrefs.GetInt("LanguageNumber")] == null)
            {
                PlayerPrefs.SetInt("LanguageNumber", 0);
            }

            if (GetComponent<Text>() != null)
            {
                LanguageUI.text = LanguageTexts[PlayerPrefs.GetInt("LanguageNumber")];
            }

        }

    }

}
