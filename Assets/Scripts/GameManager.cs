using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    [SerializeField] private TextMeshProUGUI timerText,
                                             scoreText,
                                             distTravelledText;
    [SerializeField] private PoolingController poolCtrlr;
    [SerializeField] private PlatformManager platformManager;
    [SerializeField] private CharacterController gameChar;
    [SerializeField] private RectTransform pausePanel,
                                           gameOverPanel;

    private bool gamePaused;
    private int coinsCollected;
    private int timer;
    private Coroutine timerCo;
    private int distTravelled;
    private Vector3 initialPosition;

    public bool isGamePaused { get { return gamePaused; } }

    private void Start()
    {
        coinsCollected = 0;
        EventsManager.PlayerDead += GameEnded;
        EventsManager.TogglePause += PauseToggle;
        EventsManager.CoinCollected += UpdateCoinData;
        EventsManager.ResetGame += DestroyGameObject;
        timer = 0;
        gamePaused = false;
    }

    private void UpdateCoinData()
    {
        coinsCollected++;
    }

    public void StartGame()
    {
        initialPosition = gameChar.transform.position;
        Debug.Log(initialPosition);
        platformManager.gameObject.SetActive(true);
        pausePanel.gameObject.SetActive(false);
        gameChar.enabled = true;
        timerCo = StartCoroutine(Timer());
    }
    
    private void PauseToggle()
    {
        gamePaused = !gamePaused;
    }

    private IEnumerator Timer()
    {
        while (this)
        {
            yield return new WaitForSeconds(1f);
            timer++;
            timerText.SetText(timer.ToString());
        }
    }

    private void GameEnded()
    {
        StopCoroutine(timerCo);
        distTravelled = (int)(gameChar.transform.position.z - initialPosition.z);
        distTravelled = Mathf.Abs(distTravelled);
        Debug.Log(gameChar.transform.position + " : Dist : " + distTravelled);
        scoreText.SetText("Score : " + coinsCollected.ToString());
        distTravelledText.SetText("Distance : " + distTravelled.ToString());
        gameChar.gameObject.SetActive(false);
        platformManager.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(true);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventsManager.ResetGame -= DestroyGameObject;
        EventsManager.PlayerDead -= GameEnded;
    }
}
