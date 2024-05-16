using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    private bool isPushing = false;
    private Rigidbody2D rb;
    private Animator anim;

    public GameObject objectToPush;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        // Moving the player
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        anim.SetFloat("Horizontal", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Vertical", rb.velocity.y + 0.1f);
        if (moveX < 0 && !isPushing)
        {
            transform.localScale = new Vector3(-0.124f, 0.124f, 0.124f);
        }
        else if (moveX > 0)
        {
            transform.localScale = new Vector3(0.124f, 0.124f, 0.124f);
        }
        else
        {
            anim.SetBool("IsWalk", false);
        }

        // Player jumping
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        //if box transform is near player transform
        if (Vector2.Distance(transform.position, objectToPush.transform.position) < 1.5f)
        {
            Debug.Log("Near");
            PushObject();
        }
    }

    // Check if the player has landed
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    void PushObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPushing = true;
            anim.SetBool("isPush", true);
            objectToPush.GetComponent<BoxController>().isPushing = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            isPushing = false;
            anim.SetBool("isPush", false);
            objectToPush.GetComponent<BoxController>().isPushing = false;
        }
    }
}