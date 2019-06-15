using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace SEIAR
{
    public class InformationButton : MonoBehaviour
    {

        static public bool Touched = false;
        static public bool ExitInformation;

        [Header("Experimentos")]
        public GameObject ExperimentObject;

        [Header("Dados")]
        public TextAsset[] TextContent;

        [Header("Realidade Virtual")]
        public float TimerLimit = 1.0f;
        private float Timer;

        private Ray RayCamera;
        private bool MouseEnter;
        private int PressingTime;
        private Button ExitButton;
        private string[] TextName;
        private Animator Animation;
        private ScrollRect TextScroll;
        private LanguageButton Language;
        private float TimeDefaultBackup;
        private GameObject[] Experiments;
        private Text TextObject, TextNameObject;
        private GameObject InformationUI, ApplicationUI, ScreenObject, InformationScreenObject;

        void Start()
        {
            TimeDefaultBackup = Time.timeScale;

            Language = FindObjectOfType<LanguageButton>();
            ScreenObject = GameObject.FindGameObjectWithTag("Screen");
        }

        void FixedUpdate()
        {
            Experiments = GameObject.FindGameObjectsWithTag("Experiment");

            InformationScreenObject = GameObject.FindGameObjectWithTag("Information");

            if (!ApplicationButton.GvrSceneLoaded)
            {
                ApplicationUI = ScreenObject.transform.GetChild(0).gameObject;
                InformationUI = ScreenObject.transform.GetChild(1).gameObject;
            }
            else
            {
                InformationUI = InformationScreenObject.transform.GetChild(0).gameObject;
            }

            Animation = InformationUI.GetComponent<Animator>();

            if (MouseEnter)
            {
                PressingTime = PressingTime + 1;
            }
            else if (PressingTime > 0)
            {
                PressingTime = PressingTime - 1;
            }

            RaycastHit Hit = new RaycastHit();

            if (MouseEnter && Input.GetMouseButtonDown(0) && PressingTime >= 2)
            {

                if (ApplicationButton.GyroEnabled == false)
                {
                    RayCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
                }
                else
                {
                    RayCamera = CameraType.GyroCamera.ScreenPointToRay(Input.mousePosition);
                }

                if (Physics.Raycast(RayCamera, out Hit, 10000))
                {

                    if (Hit.collider.gameObject && Touched == false)
                    {

                        Time.timeScale = TimeDefaultBackup;

                        Touched = true;

                        InformationShow();

                    }
                }
            }
        }

        public void Exit()
        {
            Touched = false;
            ExitInformation = true;

            Animation.Play("DownSlide");

            if (!ApplicationButton.GoogleVirtualReality)
            {
                ApplicationUI.SetActive(true);
            }

            StartCoroutine("DownSlide");

            float TimeCount = 0.0f;

            while (TimeCount <= 5.0f)
            {
                TimeCount += Time.deltaTime;
            }

            ExperimentObject.SetActive(false);

            Time.timeScale = ApplicationButton.TimeDefaultBackup;
        }

        IEnumerator DownSlide()
        {
            yield return new WaitForSecondsRealtime(0.75f);
            
            foreach (GameObject Go in Experiments)
            {
                Go.SetActive(false);
            }

            InformationUI.SetActive(false);

            // Animation.Play("UpSlide", -1, Time.deltaTime);

            StopCoroutine("DownSlide");
        }

        void OnMouseEnter()
        {
            MouseEnter = true;
        }

        void OnMouseExit()
        {
            MouseEnter = false;
        }

        public void InformationShow ()
        {
            InformationUI.SetActive(true);
            foreach (GameObject Go in Experiments)
            {
                Go.SetActive(false);
            }
            ExperimentObject.SetActive(true);
            Animation.Play("UpSlide");

            if (!ApplicationButton.GoogleVirtualReality)
            {
                ExitButton = GameObject.FindGameObjectWithTag("InformationExitButton").GetComponent<Button>();
                ExitButton.onClick = new Button.ButtonClickedEvent();
                ExitButton.onClick.AddListener(() => Exit());
            }

            // Obter Texto, Nome e o Scroll das informações.
            TextObject = GameObject.FindGameObjectWithTag("InformationText").GetComponent<TextJustified>();
            TextNameObject = GameObject.FindGameObjectWithTag("InformationExperimentName").GetComponent<Text>();
            TextScroll = GameObject.FindGameObjectWithTag("InformationTextScroll").GetComponent<ScrollRect>();

            // Resetar o Scroll para o início.
            TextScroll.verticalNormalizedPosition = 1.0f;

            // Inserir Texto e Nome do Experimento.
            TextObject.text = TextContent[Language.LanguageNumber].text;
            TextNameObject.text = TextContent[Language.LanguageNumber].name.ToUpper();
        }
    }
}