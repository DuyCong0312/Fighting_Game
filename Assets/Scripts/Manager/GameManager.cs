using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player_1")]
    [SerializeField] private PlayerHealth player01Health;
    [SerializeField] private Toggle[] player1Wins;
    private int player1RoundsWon = 0;

    [Header("Player_2")]
    [SerializeField] private PlayerHealth player02Health;
    [SerializeField] private Toggle[] player2Wins;
    private int player2RoundsWon = 0;

    [Header("Game UI Settings")]
    [SerializeField] private UiTimeCount time;
    [SerializeField] private GameObject panelGameSetPerRound;
    [SerializeField] private TextMeshProUGUI gameSetPerRound;
    [SerializeField] private GameObject panelGameSetFinal;
    [SerializeField] private TextMeshProUGUI gameSetFinal;
    [SerializeField] private GameObject panelPauseGame;

    public bool gameEnded = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        panelGameSetPerRound.SetActive(false);
        panelGameSetFinal.SetActive(false);
        panelPauseGame.SetActive(false);
    }

    private void Update()
    {
        PauseGame();
        if (!gameEnded && (player01Health.currentHealth <= 0 || player02Health.currentHealth <= 0))
        {
            GameSet(); 
        }
    }
    public void GameSetTime()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (player01Health.currentHealth > player02Health.currentHealth)
        {
            gameSetPerRound.text = ("You win");
            AwardWinToPlayer(1);
            Debug.Log("You Win");
        }
        else if(player01Health.currentHealth < player02Health.currentHealth)
        {
            gameSetPerRound.text = ("You lose");
            AwardWinToPlayer(2);
            Debug.Log("You Lose");
        }
        else 
        {
            gameSetPerRound.text = ("Draw");
            SetPanel();
            StartCoroutine(StartNewRound());
            Debug.Log("Draw");
        }
    }

    private void GameSet()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (player01Health.currentHealth <= 0)
        {
            gameSetPerRound.text = ("You lose");
            AwardWinToPlayer(2);
            Debug.Log("You Lose");
        }
        else if (player02Health.currentHealth <= 0)
        {
            gameSetPerRound.text = ("You win");
            AwardWinToPlayer(1);
            Debug.Log("You Win");
        }
        else if (player01Health.currentHealth <= 0 && player02Health.currentHealth <= 0)
        {
            gameSetPerRound.text = "Draw";
            SetPanel();
            StartCoroutine(StartNewRound());
            Debug.Log("Draw");
        }
    }

    private void SetPanel()
    {
        panelGameSetPerRound.SetActive(true);
    }

    private void AwardWinToPlayer(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Wins[player1RoundsWon].isOn = true;
            player1RoundsWon++;
        }
        else
        {
            player2Wins[player2RoundsWon].isOn = true;
            player2RoundsWon++;
        }

        if (player1RoundsWon >= 2 || player2RoundsWon >= 2)
        {
            gameEnded = true;
            panelGameSetFinal.SetActive(true);
            panelGameSetPerRound.SetActive(false);
            gameSetFinal.text = ("Player" + playerNumber + "Win!");
            Debug.Log("Match Over Player" + playerNumber + "Win!");
            return;
        }
        else
        {
            panelGameSetPerRound.SetActive(true);
        }

        StartCoroutine(StartNewRound());
    }
    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(2);
        ResetPlayersHealth();
        ResetTime();
        panelGameSetPerRound.SetActive(false);
        gameEnded = false;
    }

    private void ResetPlayersHealth()
    {
        player01Health.ResetHealth();
        player02Health.ResetHealth();
    }

    private void ResetTime()
    {
        time.ResetTime();
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            panelPauseGame.SetActive(true);
        }
    }
}
