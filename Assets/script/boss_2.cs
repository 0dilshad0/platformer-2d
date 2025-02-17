using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss_2 : MonoBehaviour
{
    public float detectionRange = 3f;
    public float attackingRange = 0.1f;
    public float groundCheckDistance = 0.57f;
    public float frontCheckDistance = 0.72f;
    public float speed = 3f;
    public int helth = 20;
    public float direction = 1f;
    public float attackinCoolDowm = 2.5f;
    public float force = 15f;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public Transform point;
    public Rigidbody2D rb;
    public Animator animator;
    public ParticleSystem ded;
    public Transform firePoint;
    public Transform tornadoPoint;
    public GameObject fireball;
    public GameObject tornado;
    public sword_man sword_Man;
    public ParticleSystem invParticle;
    public GameObject army;
    public Transform armypointA;
    public Transform armypointB;
    public Slider Slider;
    public GameObject platform; 

    private bool isRange;
    private bool isAttack;
    private bool isAttackRange;
    private bool isGround;
    private bool isBlock;
    private Transform player;
    private GameObject ball;
    private float lastAttackTime = -Mathf.Infinity;


    public GameObject[] body;
    private bool Isinvisible;
    private CapsuleCollider2D collider;

    private void Start()
    {
        // Ignore collision between enemies
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyLayer"), LayerMask.NameToLayer("EnemyLayer"));
        collider = GetComponent<CapsuleCollider2D>();
        Slider.maxValue = helth;
    }

    void Update()
    {
        Slider.value = helth;
        if(helth==10)
        {
           foreach(GameObject Body in body)
            {
                Body.SetActive(false);
               Isinvisible = true;
            }
            StartCoroutine(invisible());
            StartCoroutine(spon());
            rb.bodyType = RigidbodyType2D.Static;
            collider.enabled = false;
            Instantiate(invParticle, transform.position, Quaternion.identity);
            

            helth--;
        }
        if (helth <= 0)
        {
            platform.SetActive(true);
            Instantiate(ded, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }

        // check ground
        isGround = Physics2D.Raycast(point.position, Vector2.down, groundCheckDistance, groundLayer);

        // Check front
        RaycastHit2D frontInfo = Physics2D.Raycast(point.position, Vector2.right * direction, frontCheckDistance);
        isBlock = frontInfo.collider != null && !frontInfo.collider.CompareTag("Player") && !frontInfo.collider.CompareTag("Enemy");

        //check player
        Collider2D playerinfo = Physics2D.OverlapCircle(point.position, detectionRange, playerLayer);
        isRange = playerinfo != null;

        //fir range
        RaycastHit2D firInfo = Physics2D.Raycast(point.position, Vector2.right * direction, attackingRange);
        isAttackRange = firInfo.collider != null && firInfo.collider.CompareTag("Player") && !firInfo.collider.CompareTag("Enemy") && !firInfo.collider.CompareTag("FireBall");
        if (isRange)
        {
            player = playerinfo.transform;
            if (!isGround)
            {
                rb.linearVelocity = Vector2.zero;
                isAttack = true;
            }
            else if (isAttackRange && !Isinvisible)
            {
                attack();

                isAttack = true;
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
        int directions = (int)Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(speed * directions, rb.linearVelocity.y);
        transform.localScale = new Vector3(directions * 0.53f, 0.53f, 0.53f);
        direction = directions;
    }

    private void attack()
    {
        rb.linearVelocity = Vector2.zero;
        if (Time.time < lastAttackTime + attackinCoolDowm)
            return;
        lastAttackTime = Time.time;
        int type = Random.Range(0, 5);
        if(type==0)
        {
            animator.SetTrigger("isAttack");
            GameObject tor = Instantiate(tornado, firePoint.position, firePoint.rotation);
            if (tor != null)
            {
                Rigidbody2D ballRb1 = tor.GetComponent<Rigidbody2D>();
                ballRb1.linearVelocity = new Vector2(direction * force, ballRb1.linearVelocity.y);

            }
        }
        else
        {
            animator.SetTrigger("isAttack");
            GameObject ball = Instantiate(fireball, firePoint.position, firePoint.rotation);

            if (ball != null)
            {
                Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
                ballRb.linearVelocity = new Vector2(direction * force, ballRb.linearVelocity.y);

            }
        }
      

    }

    private void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3(direction * 0.53f, 0.53f, 0.53f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(point.position, point.position + Vector3.down * groundCheckDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(point.position, point.position + Vector3.right * direction * detectionRange);

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


    }
    IEnumerator invisible()
    {
        yield return new WaitForSecondsRealtime(10f);
        foreach (GameObject Body in body)
        {
            Body.SetActive(true);
            Isinvisible = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            collider.enabled = true;
            Instantiate(invParticle, transform.position, Quaternion.identity);
        }
       

    }
    IEnumerator spon()
    {
        while (Isinvisible)
        {
            yield return new WaitForSecondsRealtime(3f);
            Instantiate(army, armypointA.position, Quaternion.identity);
            Instantiate(army, armypointB.position, Quaternion.identity);

        }


    }

}
