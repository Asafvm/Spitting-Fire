using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Make an image appear and disappear (works with UI buttons)
/// </summary>
public class ImagePopper : MonoBehaviour
{
    private bool show = false;
    private Coroutine activeCoroutine;
    private Image image;


    private void Awake()
    {
        image = GetComponent<Image>();
        
    }
    private void Start()
    {
        image.enabled = show;
    }
    public void TriggerImage()
    {
        show = !show;
        setImage(show);
    }



    public void setImage(bool show)
    {
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);
        if (show)
        {
            activeCoroutine = StartCoroutine(PopImage(1));
        }
        else
        {
            activeCoroutine = StartCoroutine(PopImage(0));
        }
    }

    private IEnumerator PopImage(int target)
    {
        image.enabled = target == 1;
        yield return null;
    }
}
