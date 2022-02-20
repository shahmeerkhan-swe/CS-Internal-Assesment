using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{


    public static ScoreManager instance;


    public Text PointsText;
    public Text HighScoreText;

    int score = 0;
    int highScore = 0;

    

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        PointsText.text = score.ToString() + " POINTS";
        HighScoreText.text = "HighScore: " + highScore.ToString();

    }

    public void AddPoint()
    {
        score += 1;
        PointsText.text = score.ToString() + " POINTS";


        if (highScore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        
    }


}
