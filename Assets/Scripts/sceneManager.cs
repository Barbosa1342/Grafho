using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour
{
    public TMP_Text finalScoreText;
    public TMP_Text scoreText;
    static public sceneManager sceneManagerObj;

    [SerializeField] GameObject gameHUD;
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] healthSystem healthScript;

    [SerializeField] Image[] healthContainer;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    private void Awake()
    {
        sceneManagerObj = this;
    }

    private void Start()
    {
        if (gameHUD.activeSelf == false)
        {
            gameHUD.SetActive(true);
        }

        if (gameOverScreen.activeSelf == true)
        {
            gameOverScreen.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void ResetGame()
    {
        pointSystem.restartPoints();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOverScreen()
    {
        gameHUD.SetActive(false);
        gameOverScreen.SetActive(true);

        int points = pointSystem.Points;
        finalScoreText.text = points.ToString() + " Points";
    }

    public void updateScore()
    {
        int points = pointSystem.Points;
        scoreText.text = points.ToString() + " Points";
    }

    public void updateHeart()
    {
        for (int i = 0; i < healthContainer.Length; i++)
        {
            if (i < healthScript.CurrentHealth)
            {
                healthContainer[i].sprite = fullHeart;
            }
            else
            {
                healthContainer[i].sprite = emptyHeart;
            }
        }
    }
}
