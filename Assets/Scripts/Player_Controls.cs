using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controls : MonoBehaviour
{
[SerializeField]
    public Text touchText;
    public Text swipeText;
    private Vector2 BeginSwipe;
    private Vector2 EndSwipe;
    private bool isMoving;
    public Vector2 velocity;
    private Rigidbody2D rb2D;
    private Sprite mySprite;
    private SpriteRenderer sr;
    public bool isGrounded = false;
    public Player_Data player_Data;
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchText.text = "Moving";
            isMoving = true;
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                BeginSwipe = Input.GetTouch(0).position;
            }
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary && isMoving == false)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            touchText.text = "Touched";
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchText.text = "Ended Moving";
            if (isMoving)
            {
                EndSwipe = Input.GetTouch(0).position;
                float distance = (BeginSwipe - EndSwipe).magnitude;
                swipeText.text = distance.ToString();
            }
            else
            {
                swipeText.text = "No Swipe";
            }
            isMoving = false;
        }

        Movement();
    }

    private bool IsLeftButton;
    private bool IsRightButton;
    private bool IsJumpButton;
    private float speed = 2.5f;
    private float maxSpeed = 5;
    private float timer = 0;
    private void Movement()
    {
        if (IsJumpButton && isGrounded && velocity.y == 0)
        {
            if (IsLeftButton)
            {
                if (velocity.x >= -maxSpeed)
                {
                    velocity.x -= speed;
                }
            }
            else if (IsRightButton)
            {
                if (velocity.x <= maxSpeed)
                {
                    velocity.x += speed;
                }
            }
            velocity.y = 15;
            IsJumpButton = false;
        }
        else if (!IsJumpButton)
        {
            if (IsLeftButton)
            {
                if (velocity.x >= -maxSpeed)
                {
                    velocity.x -= speed;
                }
            }
            else if (IsRightButton)
            {
                if (velocity.x <= maxSpeed)
                {
                    velocity.x += speed;
                }
            }
        }
        if (!IsRightButton && !IsLeftButton)
        {
            if (velocity.x > 0)
            {
                velocity.x -= speed;
            }
            else if (velocity.x < 0)
            {
                velocity.x += speed;
            }
        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else if (!isGrounded && velocity.y > -10)
        {
            velocity.y -= 1f;
        }
        rb2D.MovePosition(rb2D.position + velocity * Time.deltaTime);
    }

    public void OnLeftButton()
    {
        IsLeftButton = true;
        player_Data.playerSword.GetComponent<Animator>().SetBool("Forward", false);
        GetComponent<SpriteRenderer>().flipX = true;
        GetComponent<Animator>().SetTrigger("IsRunning");
    }
    public void OnLeftButtonRelease()
    {
        IsLeftButton = false;
        GetComponent<Animator>().SetTrigger("IsIdle");
    }
    public void OnRightButton()
    {
        IsRightButton = true;
        player_Data.playerSword.GetComponent<Animator>().SetBool("Forward", true);
        GetComponent<SpriteRenderer>().flipX = false;
        GetComponent<Animator>().SetTrigger("IsRunning");
    }
    public void OnRightButtonRelease()
    {
        IsRightButton = false;
        GetComponent<Animator>().SetTrigger("IsIdle");
    }

    public void OnJumpButton() { IsJumpButton = true; }
    public void OnJumpButtonRelease() { IsJumpButton = false; }

    public void OnAttackButton()
    {
        player_Data.playerSword.GetComponent<SpriteRenderer>().enabled = true;
        player_Data.playerSword.GetComponent<PolygonCollider2D>().enabled = true;
    }
    public void OnAttackButtonRelease()
    {
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            Debug.Log("Enter");
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            Debug.Log("Exit");
            isGrounded = false;
        }
    }
}
