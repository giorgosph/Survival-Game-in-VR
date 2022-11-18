using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected NavMeshAgent agent;  // AI agent using NavMesh from unity
    protected LayerMask whatIsGround, whatIsPlayer; // recognise ground and player objects
    protected GameObject player;    // position of the player
    protected Transform playerTransform;    // position of the player

    protected bool inAttackRange;
    protected int money;
    protected float health = 3f;
    protected float timeBetweenAttacks { get; set; } = 4f;

    protected bool setAttackRange = false;
    protected bool stepSoundOn = false;
    protected bool punchSoundOn = false;
    protected int setSounds = 0;

    protected AudioSource stepSource = null;
    protected AudioSource punchSource = null;
    protected Animator enemyAnimator;

    private bool alreadyAttacked;
    private bool gameWasPaused = false;

    public float attackRange;

    void FixedUpdate()
    {
        PuaseAudio();
    }

    protected void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        whatIsGround = LayerMask.GetMask("Ground");
        whatIsPlayer = LayerMask.GetMask("Body");
        player = GameObject.Find("/VR Rig");
        playerTransform = player.transform;
        enemyAnimator = this.GetComponent<Animator>();
    }


    protected void HandleSound()
    {
        if (!stepSoundOn && stepSource != null)
        {
            stepSource.Play();
            stepSoundOn = true;
        }
        else if (stepSource != null)
        {
            stepSource.Stop();
            stepSoundOn = false;
        }

        if (!punchSoundOn && punchSource != null)
        {
            punchSource.Play();
            punchSoundOn = true;
        }
        else if (punchSource != null)
        {
            punchSource.Stop();
            punchSoundOn = false;
        }
    }

    private void PuaseAudio() 
    {
        if (PauseMenu.GameIsPaused && !gameWasPaused)
        {
            stepSource.Stop();
            punchSource.Stop();

            gameWasPaused = true;
        }
        else if(!PauseMenu.GameIsPaused && gameWasPaused)
        {
            stepSoundOn = false;
            punchSoundOn = false;
            setSounds = 0;

            gameWasPaused = false;
        }
    }

    protected void AttackPlayer(int amount)
    {
        if (!alreadyAttacked)
        {
            // Attack code
            player.GetComponent<Player>().TakeDamage(amount);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    protected void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float amount)
    {
        enemyAnimator.SetBool("gotHit", true);
        health -= amount;
        enemyAnimator.SetBool("gotHit", false);
        if (health <= 0)
        {
            enemyAnimator.SetBool("onDeath", true);

            Invoke(nameof(DestroyEnemy), .5f);
        }
    }

    private void DestroyEnemy()
    {
        player.GetComponent<Player>().TakeMoney(money);
        Destroy(gameObject);
    }
}
