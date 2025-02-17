using System;

using UnityEngine;

public class enemy_one : MonoBehaviour
{
    public float detectionRange = 3f;
    public float attackingRange = 0.1f;
    public float groundCheckDistance = 0.57f;
    public float frontCheckDistance = 0.72f;
    public float speed = 3f;
    public int helth=3;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public Transform point;
    public Rigidbody2D rb;
    public Animator animator;
    public ParticleSystem ded;
    public GameObject healthObj;
    public sword_man sword_Man;
    public ParticleSystem blood;

    private bool isRange;
    private bool isAttack;
    private bool isGround;
    private bool isBlock;
    private int direction = 1;
    private Transform player;
    private bool collide;
    private int type = 0;


    private void Start()
    {
        // Ignore collision between enemies
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyLayer"), LayerMask.NameToLayer("EnemyLayer"));
        type = UnityEngine.Random.Range(1, 11);
       
    }
    void Update()
    {
      

        if(helth<=0)
        {
            Instantiate(ded, transform.position, Quaternion.identity);
            if(type==1)
            {
                Instantiate(healthObj, transform.position, Quaternion.identity);
              
            }
            Destroy(gameObject);

        }

        // check ground
        isGround = Physics2D.Raycast(point.position, Vector2.down, groundCheckDistance, groundLayer);

        // Check front
        RaycastHit2D frontInfo = Physics2D.Raycast(point.position, Vector2.right * direction, frontCheckDistance);
        isBlock = frontInfo.collider != null && !frontInfo.collider.CompareTag("Player")&& !frontInfo.collider.CompareTag("Enemy");

        //check player
        Collider2D playerinfo = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        isRange = playerinfo!= null;


        if(isRange)
        {
            player = playerinfo.transform;
            if (!isGround)
            {
                rb.linearVelocity = Vector2.zero;
                isAttack = true;
            }
            else if(Vector2.Distance(transform.position,player.position) <= attackingRange)
            {
                attack();
            }
            else
            {
                isAttack = false;
                chase();
            }
        }
        else
        {
            patrol();
            isAttack = false;
        }
        animator.SetBool("isRunning", !isAttack);

    }

    private void patrol()
    {
        if (!isGround || isBlock)
        {
            Flip();
        }

        rb.linearVelocity = new Vector2(speed * direction, rb.linearVelocity.y);
    }
    private void chase()
    {
        int directions = (int)Mathf.Sign(player.position.x -transform.position.x);
        rb.linearVelocity = new Vector2(speed * directions, rb.linearVelocity.y);
        transform.localScale = new Vector3(directions, 1, 1);
        direction = directions;
    }

    private void attack()
    {
        isAttack = true;
        animator.SetTrigger("isAttack");
        rb.linearVelocity = Vector2.zero;

    }

    private void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(point.position, point.position + Vector3.down * groundCheckDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * direction * detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(point.position, point.position + Vector3.right * direction * frontCheckDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("swad"))
        {
            if (sword_Man.isattack == true)
            {
                helth--;
                Instantiate(blood, transform.position, Quaternion.identity);
            }

            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("swad"))
            {
                collide = true;
               
            }
            else
            {
                collide = false;
            }
        }

    }
   

    public void playerHealth()
    {
      
        if (collide==true)
        {
            sword_Man.currentHealth -= 10;
            
        }

    }
}


