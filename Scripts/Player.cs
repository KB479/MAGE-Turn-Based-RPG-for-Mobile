using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHasHealthBar
{
    public static Player Instance { get; private set; }

    public event EventHandler<IHasHealthBar.OnHealthChangedEventArgs> OnHealthChanged; 
    public event EventHandler OnAttack;
    public event EventHandler OnHeal; 

    private float playerHealthMax = 100f;
    private float playerHealth; 
    private bool canPlayerTakeAction;
    private bool isAttacking; 
    //private bool isAttackButtonPressed = false; 

    [SerializeField] private ParticleSystem hitTakenParticle;
    [SerializeField] private ParticleSystem healingParticle;
    [SerializeField] private Enemy enemy; 
    //Tek tek bütün enemyler için eklemiycez elbette, detay notionda!


    private void Awake()
    {
        playerHealth = playerHealthMax; 
        Instance = this;
    }

    private void Start()
    {
        enemy.OnEnemyAttack += Enemy_OnEnemyAttack;
        GameManager.Instance.OnTurnChanged += GameManager_OnTurnChanged;
        PlayerActionUI.Instance.OnAttackButtonPressed += PlayerActionUI_OnAttackButtonPressed;
        PlayerActionUI.Instance.OnHealButtonPressed += PlayerActionUI_OnHealButtonPressed;

        print("Player Health: " + playerHealth);

    }

    private void PlayerActionUI_OnHealButtonPressed(object sender, EventArgs e)
    {
        if (IsPlayerAlive() && canPlayerTakeAction)
        {
            Heal();
        }
    }

    private void PlayerActionUI_OnAttackButtonPressed(object sender, EventArgs e)
    {
        if (IsPlayerAlive() && canPlayerTakeAction)
        {
            Attack(); 
        }
    }

    private void GameManager_OnTurnChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        if(e.state == GameManager.State.PlayersTurn)
        {
            canPlayerTakeAction = true;
        }
        else
        {
            canPlayerTakeAction = false;
        }
    }


    private void Enemy_OnEnemyAttack(object sender, EventArgs e)
    {
        //Random hem UnityEngine hem de System'da var o yüzden referans belirt.
        //Attack rating olmadýðý için þimdilik magic numbers
        playerHealth -= UnityEngine.Random.Range(0f, 25f);

        Instantiate(hitTakenParticle, transform.position, Quaternion.identity);
        //CM transform ile daha temiz Instantiate yapýyordu ben kýsaca böyle yapýcam þimdilik.

        OnHealthChanged?.Invoke(this, new IHasHealthBar.OnHealthChangedEventArgs{

            healthNormalized = playerHealth / playerHealthMax
        }); 

        print("Player Health: " + playerHealth); 
    
    
    }


    private void Attack()
    {
        OnAttack?.Invoke(this, EventArgs.Empty);
        //Verilecek hasarýn büyüklüðü, playerýn baþarýsýna göre ölçülüp event args ile taþýnacak enemy'ye.

        print("Player Attack!");
    }


    private void Heal()
    {
        // Health posion sistemi ile detaylandýrýlýr burasý, þimdilik stabil iyileþtirme
        OnHeal?.Invoke(this, EventArgs.Empty);

        Instantiate(healingParticle, transform.position, Quaternion.identity);
        //CM transform ile daha temiz Instantiate yapýyordu ben kýsaca böyle yapýcam þimdilik.

        if (playerHealth >= playerHealthMax * 4/5)
        {
            playerHealth = playerHealthMax;

            OnHealthChanged?.Invoke(this, new IHasHealthBar.OnHealthChangedEventArgs
            {
                healthNormalized = playerHealth / playerHealthMax
            });
        }
        else
        {
            playerHealth += playerHealthMax / 5;
            
            OnHealthChanged?.Invoke(this, new IHasHealthBar.OnHealthChangedEventArgs
            {
                healthNormalized = playerHealth / playerHealthMax
            });
        }


    }

    public bool IsPlayerAlive()
    {
        if (playerHealth <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    //FastKill için konuldu
    public void KillPlayer()
    {
        playerHealth = 0f;
        Instantiate(hitTakenParticle, transform.position, Quaternion.identity);

        OnHealthChanged?.Invoke(this, new IHasHealthBar.OnHealthChangedEventArgs
        {
            healthNormalized = playerHealth / playerHealthMax
        });


    }




}
