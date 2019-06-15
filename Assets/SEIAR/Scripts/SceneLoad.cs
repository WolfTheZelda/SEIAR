using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    public string LoadScene = "SEIAR";
    public Text ProgressText;

    private int Progress;

    void Awake()
    {

        if (!PlayerPrefs.HasKey("LanguageNumber"))
        {

            switch (Application.systemLanguage)
            {

                case SystemLanguage.English:
                    {
                        PlayerPrefs.SetInt("LanguageNumber", 1);
                        break;
                    }

                case SystemLanguage.Portuguese:
                    {
                        PlayerPrefs.SetInt("LanguageNumber", 0);
                        break;
                    }

                /* 
                case SystemLanguage.Spanish:
                    {
                        PlayerPrefs.SetInt("LanguageNumber", 2);
                        break;
                    }

                case SystemLanguage.ChineseSimplified:
                    {
                        PlayerPrefs.SetInt("LanguageNumber", 3);
                        break;
                    } 
                */

                case SystemLanguage.Unknown:
                    {
                        PlayerPrefs.SetInt("LanguageNumber", 0);
                        break;
                    }

                default:
                    {
                        PlayerPrefs.SetInt("LanguageNumber", 0);
                        break;
                    }
            }

        }

    }

    void Start()
    {

        StartCoroutine("StartLoad");

    }

    void Update()
    {

        ProgressText.text = (Progress.ToString()) + "%";

    }

    IEnumerator StartLoad()
    {

        AsyncOperation Loading = SceneManager.LoadSceneAsync(LoadScene);

        while (!Loading.isDone)
        {

            Progress = (int)(Loading.progress * 100);
            yield return null;

        }

    }
}