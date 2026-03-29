using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] private bool isHurt = true;
    [SerializeField] private float invulnerableDur = 0.5f;
    [SerializeField] private float invulnerableTime = 0;

    [SerializeField] private float health = 3;

    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private AudioClip Boom;

    private UIManager UI;
    private Animator Anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Hunt();
        CheckInvulnerableTime();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Destroy(gameObject);
        }
    }


    //Responsible for dealing damage
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player P = collision.GetComponent<Player>();
            P.hurt();
        }
        if (collision.CompareTag("Weapon0"))
        {
            if (!isHurt)
            {
                isHurt = true;
                Player P = collision.gameObject.transform.parent.gameObject.transform.parent.GetComponent<Player>();
                health -= P.getDamage();
                //health -= 1;
                Anim.SetBool("isHurt", true);
                if (health <= 0)
                {
                    if (UI != null)
                    {
                        UI.UpdateScore(15);
                    }
                    AudioSource.PlayClipAtPoint(Boom, Camera.main.transform.position, 1.0f);
                    Destroy(gameObject);
                }
            }
        }
        if (collision.CompareTag("Pushable"))
        {
            isHurt = true;
            health = health / 2 - 2;
            if (health <= 0)
            {
                if (UI != null)
                {
                    UI.UpdateScore(15);
                }
                AudioSource.PlayClipAtPoint(Boom, Camera.main.transform.position, 1.0f);
                Destroy(gameObject);
            }
        }
    }


    //Responsible for IFrames
    void CheckInvulnerableTime()
    {
        if (isHurt)
        {
            invulnerableTime += Time.deltaTime;
            if (invulnerableTime > invulnerableDur)
            {
                isHurt = false;
                invulnerableTime = 0;
                Anim.SetBool("isHurt", false);
            }
        }
    }

    //Tracks the player
    private void Hunt()
    {
        if (playerTransform != null && isHurt == false)
        {
            // Move the enemy towards the player's position
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }
}
