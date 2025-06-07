using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI playerWinText;
    [SerializeField] private TextMeshProUGUI playerLostText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {

        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });

        //settings ekleyince...

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenuScene"); 
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

    }

    private void Start()
    {

        GameManager.Instance.OnTurnChanged += GameManager_OnTurnChanged;
        //Event tur deðiþimi isimli fakat oyun kontrolünü de burada tutuyorum, çok çirkin bunu düzelt.

        Hide(); 

    }

    private void GameManager_OnTurnChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        if (e.state == GameManager.State.PlayerWon)
        {
            Show(); 
            playerLostText.gameObject.SetActive(false);
        }

        if (e.state == GameManager.State.EnemyWon)
        {
            Show();
            playerWinText.gameObject.SetActive(false);

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
