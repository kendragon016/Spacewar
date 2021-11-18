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
    public GameObject bomb;
    float spawnTime = 3.0f;
    private Vector2 screenBounds;
    GameObject VIP;
    public GameObject laser;
    public GameObject explode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravitationalSpeed = 0.25f;
        bullet = transform.GetChild(0).gameObject;
        scoreText = GameObject.Find("Canvas/Score");

        if(scoreText.GetComponent<ScoreTracker>().player2Score > 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            bomb = transform.GetChild(0).gameObject;
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            InvokeRepeating ("SpawnBomb", spawnTime, spawnTime);
        }
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
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            rb.AddForce((Vector2.zero - (Vector2)transform.position) * gravitationalSpeed);
        }
    }

    void MoveShip(Vector2 vel)
    {
        rb.AddRelativeForce(vel * speed);
        rb.AddTorque(direction * rotationSpeed);
    }

    void FireBullet()
    {
        laser.GetComponent<AudioSource>().Play();
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
        temp.GetComponent<Rigidbody2D>().isKinematic = false;
        temp.GetComponent<BulletController>().enabled = true;
    }

    void SpawnBomb()
    {
        GameObject temp = Instantiate(bomb) as GameObject;
        temp.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
    }

    // IEnumerator BombSet()
    // {
    //     while(true)
    //     {
    //         yield return new WaitForSeconds(spawnTime);
    //         SpawnBomb();
    //     }
    // }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Star" && this.name == "Player1Ship")
        {
            explode.GetComponent<AudioSource>().Play();
            if((scoreText.GetComponent<ScoreTracker>().player2Score < 2) && (scoreText.GetComponent<ScoreTracker>().player1Score < 2))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                scoreText.GetComponent<ScoreTracker>().player2Score += 1;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                scoreText.GetComponent<ScoreTracker>().player2Score = 0;
                scoreText.GetComponent<ScoreTracker>().player1Score = 0;
            }
        }
        else if(col.gameObject.name == "Star" && this.name == "Player2Ship")
        {
            explode.GetComponent<AudioSource>().Play();
            if((scoreText.GetComponent<ScoreTracker>().player2Score < 2) && (scoreText.GetComponent<ScoreTracker>().player1Score < 2))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                scoreText.GetComponent<ScoreTracker>().player1Score += 1;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                scoreText.GetComponent<ScoreTracker>().player2Score = 0;
                scoreText.GetComponent<ScoreTracker>().player1Score = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            if(col.gameObject.name == "Player2Bullet(Clone)" && (this.name == "Player1Ship" || col.gameObject.name == "Player1VIP"))
            {
                if((scoreText.GetComponent<ScoreTracker>().player2Score < 2) && (scoreText.GetComponent<ScoreTracker>().player1Score < 2))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    scoreText.GetComponent<ScoreTracker>().player2Score += 1;
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    scoreText.GetComponent<ScoreTracker>().player2Score = 0;
                    scoreText.GetComponent<ScoreTracker>().player1Score = 0;
                }
            }
            else if(col.gameObject.name == "Player1Bullet(Clone)" && (this.name == "Player2Ship" || col.gameObject.name == "Player2VIP"))
            {
                if((scoreText.GetComponent<ScoreTracker>().player2Score < 2) && (scoreText.GetComponent<ScoreTracker>().player1Score < 2))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    scoreText.GetComponent<ScoreTracker>().player1Score += 1;
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    scoreText.GetComponent<ScoreTracker>().player2Score = 0;
                    scoreText.GetComponent<ScoreTracker>().player1Score = 0;
                }
            }
        }
    }
}
