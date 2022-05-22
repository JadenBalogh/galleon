using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private float fadeRate = 1f;

    private bool isGameOver = false;
    private bool isFading = false;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (isGameOver)
        {
            canvasGroup.alpha += fadeRate * Time.deltaTime;
        }

        if (GameManager.IsStarted && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void EndGame()
    {
        isGameOver = true;
    }
}
