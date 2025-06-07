using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnStatusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playersTurnText;
    [SerializeField] private TextMeshProUGUI enemysTurnText;


    private void Start()
    {
        Hide(); 

        GameManager.Instance.OnTurnChanged += GameManager_OnTurnChanged;


    }

    private void GameManager_OnTurnChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        if (e.state == GameManager.State.PlayersTurn)
        {
            Show();
            playersTurnText.gameObject.SetActive(true);
            enemysTurnText.gameObject.SetActive(false);

        }
        else if (e.state == GameManager.State.EnemysTurn)
        {
            Show();
            enemysTurnText.gameObject.SetActive(true);
            playersTurnText.gameObject.SetActive(false);

        }
        else
        {
            Hide();
        }
    }



    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }






}



