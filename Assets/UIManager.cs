using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private Text gameOverMenuText = null;
    [SerializeField] private Text tapText = null;
    [SerializeField] private Text scoreText = null;

    [SerializeField] private GameObject gameOverMenu = null;
    [SerializeField] private GameObject tapMenu = null;

    [SerializeField] private Slider progressSlider = null;

    bool isFinish = false;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void OnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnOpenGameOverMenu(string textMenu)
    {
        gameOverMenu.SetActive(true);
        gameOverMenuText.text = textMenu;
    }

    public void OnOpenTapInterface()
    {
        tapMenu.SetActive(true);
    }
    public void OnUpdateTapInterface(int tapCount)
    {

        tapText.text = $"{tapCount}";
    }
    public void OnUpdateScore(int score)
    {

        scoreText.text = $"Score: {score}";
    }
    public void OnUpdateSliderProgress (float rate)
    {
        if (rate > 0.99f)
        {
            progressSlider.value = 1;
            isFinish = true;
        }
        if (!isFinish) progressSlider.value = rate;
    }
}
