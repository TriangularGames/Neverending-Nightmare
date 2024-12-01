using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;

    private bool GameOver = false;

    private float distanceFromStalker;

    //Jump variables
    float jumpForce = 8.5f;
    float fallMultiplier = 1.5f;
    float lowJumpMultiplier = 2f;
    private bool prev_grounded = false;

    //Last Time on Ground
    public float rememberGroundedFor;
    float lastTimeGrounded;

    //Grounded variables
    bool isGrounded = false;
    public Transform isGroundedChecker;
    float checkGroundRadius = 0.1f;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine("ParticlesBoy");
    }

    IEnumerator ParticlesBoy()
    {
        while (!GameOver)
        {
            yield return new WaitForSeconds(Random.Range(0, distanceFromStalker));
            GetComponentInChildren<ParticleSystem>().Play();
        }
        yield return null;
    }

    // Update is called once per frame
    private void Update()
    {
        distanceFromStalker = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Chaser").transform.position);

        Jump();
        BetterJump();
        CheckIfGrounded();

        if (!isGrounded && rb2d.velocity.y < 0)
        {
            anim.SetBool("Falling", true);
        }
        else
        {
            anim.SetBool("Falling", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chaser")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().EndGame();
            GameOver = true;
        }
    }

    //Basic Jump
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded 
            >= rememberGroundedFor))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }
    }

    //Float Jump Adjustment
    void BetterJump()
    {
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    //Grounded Check
    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, 
            groundLayer);
        if (colliders != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    private void LateUpdate()
    {
        prev_grounded = isGrounded;
    }

}
