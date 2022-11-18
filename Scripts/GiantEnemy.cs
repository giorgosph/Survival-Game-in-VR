using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemy : Enemy
{
    [SerializeField] private AudioSource stepSourceG;
    [SerializeField] private AudioSource punchSourceG;

    void Awake()
    {
        Initialize();

        stepSource = stepSourceG;
        punchSource = punchSourceG;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 6;
        timeBetweenAttacks = 3f;
        money = 40;
    }

    // Update is called once per frame
    void Update()
    {
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (inAttackRange)
        {
            if (!setAttackRange)
            {
                attackRange += 0.5f;
                setAttackRange = true;
            }

            // Make sure agent does not move
            agent.SetDestination(transform.position);
            transform.LookAt(playerTransform);

            enemyAnimator.SetBool("isWalking", false);
            enemyAnimator.SetBool("toAttack", true);

            if (setSounds == 0 || setSounds == 2)
            {
                stepSoundOn = true;
                punchSoundOn = false;

                HandleSound();
                setSounds = 1;
            }

            AttackPlayer(2);
        }
        else
        {
            if (setAttackRange)
            {
                attackRange -= 0.5f;
                setAttackRange = false;
            }

            enemyAnimator.SetBool("toAttack", false);
            enemyAnimator.SetBool("isWalking", true);

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
