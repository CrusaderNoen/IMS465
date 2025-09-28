using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private int powerUpId;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player P = collision.GetComponent<Player>();
            if (P != null)
            {
                P.setWeapon(powerUpId);
            }
            Destroy(gameObject);
        }
    }
}
