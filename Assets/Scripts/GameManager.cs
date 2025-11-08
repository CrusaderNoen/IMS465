using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool paused = false;
    [SerializeField] private GameObject PausedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PausedText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
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
