using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public Rigidbody2D playerRigibody;
    public Transform groundCheckerTransform;
    public float groundCheckRadius;
    public LayerMask obstacleLayer;
    public LayerMask scoreLayer;

    public TextMeshProUGUI scoreText;

    int score = 0;


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            playerRigibody.velocity = new Vector2(playerRigibody.velocity.x, 5);
        }

        if (Input.touchCount > 0)
        {
            playerRigibody.velocity = new Vector2(playerRigibody.velocity.x, 5);
        }


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((obstacleLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
        {
            Time.timeScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if ((scoreLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
        }
    }



}
