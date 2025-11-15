using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject[] PowerUpPrefabs;

    [SerializeField] private GameManager GM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        UIManager UI = GameObject.Find("Canvas").GetComponent<UIManager>();


        UI.resetScore();



        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerUpSpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (!GM.gameOver)
        {
            yield return new WaitForSeconds(3.0f);
            Instantiate(EnemyPrefab, new Vector3(Random.Range(-4.0f, +4.0f), Random.Range(-4.0f, +4.0f), 0), Quaternion.identity);
        }
    }

    IEnumerator PowerUpSpawn()
    {
        while (!GM.gameOver)
        {
            yield return new WaitForSeconds(5.0f);
            Instantiate(PowerUpPrefabs[Random.Range(0, PowerUpPrefabs.Length)], new Vector3(Random.Range(-4.0f, +4.0f), Random.Range(-4.0f, +4.0f), 0), Quaternion.identity);
        }
    }
}
