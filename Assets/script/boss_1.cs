using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class boss_1 : MonoBehaviour
{
    public float detectionRange = 3f;
    public float attackingRange = 0.1f;
    public float groundCheckDistance = 0.57f;
    public float frontCheckDistance = 0.72f;
    public float speed = 3f;
    public int helth = 15;
    public ParticleSystem boxparticle;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public Transform point;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject enemy;
    public Transform[] spiksposs;
    public GameObject spik;
    public Animator door;
    public Slider helthBar;
    public AudioSource explotion;

    private bool isRange;
    private bool isAttack;
    private bool isGround;
    private bool isBlock;
    private int direction = 1;
    private Transform player;
    private bool collide;

    public sword_man sword_Man;
    public ParticleSystem ded;
    private void Start()
    {
        helthBar.maxValue = helth;
      
        // Ignore collision between enemies
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyLayer"), LayerMask.NameToLayer("EnemyLayer"));
        StartCoroutine(spon());
        StartCoroutine(jump());
    }
    void Update()
    {
        helthBar.value = helth;

        if (helth <= 0)
        {
            Instantiate(ded, transform.position, transform.rotation);
            door.SetTrigger("isWin");
            Destroy(gameObject);
        }

        // check ground
        isGround = Physics2D.Raycast(point.position, Vector2.down, groundCheckDistance, groundLayer);

        // Check front
        RaycastHit2D frontInfo = Physics2D.Raycast(point.position, Vector2.right * direction, frontCheckDistance);
        isBlock = frontInfo.collider != null && !frontInfo.collider.CompareTag("Player") && !frontInfo.collider.CompareTag("Enemy");

        //check player
        Collider2D playerinfo = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        isRange = playerinfo != null;



        if (isRange)
        {
            player = playerinfo.transform;
            if (!isGround)
            {
                rb.linearVelocity = Vector2.zero;
                isAttack = true;
            }
            else if (Vector2.Distance(transform.position, player.position) <= attackingRange)
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
        if(!isAttack)
        {
            rb.linearVelocity = new Vector2(speed * direction, rb.linearVelocity.y);
        }
       
    }
    private void chase()
    {
        int directions = (int)Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(speed * directions, rb.linearVelocity.y);
        transform.localScale = new Vector3(directions*3, 3, 3);
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
        transform.localScale = new Vector3(direction*3, 3, 3);
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
        if (collision.gameObject.CompareTag("swad"))
        {
            if (sword_Man.isattack == true)
            {
                helth--;
            }
        }
        if (collision.gameObject.CompareTag("box"))
        {
            explotion.Play();
            Instantiate(boxparticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
            helth -= 3;
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

    IEnumerator spon()
    {

       
        while(helth!=0)
        {
            yield return new WaitForSecondsRealtime(20f);
            Instantiate(enemy, point.position, point.rotation);
        }
     
    }
    IEnumerator jump()
    {


        while (helth != 0)
        {
            yield return new WaitForSecondsRealtime(10f);
            isAttack = true; 
            animator.SetBool("isJump", true);
           
        }

    }
    public void sponspik()
    {
        Transform sponPoint = spiksposs[Random.Range(0, spiksposs.Length)];


        Instantiate(spik, sponPoint.position, sponPoint.rotation);


    }
    public void playerHealth()
    {

        if (collide == true)
        {
            sword_Man.currentHealth -= 20;

        }
    }
}
