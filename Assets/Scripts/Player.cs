using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour

{
    //Variables
    [SerializeField] private float speed = 6.5f;
    [SerializeField] private int health = 3;
    [SerializeField] private bool isHurt = false;
    [SerializeField] private float invulnerableDur = 0.5f;
    [SerializeField] private float invulnerableTime = 0;

    [SerializeField] public int weaponNum = 1;
    [SerializeField] private bool isStriking = false;
    [SerializeField] private float attackDur = 0.5f;
    [SerializeField] private float attackTime = 0;
    [SerializeField] private float attackLag = 0.3f;
    [SerializeField] private bool attackPrep = false;

    private bool facingRight = true;

    [SerializeField] private GameObject[] weapons;
    [SerializeField] private int[] baseDamage;
    [SerializeField] private int bonusDamage;

    [SerializeField] private AudioClip AttackSFX;
    [SerializeField] private AudioClip KOSFX;
    [SerializeField] private AudioClip PowerSFX;
    [SerializeField] private AudioClip HurtSFX;

    private UIManager UI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Bounds();
        Strike();
        AttackPreparation();
        CheckStrikeTime();
        CheckInvulnerableTime();
    }

    // Moves the player
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0) //Flips the character left or right - may need fixed later, errors occur when pressing both arrows at once.
        {
            if((horizontalInput < 0 && facingRight) || (horizontalInput > 0 && !facingRight))
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
        AudioSource.PlayClipAtPoint(AttackSFX, Camera.main.transform.position, 1.0f);
        if (weaponNum == 0)
        {
            weapons[0].SetActive(true);
        }
        else if (weaponNum == 1)
        {
            attackPrep = true;
        }
        else if (weaponNum == 2)
        {
            weapons[2].SetActive(true);
        }
        isStriking = true;
    }

    void AttackPreparation()
    {
        if (attackPrep)
        {
            attackTime += Time.deltaTime;
            if (attackTime > attackLag)
            {
                weapons[weaponNum].SetActive(true);
                attackPrep = false;
            }
        }
    }


    void CheckStrikeTime()
    {
        if (isStriking)
        {
            attackTime += Time.deltaTime;
            if (attackTime > attackDur)
            {
                isStriking = false;
                for(int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].SetActive(false);
                }
                attackTime = 0;
            }
        }
    }


    //changes weapon
    public void setWeapon(int powerUpID)
    {
        AudioSource.PlayClipAtPoint(PowerSFX, Camera.main.transform.position, 1.0f);
        weaponNum = powerUpID;
    }

    //enhances stats
    public void statUp(int powerUpID)
    {
        AudioSource.PlayClipAtPoint(PowerSFX, Camera.main.transform.position, 1.0f);
        if (powerUpID == 0)
        {
            bonusDamage++;
        } else if (powerUpID == 1)
        {
            speed += 0.1f;
        } else if (powerUpID == 2)
        {
            health++;
        }
    }

    //damages the player
    public void hurt()
    {
        if (!isHurt)
        {
            AudioSource.PlayClipAtPoint(HurtSFX, Camera.main.transform.position, 1.0f);
            isHurt = true;
            health -= 1;
            if (UI != null)
            {
                UI.UpdateLives(health); //actual parameter
            }
            if (health <= 0)
            {
                GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
                GM.gameOver = true;
                AudioSource.PlayClipAtPoint(KOSFX, Camera.main.transform.position, 1.0f);
                Destroy(gameObject);
            }
        }
    }

    void CheckInvulnerableTime()
    {
        if (isHurt)
        {
            invulnerableTime += Time.deltaTime;
            if (invulnerableTime > invulnerableDur)
            {
                isHurt = false;
                invulnerableTime = 0;
            }
        }
    }
    public int getDamage()
    {
        return bonusDamage + baseDamage[weaponNum];
    }

    //pushes the crates
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            Rigidbody2D pushableRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (pushableRb != null)
            {
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                float pushForce = 1f;
                pushableRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                
            }
        }
    }
}
