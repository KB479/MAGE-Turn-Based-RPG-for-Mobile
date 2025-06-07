using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;


    public void Awake()
    {

        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene"); 

        });

        //settings ekleyince...

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });


        Time.timeScale = 1f; 

    }




}
