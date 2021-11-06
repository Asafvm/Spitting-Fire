using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]

public class UiResize : MonoBehaviour
{
    private RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rect.sizeDelta = new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
