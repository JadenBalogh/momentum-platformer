using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private CanvasGroup hudCanvas;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    public bool IsGameOver = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (IsGameOver) return;
        timeText.text = Time.timeSinceLevelLoad.ToString("#.#");
    }

    public static void EndGame(bool won) => instance.IEndGame(won);
    private void IEndGame(bool won)
    {
        hudCanvas.alpha = 1;
        IsGameOver = true;
        timeText.color = won ? Color.green : Color.red;
        gameOverText.text = won ? "You Won" : "You Lost";
        Time.timeScale = 0;
    }
}
