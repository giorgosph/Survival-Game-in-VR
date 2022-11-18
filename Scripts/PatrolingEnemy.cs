using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RandomCoordinates;

public class PatrolingEnemy : Enemy
{
    public Vector3 walkPoint;
    public float walkPointRange;
    public float sightRange;

    private bool walkPointSet;
    private bool inSightRange;

    [SerializeField] private AudioSource stepSourceP;
    [SerializeField] private AudioSource punchSourceP;

    void Awake()
    {
        Initialize();

        stepSource = stepSourceP;
        punchSource = punchSourceP;
    }

    void Start()
    {
        health = 3;
        timeBetweenAttacks = 1.4f;
        money = 20;
    }

    void Update()
    {
        // Check for sight and attack range
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!inSightRange && !inAttackRange) Patroling();
        if (inSightRange && !inAttackRange) ChasePlayer();
        if (inSightRange && inAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) 
        {
            walkPoint = RandomCoords.RandomCoordinates(transform.position.x - walkPointRange, transform.position.x + walkPointRange,
                transform.position.z - walkPointRange, transform.position.z + walkPointRange);
            walkPointSet = true;
        }
        else agent.SetDestination(walkPoint);

        enemyAnimator.SetBool("isRunning", false);
        enemyAnimator.SetBool("toAttack", false);
        enemyAnimator.SetBool("isWalking", true);

        if (setSounds == 0)
        {
            stepSoundOn = false;
            punchSoundOn = true;

            HandleSound();
            setSounds = 1;
        }
        else if (setSounds == 2)
        {
            stepSource.pitch -= 0.25f;
            setSounds = 1;
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private void ChasePlayer()
    {
        if (setAttackRange)
        {
            attackRange -= 0.5f;
            setAttackRange = false;
        }

        enemyAnimator.SetBool("isWalking", false);
        enemyAnimator.SetBool("toAttack", false);
        enemyAnimator.SetBool("isRunning", true);

        if (setSounds == 0 || setSounds == 3)
        {
            stepSource.pitch += 0.2f;

            stepSoundOn = false;
            punchSoundOn = true;

            HandleSound();
            setSounds = 2;
        }
        else if (setSounds == 1)
        {
            stepSource.pitch += 0.25f;
            setSounds = 2;
        }

        agent.SetDestination(playerTransform.position);
        transform.LookAt(playerTransform);
    }

    private void AttackPlayer()
    {
        if (!setAttackRange)
        {
            attackRange += 0.5f;
            setAttackRange = true;
        }

        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);

        enemyAnimator.SetBool("isRunning", false);
        enemyAnimator.SetBool("isWalking", false);
        enemyAnimator.SetBool("toAttack", true);

        if (setSounds == 0 || setSounds == 2)
        {
            stepSoundOn = true;
            punchSoundOn = false;

            HandleSound();
            setSounds = 3;
        }

        AttackPlayer(1);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, sightRange);
    }
}
