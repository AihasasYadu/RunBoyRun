using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private PoolingController poolCtrlr;
    [SerializeField] private PlatformManager platformManager;
    [SerializeField] private CharacterController gameChar;
    [SerializeField] private RectTransform panel;
    [SerializeField] private Button startButton;

    private int timer;
    private Coroutine timerCo;
    private float distTravelled;
    private Transform initialPosition;

    private void Start()
    {
        EventsManager.PlayerDead += GameEnded;
        startButton.onClick.AddListener(StartGame);
        timer = 0;
    }

    public void StartGame()
    {
        initialPosition = gameChar.transform;
        platformManager.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        gameChar.enabled = true;
        timerCo = StartCoroutine(Timer());
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
        distTravelled = Mathf.Abs(gameChar.transform.position.z - initialPosition.position.z);
    }

    private void OnDestroy()
    {
        EventsManager.PlayerDead -= GameEnded;
    }
}
