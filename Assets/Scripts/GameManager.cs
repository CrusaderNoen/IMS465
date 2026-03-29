using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    private bool paused = false;
    [SerializeField] private GameObject PausedText;
    [SerializeField] private GameObject TitleScreen;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Box;
    private SpawnManager SM;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PausedText.SetActive(false);
        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            TitleScreen.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(Player, new Vector3(-3, 0, 0), Quaternion.identity);
                TitleScreen.SetActive(false);
                gameOver = false;
                SM.StartGame();
                Instantiate(Box, new Vector3(-2, 0, 0), Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                Time.timeScale = 1;
                PausedText.SetActive(false);
                paused = false;
            }
            else
            {
                Time.timeScale = 0;
                PausedText.SetActive(true);
                paused = true;
            }
        }
    }


}
