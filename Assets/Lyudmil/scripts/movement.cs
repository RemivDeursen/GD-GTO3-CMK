using UnityEngine;
using UnityEngine.UI;
public class movement : MonoBehaviour {


    public float horizontalMovementForce = 400f;
    public float jumpForce = 400f;
    bool onTheGround;
    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private float direction;
    public float timer = 0;
    public float maxTime;
    bool isJumping;
    public Text keysCounterText;
    int Key_counter=0;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            Debug.Log("floor");
            onTheGround = true;
            isJumping = false;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer ==
            LayerMask.NameToLayer("Platform"))
        {
            Debug.Log("not on the floor");
            onTheGround = false;
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "pickup")
        {
            Key_counter++;
            collision.gameObject.SetActive(false);
            Debug.Log("picked UP");
            keysCounterText.text = "Keys: " + Key_counter;
        }
    }
    private Vector3 m_velocity = Vector3.zero;
    void FixedUpdate()
    {
        //if(Input.GetKey(KeyCode.A))
        //      {
        //          if(Input.GetKey(KeyCode.Space))
        //          {
        //              Vector3 speed1 = new Vector2(-10f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        //              gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(gameObject.GetComponent<Rigidbody2D>().velocity, speed1/10, ref m_velocity, .05f);
        //          }
        //          Vector3 speed = new Vector2(-10f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        //          gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(gameObject.GetComponent<Rigidbody2D>().velocity, speed, ref m_velocity, .05f);
        //      }
        //      else if (Input.GetKey(KeyCode.D))
        //      {
        //          if (Input.GetKey(KeyCode.Space))
        //          {
        //              Vector3 speed1 = new Vector2(10f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        //              gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(gameObject.GetComponent<Rigidbody2D>().velocity, speed1 / 10, ref m_velocity, .05f);
        //          }
        //          Vector3 speed = new Vector2(10f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        //          gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(gameObject.GetComponent<Rigidbody2D>().velocity, speed, ref m_velocity, .05f);
        //      }
            direction = Input.GetAxisRaw("Horizontal");
        if (direction != 0)
        {
            velocity = new Vector2(10, 0);
        }

        if (Input.GetKey(KeyCode.Space) && onTheGround == true)
        {
            isJumping = true;
        }
        if( isJumping)
        { 
            Debug.Log("jump");
            if(timer < maxTime)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
                rb.AddForce(new Vector2(0, jumpForce*500 * Time.fixedDeltaTime));
            timer += 1 * Time.deltaTime;
            }
            else if (timer >= maxTime)
            {
                timer = 0;
                isJumping = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
            }  
            
        }
        rb.MovePosition(rb.position + (direction * velocity) * Time.fixedDeltaTime);

    }
}
