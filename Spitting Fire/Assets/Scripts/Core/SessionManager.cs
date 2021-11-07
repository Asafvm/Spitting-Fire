using System;
using System.Collections;
using System.Collections.Generic;

using Cinemachine;

using TMPro;

using UnityEngine;

/// <summary>
/// Handles score updating, starting and ending game sessions and interacting with SceneLoader
/// </summary>
public class SessionManager : MonoBehaviour
{
    ScoreHandler scoreHandler;
    SceneLoader sceneLoader;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] float targetScore = 10;
    [SerializeField] float gameOverDelay = 1f;

    private void Awake()
    {
        scoreHandler = FindObjectOfType<ScoreHandler>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }


    void Start()
    {
        StartGame();
    }

    public void TargetDestroyed(string tag)
    {
        if (tag.Equals("Player"))
            PlayerDestroyed();
        else
            EnemyDestroyed();

    }

    private void EnemyDestroyed()
    {
        scoreHandler.AddScore();
        if (scoreHandler.GetScore() == targetScore)
            StartCoroutine(GameOverSequence());
    }
    private void PlayerDestroyed()
    {
        StartCoroutine(GameOverSequence());
    }


    private void StartGame()
    {
        
        
        gameOverPanel.SetActive(false);
        scoreHandler.Restart();
        Time.timeScale = 1;
    }

    
    private IEnumerator GameOverSequence()
    {
        //stop following target
        CinemachineVirtualCamera cam = FindObjectOfType<CinemachineVirtualCamera>();
        cam.Follow = null;
        yield return new WaitForSeconds(gameOverDelay);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; //freeze time
    }

    public void QuitToMainScreen()
    {
        sceneLoader.LoadScene(0);
    }

    public void RestartScene()
    {
        sceneLoader.ReloadScene();
    }
}
