using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IHasHealthBar
{
    public event EventHandler<IHasHealthBar.OnHealthChangedEventArgs> OnHealthChanged;
    public event EventHandler OnEnemyAttack;

    private float enemyHealthMax = 100f;
    private float enemyHealth; 
    private bool canEnemyAttack = false;
    private float enemyWaits = 2f;

    [SerializeField] private ParticleSystem hitTakenParticle;
    [SerializeField] private ParticleSystem enemyDeadParticle;
    [SerializeField] private GameObject enemyVisual; 


    private void Awake()
    {

        enemyHealth = enemyHealthMax; 
    }

    private void Start()
    {
        Player.Instance.OnAttack += Player_OnAttack;
        GameManager.Instance.OnTurnChanged += GameManager_OnTurnChanged;

        print("Enemy Health: " + enemyHealth);

    }

    private void GameManager_OnTurnChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        if(e.state == GameManager.State.EnemysTurn)
        {
            canEnemyAttack = true;
        }
        else
        {
            canEnemyAttack = false;
        }
    }

    public void Update()
    {
        if (IsEnemyAlive() && canEnemyAttack)
        {

            enemyWaits -= Time.deltaTime;

            if(enemyWaits <= 0)
            {
                EnemyAttack();
                enemyWaits = 2f; 
            }

        }
    }

    private void Player_OnAttack(object sender, EventArgs e)
    {
        //Playerýn baþarýlý vuruþ ratingine göre enemy canýný güncelleyecek. 
        //Random hem UnityEngine hem de System'da var o yüzden referans belirt.
        //Attack rating olmadýðý için þimdilik magic numbers

        enemyHealth -= UnityEngine.Random.Range(0f, 25f);

        OnHealthChanged?.Invoke(this, new IHasHealthBar.OnHealthChangedEventArgs
        {
            healthNormalized = enemyHealth / enemyHealthMax
        }); 

        if(IsEnemyAlive() == true)
        {
            Instantiate(hitTakenParticle, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(enemyDeadParticle, transform.position, Quaternion.identity); 
        }

        print("Enemy Health: " + enemyHealth);
    }

    private void EnemyAttack()
    {

        OnEnemyAttack?.Invoke(this, EventArgs.Empty);
        print("Enemy Attack!");
    }

    public bool IsEnemyAlive()
    {
        if (enemyHealth <= 0)
        {
            enemyVisual.SetActive(false);
            return false;
        }
        else
        {
            enemyVisual.SetActive(true); 
            return true;
        }
    }

    //Fast kill için konuldu
    public void KillEnemy()
    {
        enemyHealth = 0f;
        Instantiate(enemyDeadParticle, transform.position, Quaternion.identity);

        OnHealthChanged?.Invoke(this, new IHasHealthBar.OnHealthChangedEventArgs
        {
            healthNormalized = enemyHealth / enemyHealthMax
        });
    }


}
