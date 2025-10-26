using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject[] PowerUpPrefabs;
    private bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerUpSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawn()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(3.0f);
            Instantiate(EnemyPrefab, new Vector3(Random.Range(-4.0f, +4.0f), Random.Range(-4.0f, +4.0f), 0), Quaternion.identity);
        }
    }

    IEnumerator PowerUpSpawn()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(5.0f);
            Instantiate(PowerUpPrefabs[Random.Range(0, PowerUpPrefabs.Length)], new Vector3(Random.Range(-4.0f, +4.0f), Random.Range(-4.0f, +4.0f), 0), Quaternion.identity);
        }
    }
}
