using UnityEngine;

public class Player : MonoBehaviour

{
    //Variables
    [SerializeField] private float speed = 6.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Bounds();
    }

    // Moves the player
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);
    }

    //Sets the bounds of the player
    private void Bounds()
    {
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        }

        if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        }

        if (transform.position.x < -8)
        {
            transform.position = new Vector3(-8, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 8)
        {
            transform.position = new Vector3(8, transform.position.y, transform.position.z);
        }
    }
}
