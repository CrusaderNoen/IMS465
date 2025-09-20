using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour

{
    //Variables
    [SerializeField] private float speed = 6.5f;

    [SerializeField] public GameObject weapon;
    [SerializeField] private bool isStriking = false;
    [SerializeField] private float attackDur = 0.5f;
    [SerializeField] private float attackTime = 0;

    private bool facingRight = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Bounds();
        Strike();
        CheckStrikeTime();
    }

    // Moves the player
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0) //Flips the character left or right - may need fixed later, errors occur when pressing both arrows at once.
        {
            if((Input.GetKeyDown(KeyCode.LeftArrow) && facingRight) || (Input.GetKeyDown(KeyCode.RightArrow) && !facingRight))
            {
                Vector3 currentScale = transform.localScale;
                currentScale.x *= -1; // Multiply by -1 to flip
                transform.localScale = currentScale;
                facingRight = !facingRight;
            }
        }

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


    void Strike()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isStriking == false)
        {
            Attack();
        }
    }

    //The following three methods cause the user to attack with weapon 0.
    //Reference: Game Code Library - Melee and Ranged Top Down Combat - Unity 2D
    void Attack()
    {
        weapon.SetActive(true);
        isStriking = true;
    }

    void CheckStrikeTime()
    {
        if (isStriking)
        {
            attackTime += Time.deltaTime;
            if (attackTime > attackDur)
            {
                isStriking = false;
                weapon.SetActive(false);
                attackTime = 0;
            }
        }
    }
}
