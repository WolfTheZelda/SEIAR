using System.Collections;
using UnityEngine.UI;
using UnityEngine.VR;
using UnityEngine;
using EasyAR;

namespace SEIAR
{
    public class ApplicationButton : MonoBehaviour
    {
        static public float TimeDefaultBackup;
        static public float TimePausedBackup;
        static public float TimeEditedBackup;

        static public bool GyroEnabled;
        static public bool Paused = false;

        [Header("Interface")]
        public GameObject Overlay;
        public GameObject Interface;
        private GameObject PlayIcon;
        private GameObject PauseButtonObject;
        public GameObject TorchIcon;
        public GameObject MenuUI, ApplicationUI, ReviewUI, InformationUI;
        public GameObject VirtualRealityButton;

        private GameObject AtomicBombButton, NextButtonObject, PreviousButtonObject;

        private Text ExperimentName;

        [Header("Realidade Aumentada")]
        public GameObject TargetsObject;
        public GameObject TargetsCameraObject;
        public GameObject TargetsEventSystemObject;

        [Header("Giroscópio")]
        public GameObject GyroCamera, GyroButtonOject, GyroTarget;

        [Header("Realidade Virtual")]
        public GameObject GvrSystemObject;
        public GameObject GvrEnviromentObject;

        private GameObject[] Target;

        private bool Torched = false;
        private bool Unlocked = true;
        static public bool GoogleVirtualReality = false;
        static public bool GvrAugmentedReality = false;
        static public bool GvrSceneLoaded = false;

        private int ExperimentNumber = 2;

        private ImageTargetManager ImageManager;
        private FilesManager ImageCreater;
        private LanguageButton Language;
        private Animator MenuAnimator;
        
        // Total de Experimentos menos um, sendo 0 = 1.
        public const int ExperimentTotal = 8;

        void Awake()
        {
            Language = FindObjectOfType<LanguageButton>();

            ImageManager = FindObjectOfType<ImageTargetManager>();
            ImageCreater = FindObjectOfType<FilesManager>();
        }

        void Start()
        {
            MenuAnimator = MenuUI.GetComponent<Animator>();

            // Desativar GyroTarget, caso tenha deixado ele ativado.
            GvrSystemObject.SetActive(false);
            GyroTarget.SetActive(false);

            // Impedir desligamento da tela.
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            // Definir backups da escala de tempo.
            TimeDefaultBackup = Time.timeScale;
            TimeEditedBackup = Time.timeScale;

            // Para celular ruim que nem o de Rafael.
            if (Screen.width < 720 && Screen.height < 1280)
            {
                ARBuilder.Instance.CameraDeviceBehaviours[0].CameraSize = new Vector2(Screen.width, Screen.height);
                // ARBuilder.Instance.ImageTrackerBehaviours[0].Mode = ImageTrackerBaseBehaviour.ImageTrackerMode.PreferPerformance;
            }
            else
            {
                ARBuilder.Instance.CameraDeviceBehaviours[0].CameraSize = new Vector2(720, 1280);
                // ARBuilder.Instance.ImageTrackerBehaviours[0].Mode = ImageTrackerBaseBehaviour.ImageTrackerMode.PreferQuality;
            }

            // Detectar se não tem Giroscópio.
            if (!SystemInfo.supportsGyroscope || IfUnityEditor.UnityEditor == true)
            {

                GyroButtonOject.SetActive(false);
                VirtualRealityButton.SetActive(false);
                Input.gyro.enabled = false;

            }
        }

        void FixedUpdate()
        {

            Target = GameObject.FindGameObjectsWithTag("Target");

            PauseButtonObject = GameObject.FindGameObjectWithTag("Pause");

            if (InformationButton.ExitInformation)
            {
                InformationButton.ExitInformation = false;
                PauseButtonObject.transform.GetChild(0).gameObject.SetActive(true);
                PauseButtonObject.transform.GetChild(1).gameObject.SetActive(false);

            }

            /*
            if (Time.timeScale > 0)
            {
                PauseButtonObject.transform.GetChild(0).gameObject.SetActive(true);
                PauseButtonObject.transform.GetChild(1).gameObject.SetActive(false);
                Paused = false;
            }
            */

            for (int i = 0; i <= ExperimentTotal; i++)
            {

                foreach (GameObject Go in Target)
                {

                    if (i == ExperimentNumber)
                    {

                        Go.transform.GetChild(i).gameObject.SetActive(true);

                    }
                    else
                    {

                        Go.transform.GetChild(i).gameObject.SetActive(false);

                    }

                }

            }

            PreviousButtonObject = GameObject.FindGameObjectWithTag("ExperimentPrevious");
            NextButtonObject = GameObject.FindGameObjectWithTag("ExperimentNext");

            AtomicBombButton = GameObject.FindGameObjectWithTag("ExperimentExtraAtomicBomb");

            // Gerenciar botões em relação ao Experimento.
            switch (ExperimentNumber)
            {
                // Sistema Solar.
                case 0:

                    // Gambiarra para arrumar bug no qual alguns planetas ficam desativados.
                    foreach (GameObject Go in Target)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            Go.transform.GetChild(0).transform.GetChild(i).gameObject.SetActive(true);
                        }
                    }

