using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button startButton,
                                    pauseButton,
                                    resetButton;
    [SerializeField] private RectTransform pausedPanel,
                                           startPanel;
    [SerializeField] private Sprite pauseButtonSprite,
                                    resumeButtonSprite;

    private void Awake()
    {
        pausedPanel.gameObject.SetActive(false);
    }

    private void Start()
    {
        startButton.onClick.AddListener(Begin);
        pauseButton.onClick.AddListener(PauseGame);
        resetButton.onClick.AddListener(RestartGame);
    }

    private void Begin()
    {
        GameManager.Instance.StartGame();
        startButton.gameObject.SetActive(false);
        startPanel.gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        EventsManager.Instance.ToggleGamePause();
        pauseButton.GetComponent<Image>().sprite = resumeButtonSprite;
        pauseButton.onClick.RemoveListener(PauseGame);
        pausedPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
        pauseButton.onClick.AddListener(ResumeGame);
    }

    private void ResumeGame()
    {
        EventsManager.Instance.ToggleGamePause();
        pauseButton.GetComponent<Image>().sprite = pauseButtonSprite;
        pauseButton.onClick.RemoveListener(ResumeGame);
        pausedPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
        pauseButton.onClick.AddListener(PauseGame);
    }

    private void RestartGame()
    {
        EventsManager.Instance.RestartGame();
        pauseButton.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
