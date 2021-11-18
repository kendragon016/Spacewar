using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform rotationCenter;
    float rotationRadius = 1.5f, angularSpeed = 2f;
    float posX, posY, angle = 0f;
    GameObject bullet;
    GameObject scoreText;
    public GameObject explode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = GameObject.Find("Canvas/Score");
    }

    // Update is called once per frame
    void Update()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360f)
            angle = 0f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player2Bullet(Clone)" && this.name == "Player1Guard1")
        {
            if((scoreText.GetComponent<ScoreTracker>().player2Score < 2) && (scoreText.GetComponent<ScoreTracker>().player1Score < 2))
            {
                explode.GetComponent<AudioSource>().Play();
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
        else if(col.gameObject.name == "Player1Bullet(Clone)" && this.name == "Player2Guard1")
        {
            if((scoreText.GetComponent<ScoreTracker>().player2Score < 2) && (scoreText.GetComponent<ScoreTracker>().player1Score < 2))
            {
                explode.GetComponent<AudioSource>().Play();
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
