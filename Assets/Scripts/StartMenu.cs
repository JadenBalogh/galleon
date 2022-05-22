using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private float fadeRate = 1f;

    private bool isFading = false;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (canvasGroup.alpha <= 0) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFading = true;
            GameManager.StartGame();
        }

        if (isFading)
        {
            canvasGroup.alpha -= fadeRate * Time.deltaTime;
        }
    }
}
