using UnityEngine;

public class StatUp : MonoBehaviour
{

    [SerializeField] private int powerUpId;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player P = collision.GetComponent<Player>();
            if (P != null)
            {
                P.statUp(powerUpId);
            }
            Destroy(gameObject);
        }
    }
}