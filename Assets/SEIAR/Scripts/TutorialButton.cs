using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{

    private int TutorialPage, Contents;
    public GameObject TutorialUI, NextButtonUI, PreviosButtonUI, ExitStartButtonUI, ExitEndButtonUI;
    public Animator Frontground;

    // Use this for initialization
    void Start()
    {

        if (!PlayerPrefs.HasKey("Tutorial") && !IfUnityEditor.UnityEditor)
        {

            TutorialPage = 0;
            TutorialUI.SetActive(true);
            Frontground.Play("UpPopup");

            if (TutorialUI.activeSelf == true)
            {
                RandomExperiment.ExperimentObject[RandomExperiment.ExperimentNumber].SetActive(false);
            }

        }

        Contents = (TutorialUI.transform.GetChild(1).transform.GetChild(0).childCount) - 1;

    }

    void ChangePage()
    {

        for (int i = 0; i <= Contents; i++)
        {
            if (i == TutorialPage)
            {
                TutorialUI.transform.GetChild(1).transform.GetChild(0).transform.GetChild(i).transform.gameObject.SetActive(true);
            } else
            {
                TutorialUI.transform.GetChild(1).transform.GetChild(0).transform.GetChild(i).transform.gameObject.SetActive(false);
            }
        }
        
        if (TutorialPage <= 0)
        {

            NextButtonUI.SetActive(true);
            PreviosButtonUI.SetActive(false);
            ExitStartButtonUI.SetActive(true);
            ExitEndButtonUI.SetActive(false);

        }
        else if (TutorialPage < Contents)
        {

            NextButtonUI.SetActive(true);
            PreviosButtonUI.SetActive(true);
            ExitStartButtonUI.SetActive(false);
            ExitEndButtonUI.SetActive(false);

        }
        else if (TutorialPage >= Contents)
        {

            NextButtonUI.SetActive(false);
            PreviosButtonUI.SetActive(true);
            ExitStartButtonUI.SetActive(false);
            ExitEndButtonUI.SetActive(true);

        }
    }

    public void NextUI()
    {

        TutorialPage = TutorialPage + 1;

        ChangePage();

    }

    public void PreviousUI()
    {

        TutorialPage = TutorialPage - 1;        

        ChangePage();

    }

    public void ExitUI()
    {

        PlayerPrefs.SetString("Tutorial", "true");
        Frontground.Play("DownPopup");
        StartCoroutine("DownPopup");


    }

    IEnumerator DownPopup()
    {

        yield return new WaitForSecondsRealtime(1.0f);

        TutorialUI.SetActive(false);
        RandomExperiment.ExperimentObject[RandomExperiment.ExperimentNumber].SetActive(true);

        Frontground.Play("UpPopup", -1, Time.deltaTime);
        StopCoroutine("DownPopup");

    }

    public void OpenUI()
    {
        TutorialUI.SetActive(true);
        Frontground.Play("UpPopup");
        RandomExperiment.ExperimentObject[RandomExperiment.ExperimentNumber].SetActive(false);
        TutorialPage = 0;
        ChangePage();
    }
}
