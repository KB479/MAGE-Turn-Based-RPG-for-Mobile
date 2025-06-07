using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler<OnStateChangedEventArgs> OnTurnChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    [SerializeField] private Enemy enemy; 

    public enum State
    {
        PlayersTurn,
        EnemysTurn,
        PlayerWon,
        EnemyWon,
    }

    private State state; 


    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        state = State.PlayersTurn;

        Player.Instance.OnAttack += Player_OnAttack;
        Player.Instance.OnHeal += Player_OnHeal;
        enemy.OnEnemyAttack += Enemy_OnEnemyAttack;

        //Fast kill dinlemesi için:
        FastKillControlUI.Instance.OnPlayerFastKill += FastKillControlUI_OnPlayerFastKill;
        FastKillControlUI.Instance.OnEnemyFastKill += FastKillControlUI_OnEnemyFastKill;


    }

    //Fast kill listening functions:  

    private void FastKillControlUI_OnEnemyFastKill(object sender, EventArgs e)
    {
        state = State.PlayerWon;
    }

    private void FastKillControlUI_OnPlayerFastKill(object sender, EventArgs e)
    {
        state = State.EnemyWon;
    }

    //Normal Attack listening functions: 

    private void Enemy_OnEnemyAttack(object sender, EventArgs e)
    {
        state = State.PlayersTurn; 
    }
    private void Player_OnHeal(object sender, EventArgs e)
    {
        state = State.EnemysTurn;
    }
    private void Player_OnAttack(object sender, EventArgs e)
    {
        state = State.EnemysTurn;
    }

    //IsPlayerTurn fonksiyonunu attack button interactable kontrolü için koydum, fakat player da saldýrmadan can player sorgusu yapýyor
    //benzer iþlem ikiye bölünmüþ gibi, kirli bir kod bu, düzelt sonra! 

    public bool IsPlayerTurn()
    {
        if (state == State.PlayersTurn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Update()
    {

        switch (state)
        {
            case State.PlayersTurn:

                OnTurnChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                }); 

                if(Player.Instance.IsPlayerAlive() == false)
                {
                    state = State.EnemyWon;
                }

                break; 
            
            case State.EnemysTurn:
                
                OnTurnChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                if(enemy.IsEnemyAlive() == false)
                {
                    state = State.PlayerWon;
                }

                break;

            case State.PlayerWon:

                OnTurnChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                break;

            case State.EnemyWon:

                OnTurnChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                break;
        }
    
    }








}
