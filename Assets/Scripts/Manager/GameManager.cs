using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth player01Health;
    [SerializeField] private PlayerHealth player02Health;
    [SerializeField] private GameObject panelGameSet;
    [SerializeField] private TextMeshProUGUI gameSet;

    private bool gameEnded = false;

    private void Start()
    {
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
            Debug.Log("You Win");
        }
        else if(player01Health.currentHealth < player02Health.currentHealth)
        {
            gameSet.text = ("You lose");
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
            Debug.Log("You Lose");
        }
        else if (player02Health.currentHealth <= 0)
        {
            gameSet.text = ("You win");
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
}
