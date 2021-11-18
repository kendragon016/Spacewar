using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VIPController : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 2f;
    float range = 0.5f;
    float maxDistance = 6f;
    Vector2 wayPoint;
    private Vector2 screenBounds;
    GameObject bullet;
    GameObject scoreText;
    public GameObject explode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = GameObject.Find("Canvas/Score");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player2Bullet(Clone)" && this.name == "Player1VIP")
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
        else if(col.gameObject.name == "Player1Bullet(Clone)" && this.name == "Player2VIP")
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        SetNewDestination();
    }
}