                    PreviousButtonObject.transform.GetChild(0).gameObject.SetActive(false);
                    NextButtonObject.transform.GetChild(0).gameObject.SetActive(true);

                    break;
                
                // Bomba Atômica.
                case 3:

                    // Ativar botão exclusivo para Bomba Atômica.
                    AtomicBombButton.transform.GetChild(0).gameObject.SetActive(true);

                    break;

                // Último Experimento.
                case ExperimentTotal:

                    PreviousButtonObject.transform.GetChild(0).gameObject.SetActive(true);
                    NextButtonObject.transform.GetChild(0).gameObject.SetActive(false);

                    break;
                
                // Todos os outros Experimentos.
                default:

                    PreviousButtonObject.transform.GetChild(0).gameObject.SetActive(true);
                    NextButtonObject.transform.GetChild(0).gameObject.SetActive(true);

                    AtomicBombButton.transform.GetChild(0).gameObject.SetActive(false);

                    break;
            }
            
            ExperimentName = GameObject.FindGameObjectWithTag("ExperimentName").GetComponent<Text>();

            // Pegar nome dos experimentos.
            foreach (GameObject Go in Target)
            {

                LanguageText ExperimentText = Go.transform.GetChild(ExperimentNumber).transform.gameObject.GetComponent<LanguageText>();

                ExperimentName.text = ExperimentText.LanguageTexts[Language.LanguageNumber];

            }

