using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float jumpHeight;
    public float moveSpeed;
    private bool isGrounded;
    private bool isClimbing = false;
    private bool canJump = true;
    public SceneBehaviour sceneBehaviour;
    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && isClimbing)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && canJump)
        {
            myRigidbody.AddForce(Vector3.up * jumpHeight);
        }
        if (Input.GetKey(KeyCode.S) && isClimbing)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Ladder":
                isClimbing = true;
                myRigidbody.gravityScale = 0.0f;
                myRigidbody.velocity = Vector2.zero;
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

    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Ladder":
                isClimbing = false;
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

