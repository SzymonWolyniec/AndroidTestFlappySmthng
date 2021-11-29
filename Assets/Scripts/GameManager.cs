using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI startHighScore, playAgainHighScore;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private List<CanvasGroup> canvasesGroups;

    private Queue<IEnumerator> canvasCoroutinesQueue = new Queue<IEnumerator>();

    void Start()
    {
        Application.targetFrameRate = 90;
        startHighScore.text = "High score: " + PlayerPrefs.GetInt("highscore", 0);

        StartCoroutine(CanvasCoroutineCoordinator());
    }

    private void OnEnable()
    {
        EventManager.OnStartClicked += OnStart;
        EventManager.OnDied += OnDied;
        EventManager.OnResetClicked += OnReset;
    }

    private void OnDisable()
    {
        EventManager.OnStartClicked -= OnStart;
        EventManager.OnDied += OnDied;
        EventManager.OnResetClicked -= OnReset;
    }

    private IEnumerator CanvasCoroutineCoordinator()
    {
        while (true)
        {
            while (canvasCoroutinesQueue.Count > 0)
                yield return StartCoroutine(canvasCoroutinesQueue.Dequeue());
            yield return null;
        }
    }

    private void OnStart()
    {
        canvasesGroups[0].interactable = false;
        canvasCoroutinesQueue.Enqueue(ChangeCanvas(canvasesGroups[0], canvasesGroups[1]));
        player.SetActive(true);
    }

    private void OnDied()
    {
        canvasesGroups[1].interactable = false;
        playAgainHighScore.text = "High score: " + PlayerPrefs.GetInt("highscore", 0);
        canvasCoroutinesQueue.Enqueue(ChangeCanvas(canvasesGroups[1], canvasesGroups[2]));
        player.SetActive(false);
    }

    private void OnReset()
    {
        canvasesGroups[2].interactable = false;
        canvasCoroutinesQueue.Enqueue(ChangeCanvas(canvasesGroups[2], canvasesGroups[1]));

        player.SetActive(true);
    }

    private IEnumerator ChangeCanvas(CanvasGroup oldCanvas, CanvasGroup newCanvas)
    {
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