            if(!GoogleVirtualReality)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

        }

        public void NextButton()
        {
            if (ExperimentNumber < ExperimentTotal)
            {
                ExperimentNumber = ExperimentNumber + 1;
            }
        }

        public void PreviousButton()
        {
            if (ExperimentNumber > 0)
            {
                ExperimentNumber = ExperimentNumber - 1;
            }
        }

        public void SpeedUpButton()
        {
            PauseButtonObject.transform.GetChild(0).gameObject.SetActive(true);
            PauseButtonObject.transform.GetChild(1).gameObject.SetActive(false);

            if (Time.timeScale <= 15)
            {
                Time.timeScale = Time.timeScale + 1;
                TimeEditedBackup = Time.timeScale;

            }
        }

        public void SlowDownButton()
        {
            PauseButtonObject.transform.GetChild(0).gameObject.SetActive(true);
            PauseButtonObject.transform.GetChild(1).gameObject.SetActive(false);

            if (Time.timeScale > 1)
            {
                Time.timeScale = Time.timeScale - 1;
                TimeEditedBackup = Time.timeScale;

            }
        }

        public void ExitButton()
        {
            ARBuilder.Instance.CameraDeviceBehaviours[0].SetFlashTorchMode(false);
            TorchIcon.SetActive(false);

            MenuUI.SetActive(true);
            ReviewUI.SetActive(false);
            InformationUI.SetActive(false);

            MenuAnimator.Play("RightMenu");

            Time.timeScale = TimeDefaultBackup;
            InformationButton.Touched = true;

            ImageCreater.ClearTexture();
            ImageManager.ClearAllTarget();
        }

        public void PauseButton()
        {
            Paused = !Paused;

            if (Paused)
            {

                PauseButtonObject.transform.GetChild(0).gameObject.SetActive(false);
                PauseButtonObject.transform.GetChild(1).gameObject.SetActive(true);
                Time.timeScale = 0;
                TimePausedBackup = 0;

            }
            else if (!Paused)
            {
                PauseButtonObject.transform.GetChild(0).gameObject.SetActive(true);
                PauseButtonObject.transform.GetChild(1).gameObject.SetActive(false);
                Time.timeScale = TimeEditedBackup;

            }
        }

        public void TorchButton()
        {
            Torched = !Torched;

            if (Torched)
            {

                TorchIcon.SetActive(true);
                ARBuilder.Instance.CameraDeviceBehaviours[0].SetFlashTorchMode(true);

            }
            else if (!Torched)
            {

                TorchIcon.SetActive(false);
                ARBuilder.Instance.CameraDeviceBehaviours[0].SetFlashTorchMode(false);

            }
        }

        public void BombButton()
        {
            if (Unlocked)
            {

                Unlocked = false;

                AtomicBomb.Detonated = true;

                StartCoroutine("BombLocked");

            }
        }

        public void GyroButton()
        {
            // Limpar e remover UserTarget.
            ImageCreater.ClearTexture();
            ImageManager.ClearAllTarget();

            // Ativar Modo GPS.
            GyroCamera.SetActive(true);
            GyroTarget.SetActive(true);
            GyroEnabled = true;
        }

        public void ScannerButton()
        {
            // Criar novo UserTarget.
            ImageCreater.ClearTexture();
            ImageManager.ClearAllTarget();
            ImageCreater.StartTakePhoto();

            // Desativar Modo GPS.
            GyroCamera.SetActive(false);
            GyroTarget.SetActive(false);
            GyroEnabled = false;

            foreach (GameObject Go in Target)
            {

                for (int i = 0; i <= ExperimentTotal; i++)
                {

                    // Resetar posição, rotação e escala de todos os experimentos.
                    Go.transform.GetChild(i).transform.localPosition = new Vector3(0, 0, 0);
                    Go.transform.GetChild(i).transform.localScale = new Vector3(1, 1, 1);
                    Go.transform.GetChild(i).transform.rotation = Quaternion.identity;

                }

            }
        }

        public void GvrButton ()
        {
            StartCoroutine(OverlayTimer(1.5f));

            GoogleVirtualReality = !GoogleVirtualReality;            

            if (GoogleVirtualReality)
            {
                StartCoroutine(VRLoadDevice("cardboard"));

                Interface.SetActive(false);
                GyroCamera.SetActive(false);
                TargetsObject.SetActive(false);
                TargetsCameraObject.transform.GetChild(1).GetComponentInChildren<AudioListener>().enabled = false;
                for (int i = 0; i < TargetsCameraObject.transform.childCount; i++)
                {
                    TargetsCameraObject.transform.GetChild(i).gameObject.SetActive(false);
                }

                if(GvrAugmentedReality)
                {
                    TargetsCameraObject.transform.GetChild(1).gameObject.SetActive(true);
                }

                TargetsEventSystemObject.SetActive(false);

                GvrSystemObject.SetActive(true);

                GvrSceneLoaded = true;

                // Camera.main.transform.rotation = Quaternion.identity;

                /*
                GyroTarget.transform.position = new Vector3(0, 0, 1.5f);
                GyroTarget.transform.rotation = Quaternion.identity;
                */

            }
            else if (!GoogleVirtualReality)
            {
                StartCoroutine(VRLoadDevice("none"));

                GvrSystemObject.SetActive(false);

                for (int i = 0; i < TargetsCameraObject.transform.childCount; i++)
                {
                    TargetsCameraObject.transform.GetChild(i).gameObject.SetActive(true);
                }
                TargetsCameraObject.transform.GetChild(1).GetComponentInChildren<AudioListener>().enabled = true;
                TargetsEventSystemObject.SetActive(true);
                TargetsObject.SetActive(true);
                Interface.SetActive(true);

                GvrSceneLoaded = false;

                /*
                GyroTarget.transform.position = new Vector3(0, 0, 0);
                GyroTarget.transform.rotation = Quaternion.identity;
                */

            }
        }

        public void GvrModeButton()
        {
            GvrAugmentedReality = !GvrAugmentedReality;

            if(GvrAugmentedReality)
            {
                TargetsCameraObject.transform.GetChild(1).gameObject.SetActive(true);
                GvrEnviromentObject.SetActive(false);
            }
            else if (!GvrAugmentedReality)
            {
                TargetsCameraObject.transform.GetChild(1).gameObject.SetActive(false);
                GvrEnviromentObject.SetActive(true);
            }
        }

        IEnumerator BombLocked()
        {

            yield return new WaitForSecondsRealtime(45.0f);

            Unlocked = true;

        }

        IEnumerator VRLoadDevice(string NewDevice)
        {
            VRSettings.LoadDeviceByName(NewDevice);
            yield return null;
            VRSettings.enabled = true;
        }

        IEnumerator OverlayTimer(float TimeLimit)
        {
            Overlay.SetActive(true);

            yield return new WaitForSecondsRealtime(TimeLimit);

            Overlay.SetActive(false);
        }
    }
}