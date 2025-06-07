using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class MagicSketchUI : MonoBehaviour
{

    [SerializeField] private Button sketchButton;
    [SerializeField] private TextMeshProUGUI tutorial;

    private bool showStatus; 


    private void Awake()
    {
        sketchButton.interactable = true;
        showStatus = false;
        tutorial.gameObject.SetActive(false);

        sketchButton.onClick.AddListener(() =>
        {
            ShowHide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnTurnChanged += GameManager_OnTurnChanged;
    }

    private void GameManager_OnTurnChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        // asl�nda burda oyun bitti mi kontrol� yap�lmas� laz�m fakat o state eklenmedi daha
        // o y�zden oyun bitti mi kontrol�n� bu �ekilde dolayl� yoldan yapt�m, d�zeltilecek!

        if (e.state == GameManager.State.EnemyWon || e.state == GameManager.State.PlayerWon)
        {
            sketchButton.interactable = false;
            tutorial.gameObject.SetActive(false);
            showStatus = false;
        }
        
    }

    private void ShowHide()
    {
        if (showStatus == false)
        {
            tutorial.gameObject.SetActive(true);
            showStatus = true;
        }
        else
        {
            tutorial.gameObject.SetActive(false);
            showStatus= false;
        }

    }




}
