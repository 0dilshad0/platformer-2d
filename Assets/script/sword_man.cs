using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class sword_man : MonoBehaviour
{
    public float move_speed = 1f;
    public float jump_force = 4f;
    public float checkDistance = 4f;
    public int Health = 100;
    public int currentHealth;

    public AudioSource walk;
    public AudioSource swad;
    public Slider HealthBar;
    public ParticleSystem ded;
    public GameObject man;
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;
    private bool isGrounded;
    private float direction=0;

    public bool win=false;
    public bool isattack;

    void Start()
    {
        currentHealth = Health;
        HealthBar.maxValue = Health;
    }


    void Update()
    {
        HealthUpdate();
        walkSound();
        basicMove();
      
    }
   private void walkSound()
    {
        if(isGrounded && direction!=0)
        {
            walk.volume = 0.1f;
        }
        else
        {
            walk.volume = 0f;
        }
    }
    private void HealthUpdate()
    {
        HealthBar.value = currentHealth;

        if(currentHealth<=0)
        {
            Debug.Log(currentHealth);
            Instantiate(ded,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        if(currentHealth>100)
        {
            currentHealth = 100;
        }
    }
    private void basicMove()
    {
         direction = Input.GetAxis("Horizontal");
        //move 
        animator.SetBool("isMove", direction != 0 && isGrounded);
        
        //direction 
        if (direction < 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
        //jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("isJump", true);
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jump_force);
        }
        else
        {
            animator.SetBool("isJump", false);
        }
        //attack
        if (Input.GetMouseButtonDown(0))
        {
           
            animator.SetBool("isAttack", true);
        }
        else
        {
            animator.SetBool("isAttack", false);

        }
        //sit
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isSit", true);
            isGrounded = false;
        }
        else
        {
            GroundCheck();
            animator.SetBool("isSit", false);
            man.transform.position += new Vector3(direction * move_speed, 0, 0) * Time.deltaTime;

        }

    }

    private void GroundCheck()
    {
      
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, groundLayer);   
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Debug.DrawRay(transform.position, Vector2.down * checkDistance, isGrounded ? Color.green : Color.red);
    }

    public void attackon()
    {
        isattack = true;
        swad.Play();
    }
    public void attackoff()
    {
        isattack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("FireBall"))
        {
            currentHealth -= 10;
        }
        if (collision.gameObject.CompareTag("trap"))
        {
            currentHealth =0;
        }
        if(collision.gameObject.CompareTag("health"))
        {
            Destroy(collision.gameObject);
                currentHealth += 30;
          
        }
        if(collision.gameObject.CompareTag("portal"))
        {
            win = true;
            Destroy(gameObject);
        }
    }

}