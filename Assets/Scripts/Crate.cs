using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class Crate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Bounds();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Destroy(gameObject);
        }
    }

    //Puts crate back in playable zone
    private void Bounds()
    {
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(Random.Range(-2.0f, +2.0f), Random.Range(-2.0f, +2.0f), 0);
        }

        if (transform.position.y > 4)
        {
            transform.position = new Vector3(Random.Range(-2.0f, +2.0f), Random.Range(-2.0f, +2.0f), 0);
        }

        if (transform.position.x < -8)
        {
            transform.position = new Vector3(Random.Range(-2.0f, +2.0f), Random.Range(-2.0f, +2.0f), 0);
        }

        if (transform.position.x > 8)
        {
            transform.position = new Vector3(Random.Range(-2.0f, +2.0f), Random.Range(-2.0f, +2.0f), 0);
        }
    }
}
