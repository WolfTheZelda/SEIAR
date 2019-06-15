using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlappyCellButton : MonoBehaviour
{
    public static FlappyCellButton Static;
    public Text ScoreText;
    public GameObject GameOverUI;
    public Text RecordText;
    public Text PointsText;
    public GameObject PointsUI;

    private int Score = 0;
    public bool GameOver;
    public float ScrollSpeed = -1.5f;
    private int Record;

    public string[] ScoreString;
    public string[] RecordString;


    void Awake()
    {
        if (Static == null)
        {
            Static = this;
        }
        else if (Static != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (GameOver && TouchDetector.Touched && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdScored()
    {
        if (GameOver)
        {
            return;
        }

        Score = Score + 1;
        PointsText.text = Score.ToString();
    }

    public void BirdDied()
    {
        PointsUI.SetActive(false);
        GameOverUI.SetActive(true);
        Record = PlayerPrefs.GetInt("Score");

        if (Score > Record)
        {
            Record = Score;
            PlayerPrefs.SetInt("Score", Record);

        }

        ScoreText.text = ScoreString[PlayerPrefs.GetInt("LanguageNumber")] + " " + Score.ToString();
        RecordText.text = RecordString[PlayerPrefs.GetInt("LanguageNumber")] + " " + Record.ToString();
    }
}
