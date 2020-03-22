using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float jumpHeight;
    public float moveSpeed;
    public float climbSpeed;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool isClimbing = false;
    [SerializeField]
    private bool climbed = false;
    [SerializeField]
    private bool canJump = true;
    private bool facingRight = true;
    public SceneBehaviour sceneBehaviour;
    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;
    private Animator myAnimator;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        float velocityX = move * moveSpeed;
        float velocityY = myRigidbody.velocity.y;
        if (Input.GetKeyDown(KeyCode.W) && isClimbing)
        {
            climbed = false;
            myAnimator.SetBool("climbing", true);
        }
        if (Input.GetKey(KeyCode.W) && isClimbing && !climbed)
        {
            velocityY = 1.0f * climbSpeed;
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && canJump)
        {
            myRigidbody.AddForce(Vector3.up * jumpHeight);
            velocityY = 1.0f * climbSpeed;
        }
        if (Input.GetKey(KeyCode.S) && isClimbing)
        {
            velocityY = -1.0f * climbSpeed;
        }
        if (Input.GetKeyDown(KeyCode.S) && isClimbing)
        {
            myAnimator.SetBool("climbing", true);
        }
        if (isClimbing)
        {
            myAnimator.SetFloat("climbingSpeed", velocityY);
            Debug.Log(velocityY);
        }
        myAnimator.SetFloat("speed", Mathf.Abs(velocityX));
        if (move > 0 && !facingRight)
            ReverseImage();
        else if (move < 0 && facingRight)
            ReverseImage();

        myRigidbody.velocity = new Vector2(velocityX, velocityY);
    }

    void ReverseImage()
    {
        facingRight = !facingRight;
        Vector2 scale = myRigidbody.transform.localScale;
        scale.x *= -1;
        myRigidbody.transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Ladder":
                isClimbing = true;
                myRigidbody.gravityScale = 0.0f;
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<LadderBehaviour>().GetGroundCollider(), myCollider, true);
                break;
            case "LadderBottom":
                canJump = false;
                break;
            case "Ground":
                isGrounded = true;
                break;
            case "Barrel":
                sceneBehaviour.Dead();
                break;
            case "Finish":
                sceneBehaviour.Win();
                break;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Ladder")
        {
            myRigidbody.velocity = Vector2.zero;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Ladder":
                isClimbing = false;
                climbed = true;
                myAnimator.SetBool("climbing", false);
                myRigidbody.gravityScale = 1.0f;
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<LadderBehaviour>().GetGroundCollider(), myCollider, false);
                break;
            case "Ground":
                isGrounded = false;
                break;
            case "LadderBottom":
                canJump = true;
                break;
        }
    }

}

