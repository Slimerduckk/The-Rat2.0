using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public int playerHp = 5;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpDistance = 2f;
    const int g = 10;
    float jumpForce;
    Rigidbody2D rb;
    bool isJump = false;
    LvlManager lvl;
    [SerializeField]int attack = 2;
    private Enemy enemy;
    void Start()
    {
        lvl = FindObjectOfType<LvlManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        jumpForce = Mathf.Sqrt(2 * jumpDistance * g * rb.gravityScale);
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(speed * deltaX, rb.linearVelocity.y);
        if (!isJump && Input.GetKey(KeyCode.Space))
        {
            isJump = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
        if(playerHp <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isJump = false;
        }
        if (collision.gameObject.CompareTag("finish"))
        {
            //lvl.NextLvl();
            if (collision?.gameObject != null && collision.gameObject.CompareTag("finish"))
            {   
        // Check if lvl and NextLvl are not null
            if (lvl != null)
            {
                lvl.NextLvl();
            }
            else
            {
                Debug.LogError("lvl is null. Ensure it is assigned in the Inspector or via script.");
            }
            }
        }   
        if(collision.gameObject.CompareTag("spike"))
        {
            playerHp--;
        }
        if (collision.gameObject.CompareTag("enemy")) 
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.hp -= attack;
                Debug.Log("Enemy HP: " + enemy.hp);
            }
        }
    }
}