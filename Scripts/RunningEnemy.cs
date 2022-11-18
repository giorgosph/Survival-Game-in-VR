using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningEnemy : Enemy
{
    [SerializeField] private AudioSource stepSourceRun;
    [SerializeField] private AudioSource punchSourceRun;

    void Awake()
    {
        Initialize();

        stepSource = stepSourceRun;
        punchSource = punchSourceRun;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 1;
        timeBetweenAttacks = 0.5f;
        money = 10;
    }

    // Update is called once per frame
    void Update()
    {
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (inAttackRange)
        {
            enemyAnimator.SetBool("toJump", true);

            if (setSounds == 0 || setSounds == 1)
            {
                stepSoundOn = true;
                punchSoundOn = false;

                HandleSound();
                setSounds = 2;

                AttackPlayer(3);
                Destroy(gameObject, 1f);
            }
        }
        else
        {
            enemyAnimator.SetBool("isRunning", true);

            if (setSounds == 0)
            {
                stepSoundOn = false;
                punchSoundOn = true;

                HandleSound();
                setSounds = 1;
            }

            agent.SetDestination(playerTransform.position);
            transform.LookAt(playerTransform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, attackRange);
    }
}
