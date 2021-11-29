using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D playerRigibody;

    [SerializeField]
    private LayerMask obstaclesLayer, scoreLayer;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int score = 0;
    private Vector2 playerStartPos;
    private float startRotation;

    private void Awake()
    {
        playerStartPos = this.transform.position;
        startRotation = this.transform.eulerAngles.z;
        EventManager.OnResetClicked += OnPlayAgain;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            playerRigibody.velocity = new Vector2(0, 5);
        }

        this.transform.transform.localEulerAngles = new Vector3(0, 0, playerRigibody.velocity.y + startRotation);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((obstaclesLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
        {
            if (score > PlayerPrefs.GetInt("highscore", 0))
            {
                PlayerPrefs.SetInt("highscore", score);
            }

            EventManager.OnPlayerDied();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if ((scoreLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
        {
            if (this.gameObject.activeSelf)
            {
                score++;
                scoreText.text = "Score: " + score.ToString();
            }
        }
    }

    private void OnPlayAgain()
    {
        this.transform.position = playerStartPos;
        playerRigibody.velocity = new Vector2(0, 0);
        score = 0;
        scoreText.text = "Score: " + score;
    }



}
