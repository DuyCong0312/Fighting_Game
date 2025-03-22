using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player_1")]
    [SerializeField] private PlayerHealth player01Health;
    [SerializeField] private Toggle[] player1Wins;
    private int player1RoundsWon = 0;

    [Header("Player_2")]
    [SerializeField] private PlayerHealth player02Health;
    [SerializeField] private Toggle[] player2Wins;
    private int player2RoundsWon = 0;

    [Header("Game UI Settings")]
    [SerializeField] private HealthBar[] healthBars;
    [SerializeField] private GameObject panelGameSet;
    [SerializeField] private TextMeshProUGUI gameSet;

    private bool gameEnded = false;

    private void Start()
    {
        PlayerHealth[] players = FindObjectsOfType<PlayerHealth>();

        if (players.Length == 2)
        {
            if (players[0].transform.position.x < players[1].transform.position.x)
            {
                player01Health = players[0];
                player02Health = players[1];
            }
            else
            {
                player01Health = players[1];
                player02Health = players[0];
            }

            player01Health.SetHealthBar(healthBars[0]);
            player02Health.SetHealthBar(healthBars[1]);
        }
        else
        {
            Debug.LogError("Not found enough player!");
        }
        panelGameSet.SetActive(false);
    }

    private void Update()
    {
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
            gameSet.text = ("You win");
            AwardWinToPlayer(1);
            Debug.Log("You Win");
        }
        else if(player01Health.currentHealth < player02Health.currentHealth)
        {
            gameSet.text = ("You lose");
            AwardWinToPlayer(2);
            Debug.Log("You Lose");
        }
        else 
        {
            gameSet.text = ("Draw");
            Debug.Log("Draw");
        }
        SetPanel();
    }

    private void GameSet()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (player01Health.currentHealth <= 0)
        {
            gameSet.text = ("You lose");
            AwardWinToPlayer(2);
            Debug.Log("You Lose");
        }
        else if (player02Health.currentHealth <= 0)
        {
            gameSet.text = ("You win");
            AwardWinToPlayer(1);
            Debug.Log("You Win");
        }
        else if (player01Health.currentHealth <= 0 && player02Health.currentHealth <= 0)
        {
            gameSet.text = "Draw";
            Debug.Log("Draw");
        }
        SetPanel();
    }

    private void SetPanel()
    {
        panelGameSet.SetActive(true);
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
            gameSet.text = ("Match Over Player" + playerNumber + "Win!");
            Debug.Log("Match Over Player" + playerNumber + "Win!");
            return;
        }

        StartCoroutine(StartNewRound());
    }
    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(2);
        ResetPlayersHealth();
        panelGameSet.SetActive(false);
        gameEnded = false;
    }

    private void ResetPlayersHealth()
    {
        player01Health.ResetHealth();
        player02Health.ResetHealth();
    }
}
