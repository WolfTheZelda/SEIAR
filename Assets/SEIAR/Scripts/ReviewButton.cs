using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace SEIAR
{
    public class ReviewButton : MonoBehaviour
    {

        static public bool Online;

        public GameObject[] StarsButtons;

        public GameObject[] StarsYellowButtons;

        public bool Send;
        public bool Permission;
        public bool ApprovalBool;

        public string UserName, UserAge, UserStars, UserComment;

        private int Possibility;

        public Text Status;

        public GameObject ReviewUI;
        public GameObject ReviewUIFrontground;

        public InputField UserNameUI;
        public InputField UserAgeUI;
        public InputField CommentUI;

        private Animator Animation;
        private LanguageButton Language;

        public string[] PleaseStatus;
        public string[] InternalErrorStatus;
        public string[] SucessStatus;
        public string[] ExternalErrorStatus;
        public string[] FieldErrorStatus;

        void Start()
        {
            Language = FindObjectOfType<LanguageButton>();

            Animation = ReviewUIFrontground.GetComponent<Animator>();

            StartCoroutine("CheckInternet");

            Possibility = UnityEngine.Random.Range(0, 10);

        }

        void FixedUpdate()
        {

            if (!PlayerPrefs.HasKey("Sended") && MenuButton.MenuActive == false && Online == true && ReviewUI != null && Possibility == 5)
            {

                ReviewButtonUI();

            }

            if (UserNameUI.text != "" && UserAgeUI.text != "" && CommentUI.text != "" && UserStars != "")
            {

                Permission = true;

                UserName = UserNameUI.text;
                UserAge = UserAgeUI.text;
                UserComment = CommentUI.text;

            }
            else
            {

                Permission = false;

            }

        }

        IEnumerator CheckInternet()
        {

            WWW Check = new WWW("https://tecwolf.com.br/");
            yield return Check;

            if (Check.error != null)
            {

                Online = false;

            }
            else
            {

                Online = true;

            }

            StopCoroutine("CheckInternet");

        }

        IEnumerator Review()
        {

            UserName = Base64Encode(UserName);
            UserAge = Base64Encode(UserAge);
            UserStars = Base64Encode(UserStars);
            UserComment = Base64Encode(UserComment);
            
			string UrlComplete = "https://tecwolf.com.br/assets/php/seiar-reviews.php" + "?n=" + UserName + "&a=" + UserAge + "&s=" + UserStars + "&c=" + UserComment;

            WWW SendReview = new WWW(UrlComplete);

            yield return SendReview;

            if (SendReview.error != null)
            {

                if (Status != null)
                {

                    Status.text = InternalErrorStatus[Language.LanguageNumber];

                }

            }

            if (SendReview.error == null)
            {

                if (SendReview.text == ("SENT"))
                {

                    if (Status != null)
                    {

                        PlayerPrefs.SetString("Sended", "true");

                        Status.text = SucessStatus[Language.LanguageNumber];

                    }

                }

                if (SendReview.text == ("ERROR #1"))
                {

                    if (Status != null)
                    {

                        Status.text = ExternalErrorStatus[Language.LanguageNumber];

                    }
                }

				if (SendReview.text == ("ERROR #2"))
                {

                    if (Status != null)
                    {

                        Status.text = ExternalErrorStatus[Language.LanguageNumber];

                    }
                }
            }

            StopCoroutine("Review");

        }

        public void StarOneUI()
        {

            if (UserStars == "1")
            {

                UserStars = "0";

                StarsReset();

            }
            else
            {
                UserStars = "1";

                StarsReset();

            }

        }

        public void StarTwoUI()
        {

            UserStars = "2";

            StarsReset();

        }

        public void StarThreeUI()
        {

            UserStars = "3";

            StarsReset();

        }

        public void StarFourUI()
        {

            UserStars = "4";

            StarsReset();

        }

        public void StarFiveUI()
        {

            UserStars = "5";

            StarsReset();

        }

        public void SendUI()
        {

            Send = true;

            if (Send == true && Permission == true)
            {

                StartCoroutine("Review");

                Send = false;
                Permission = false;

            }
            else
            {

                Status.text = FieldErrorStatus[Language.LanguageNumber];

                Send = false;
                Permission = false;

            }

        }

        public void ExitUI()
        {

            Animation.Play("DownPopup");
            StartCoroutine("DownPopup");

            InformationButton.Touched = false;
            
            UserNameUI.text = "";
            UserAgeUI.text = "";
            CommentUI.text = "";

            UserStars = "0";

            StarsReset();

        }

        public void ReviewButtonUI()
        {

            RandomExperiment.ExperimentObject[RandomExperiment.ExperimentNumber].SetActive(false);

            ReviewUI.SetActive(true);

            Animation.Play("UpPopup");

            Possibility = 5;

            UserStars = "0";

            InformationButton.Touched = true;

            Status.text = PleaseStatus[Language.LanguageNumber];

        }

        public void StarsReset()
        {

            foreach (GameObject Go in StarsButtons)
            {
                Go.SetActive(true);
            }

            foreach (GameObject Go in StarsYellowButtons)
            {
                Go.SetActive(false);
            }

            for (int i = 0; i < Convert.ToInt32(UserStars); i++)
            {
                StarsButtons[i].SetActive(false);
                StarsYellowButtons[i].SetActive(true);
            }

        }

        public string Base64Encode(string PlainText)
        {

            var PlainTextBytes = System.Text.Encoding.UTF8.GetBytes(PlainText);
            return Convert.ToBase64String(PlainTextBytes);

        }

        IEnumerator DownPopup()
        {

            yield return new WaitForSecondsRealtime(1.0f);

            ReviewUI.SetActive(false);
            Online = false;

            if (MenuButton.MenuActive == true)
            {

                RandomExperiment.ExperimentObject[RandomExperiment.ExperimentNumber].SetActive(true);

            }

            Animation.Play("UpPopup", -1, Time.deltaTime);
            StopCoroutine("DownPopup");

        }        
    }
}