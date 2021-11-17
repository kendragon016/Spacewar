using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    public float speed;
    public float rotationSpeed;
    float direction;
    float gravitationalSpeed;
    public bool WASD;
    GameObject bullet;
    GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravitationalSpeed = 0.25f;
        bullet = transform.GetChild(0).gameObject;
        scoreText = GameObject.Find("Canvas/Score");
    }

    // Update is called once per frame
    void Update()
    {
        if(WASD)
        {
            movement = new Vector2(0, Input.GetAxisRaw("Vertical(WASD)"));

            if(Input.GetKey(KeyCode.A))
            {
                direction = 1;
            }
            else if(Input.GetKey(KeyCode.D))
            {
                direction = -1;
            }
            else
            {
                direction = 0;
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                FireBullet();
            }
        }
        else
        {
            movement = new Vector2(0, Input.GetAxisRaw("Vertical(Arrow Keys)"));

            if(Input.GetKey(KeyCode.LeftArrow))
            {
                direction = 1;
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                direction = -1;
            }
            else
            {
                direction = 0;
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                FireBullet();
            }
        }
    }

    void FixedUpdate()
    {
        MoveShip(movement);
        rb.AddForce((Vector2.zero - (Vector2)transform.position) * gravitationalSpeed);
    }

    void MoveShip(Vector2 vel)
    {
        rb.AddRelativeForce(vel * speed);
        rb.AddTorque(direction * rotationSpeed);
    }

    void FireBullet()
    {
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
        temp.GetComponent<Rigidbody2D>().isKinematic = false;
        temp.GetComponent<BulletController>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Star" && this.name == "Player1Ship")
        {
            scoreText.GetComponent<ScoreTracker>().player2Score += 1;
            SceneManager.LoadScene(0);
        }
        else if(col.gameObject.name == "Star" && this.name == "Player2Ship")
        {
            scoreText.GetComponent<ScoreTracker>().player1Score += 1;
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player2Bullet(Clone)" && this.name == "Player1Ship")
        {
            scoreText.GetComponent<ScoreTracker>().player2Score += 1;
            SceneManager.LoadScene(0);
        }
        else if(col.gameObject.name == "Player1Bullet(Clone)" && this.name == "Player2Ship")
        {
            scoreText.GetComponent<ScoreTracker>().player1Score += 1;
            SceneManager.LoadScene(0);
        }
    }
}
