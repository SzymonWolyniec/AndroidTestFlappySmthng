using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public Rigidbody2D playerRigibody;
    public LayerMask obstacleLayer;
    public LayerMask scoreLayer;

    public TextMeshProUGUI scoreText;
    public GameLifecycle GameScriptHandler;

    int score = 0;

    private Vector2 playerStartPos;

    private void Awake()
    {
        playerStartPos = this.transform.position;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            playerRigibody.velocity = new Vector2(0, 5);
        }

        if (Input.touchCount > 0)
        {
            playerRigibody.velocity = new Vector2(0, 5);
        }

        this.transform.transform.localEulerAngles = new Vector3(0, 0, playerRigibody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((obstacleLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
        {
            if (score > PlayerPrefs.GetInt("highscore", 0))
            {
                PlayerPrefs.SetInt("highscore", score);
            }
            GameScriptHandler.OnDied();
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

    public void OnPlayAgain()
    {
        this.transform.position = playerStartPos;
        playerRigibody.velocity = new Vector2(0, 0);
        score = 0;
        scoreText.text = "Score: " + score;
    }



}
