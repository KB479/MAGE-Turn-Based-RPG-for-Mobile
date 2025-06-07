using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionUI : MonoBehaviour
{
    public static PlayerActionUI Instance { get; private set; }

    public event EventHandler OnAttackButtonPressed;
    public event EventHandler OnHealButtonPressed;

    private bool isAttacking; 
    [SerializeField] private Button attackButton;
    [SerializeField] private Button healButton;
 

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        attackButton.onClick.AddListener(() =>
        {
            OnAttackButtonPressed?.Invoke(this, EventArgs.Empty);  
            
        });

        
        healButton.onClick.AddListener(() =>
        {
            OnHealButtonPressed?.Invoke(this, EventArgs.Empty);   
        });

    }

    private void Update()
    {
        //t�klan�nca event yollan�p player yorumlay�p ona g�re sald�racak, interactable kontol� ile hem
        //gereksiz event yollam�ycaz, hem de UI daha iyi g�z�kecek.

        if (GameManager.Instance.IsPlayerTurn())
        {
            attackButton.interactable = true;
            healButton.interactable = true;
        }
        else
        {
            attackButton.interactable = false;
            healButton.interactable = false;
        }

    }

}
