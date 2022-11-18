using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularEnemy : Enemy
{
    [SerializeField] private AudioSource stepSourceR;
    [SerializeField] private AudioSource punchSourceR;

    void Awake()
    {
        Initialize();

        stepSource = stepSourceR;
        punchSource = punchSourceR;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 2;
        timeBetweenAttacks = 1.8f;
        money = 15;
    }

    // Update is called once per frame
    void Update()
    {
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (inAttackRange)
        {
            if(!setAttackRange)
            {
                attackRange += 0.5f;
                setAttackRange = true;
            }

            // Make sure agent does not move
            agent.SetDestination(transform.position);
            transform.LookAt(playerTransform);

            enemyAnimator.SetBool("isRunning", false);
            enemyAnimator.SetBool("toAttack", true);


            if (setSounds == 0 || setSounds == 2)
            {
                stepSoundOn = true;
                punchSoundOn = false;

                HandleSound();
                setSounds = 1;
            }


            AttackPlayer(1);
        }
        else
        {
            if (setAttackRange)
            {
                attackRange -= 0.5f;
                setAttackRange = false;
            }

            enemyAnimator.SetBool("toAttack", false);
            enemyAnimator.SetBool("isRunning", true);

            if (setSounds == 0 || setSounds == 1)
            {
                stepSoundOn = false;
                punchSoundOn = true;

                HandleSound();
                setSounds = 2;
            }

            agent.SetDestination(playerTransform.position);
            transform.LookAt(playerTransform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, attackRange);
    }
}
