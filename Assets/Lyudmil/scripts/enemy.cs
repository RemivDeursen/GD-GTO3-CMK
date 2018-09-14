using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
    Rigidbody2D rb;
    int maxTime = 6;
    float timer = 0;
    Vector2 velocity = new Vector2(0,0);
    float increase = 6;
    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="player")
        {
            Debug.Log("enemy hit player");
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    // Update is called once per frame
    void Update () {
        if (timer < maxTime)
        {
            velocity.x = -increase;
            timer += 1 * Time.deltaTime;
        }
        else if (timer >= maxTime)
        {
            timer = 0;
            increase = increase * -1;
        }
        rb.velocity = velocity;
    }
}
