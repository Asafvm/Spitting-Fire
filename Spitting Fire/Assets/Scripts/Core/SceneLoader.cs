using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(int index)
    {
        if (index > SceneManager.sceneCount-1) return;
        SceneManager.LoadScene(index);
    }

    internal void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
    }
}
