using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] private bool isHurt = false;
    [SerializeField] private float invulnerableDur = 0.5f;
    [SerializeField] private float invulnerableTime = 0;

    [SerializeField] private float health = 3;
    [SerializeField] private float damage = 2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInvulnerableTime();
    }


    //Responsible for dealing damage
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.CompareTag("Player"))
        //{
        //    Player P = collision.GetComponent<Player>();
        //    P.Damage();
        //}
        if (collision.CompareTag("Weapon0") || collision.CompareTag("Player"))
        {
            if (!isHurt)
            {
                isHurt = true;
                health -= damage;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
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
            }
        }
    }
}
