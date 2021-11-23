using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameLifecycle : MonoBehaviour
{
    public TextMeshProUGUI startHighScore;
    public TextMeshProUGUI playAgainHighScore;

    public GameObject player;

    public List<CanvasGroup> canvasesGroups;

    private Obstacles obstaclesScript;

    void Start()
    {
        Application.targetFrameRate = 60;
        startHighScore.text = "High score: " + PlayerPrefs.GetInt("highscore", 0);

        obstaclesScript = this.GetComponent<Obstacles>();
        obstaclesScript.obstaclePrefabsList = Resources.LoadAll<GameObject>("ObstaclesFramesPrefabs").ToList();
    }

    public void OnStartGameClick()
    {
        StartCoroutine(ChangeCanvas(canvasesGroups[0], canvasesGroups[1]));
        obstaclesScript.gameNumber++;
        StartCoroutine(obstaclesScript.SpawnObstacles());
        player.SetActive(true);
    }

    public void OnDied()
    {
        playAgainHighScore.text = "High score: " + PlayerPrefs.GetInt("highscore", 0);
        StartCoroutine(ChangeCanvas(canvasesGroups[1], canvasesGroups[2]));
        obstaclesScript.gameNumber++;
        player.SetActive(false);
    }

    public void OnPlayAgainClick()
    {
        player.GetComponent<PlayerControls>().OnPlayAgain();
        StartCoroutine(ChangeCanvas(canvasesGroups[2], canvasesGroups[1]));

        foreach (Transform singleObstacle in obstaclesScript.obstaclesContainer)
        {
            singleObstacle.gameObject.SetActive(false);
        }

        obstaclesScript.gameNumber++;
        StartCoroutine(obstaclesScript.SpawnObstacles());
        player.SetActive(true);
    }

    IEnumerator ChangeCanvas(CanvasGroup oldCanvas, CanvasGroup newCanvas)
    {
        oldCanvas.interactable = false;
        oldCanvas.blocksRaycasts = false;

        newCanvas.gameObject.SetActive(true);
        newCanvas.interactable = true;
        newCanvas.blocksRaycasts = true;

        for (float i = 1; i >= 0; i = i - 0.03f)
        {
            oldCanvas.alpha = i;
            newCanvas.alpha = 1 - i;
            yield return new WaitForFixedUpdate();
        }

        oldCanvas.gameObject.SetActive(false);
        oldCanvas.alpha = 0;
        newCanvas.alpha = 1;
    }
}
