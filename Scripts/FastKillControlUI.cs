using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastKillControlUI : MonoBehaviour
{
    public static FastKillControlUI Instance { get; private set; }

    public event EventHandler OnPlayerFastKill;
    public event EventHandler OnEnemyFastKill;

    [SerializeField] private Button killPlayerButton; 
    [SerializeField] private Button killEnemyButton;
    [SerializeField] private Enemy enemy;
    // enemy referans ile eriþildi, çirkin kod, enemy singleton yap


    private void Awake()
    {
        Instance = this;

        killPlayerButton.interactable = true;
        killEnemyButton.interactable = true;

    }

    public void Start()
    {

        killPlayerButton.onClick.AddListener(() =>
        {
            Player.Instance.KillPlayer();
            OnPlayerFastKill?.Invoke(this, EventArgs.Empty);

            killPlayerButton.interactable = false;
            killEnemyButton.interactable = false;

        });


        killEnemyButton.onClick.AddListener(() =>
        {
            enemy.KillEnemy();
            OnEnemyFastKill?.Invoke(this, EventArgs.Empty);

            killPlayerButton.interactable = false;
            killEnemyButton.interactable = false;

        });

    }





}
