using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text lifeDisplay;
    [SerializeField] private Text scoreDisplay;
    [SerializeField] private Text highScoreDisplay;
    public int score;
    private int highScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreDisplay.text = "High: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int currentLives)
    {
        lifeDisplay.text = "Health: " + currentLives;
    }

    public void UpdateScore(int scoredPoints)
    {
        score += scoredPoints;
        scoreDisplay.text = "Score: " + score;

        if (score > highScore)
        {
            highScore = score;
            highScoreDisplay.text = "High: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore); //saves high score
        }
    }
}
