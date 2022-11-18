using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RandomCoordinates;

public class GameMananger : MonoBehaviour
{
    public GameObject patrolingEnemy;
    public GameObject runningEnemy;
    public GameObject giantEnemy;
    public GameObject regularEnemy;
    public GameObject pistol;
    public GameObject healingItem;

    private bool canSpawnPatrolingEnemy = false;
    private bool canSpawnRunningEnemy = false;
    private bool canSpawnGiantEnemy = false;
    private bool canSpawnRegularEnemy = false;

    private float timer;
    private int gunID;
    private int i = 0;

    //private List<GameObject> enemyList = new List<GameObject>();
    private List<GameObject> healingItems = new List<GameObject>();

    private float speedToSpawnPatrolingEnemy;
    private float speedToSpawnRunningEnemy;
    private float speedToSpawnGiantEnemy;
    private float speedToSpawnRegularEnemy;

    [SerializeField] private AudioSource levelUpSource;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime *= Time.timeScale;

        speedToSpawnPatrolingEnemy = 40f;
        speedToSpawnRunningEnemy = 55f; 
        speedToSpawnGiantEnemy = 28f;
        speedToSpawnRegularEnemy = 16f;
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        AddPatrolingEnemy();
        AddRunningEnemy();
        AddGiantEnemy();
        AddRegularEnemy();
        AddHealingItem();

        SetDifficulty();
    }

    private void SetDifficulty()
    {
        if (timer >= 150f)
        {
            timer = 0f;
            levelUpSource.Play();
            if (speedToSpawnPatrolingEnemy > 30f) speedToSpawnPatrolingEnemy -= 2f;
            if (speedToSpawnGiantEnemy > 16f) speedToSpawnGiantEnemy -= 2f;
            if (speedToSpawnRegularEnemy > 6f) speedToSpawnRegularEnemy -= 2f;
            if (speedToSpawnRunningEnemy > 20f) speedToSpawnRunningEnemy -= 2f;
        }
    }

    public void AddPistol(Vector3 position)
    {
        GameObject.Instantiate(pistol, position, Quaternion.identity);
    }

    private void AddHealingItem()
    {
        if (healingItems.Count < 8)
        {
            GameObject item;     

            item = Instantiate(healingItem, RandomCoords.RandomCoordinates(95f, 280f, 180f, 345f), Quaternion.identity);
            healingItems.Add(item);
        }
    }

    private void AddRegularEnemy()
    {
        if (!canSpawnRegularEnemy)
        {
            if(i > 0)
            {
                // Create new enemy and add it in the List
                Instantiate(regularEnemy, RandomCoords.RandomCoordinates(150f, 350f, 150f, 350f), Quaternion.identity);
                //enemyList.Add(enemy);
            }

            // Call reset function every 10 seconds  
            Invoke(nameof(ResetRegularEnemyAddition), speedToSpawnRegularEnemy);
            canSpawnRegularEnemy = true;
        }
    }

    private void AddPatrolingEnemy()
    {
        if (!canSpawnPatrolingEnemy)
        {
            // Create new enemy and add it in the List
            Instantiate(patrolingEnemy, RandomCoords.RandomCoordinates(150f, 350f, 150f, 350f), Quaternion.identity);
            //enemyList.Add(enemy);

            // Call reset function every 10 seconds  
            Invoke(nameof(ResetPatrolingEnemyAddition), speedToSpawnPatrolingEnemy);
            canSpawnPatrolingEnemy = true;
        }
    }

    private void AddRunningEnemy()
    {
        if (!canSpawnRunningEnemy)
        {
            if(i > 0)
            {
                // Create new enemy and add it in the List
                Instantiate(runningEnemy, RandomCoords.RandomCoordinates(150f, 350f, 150f, 350f), Quaternion.identity);
                //enemyList.Add(enemy);
            }
            // Call reset function every 10 seconds  
            Invoke(nameof(ResetRunningEnemyAddition), speedToSpawnRunningEnemy);
            canSpawnRunningEnemy = true;
        }
    }
    
    private void AddGiantEnemy()
    {
        if (!canSpawnGiantEnemy)
        {
            if(i > 0)
            {
                // Create new enemy and add it in the List
                Instantiate(giantEnemy, RandomCoords.RandomCoordinates(150f, 350f, 150f, 350f), Quaternion.identity);
                //enemyList.Add(enemy);
            }
            // Call reset function every 10 seconds  
            Invoke(nameof(ResetGiantEnemyAddition), speedToSpawnGiantEnemy);
            canSpawnGiantEnemy = true;
            i++;
        }
    }

    private void ResetRegularEnemyAddition()
    {
        canSpawnRegularEnemy = false;
    }

    private void ResetPatrolingEnemyAddition()
    {
        canSpawnPatrolingEnemy = false;
    }
    
    private void ResetRunningEnemyAddition()
    {
        canSpawnRunningEnemy = false;
    }
    
    private void ResetGiantEnemyAddition()
    {
        canSpawnGiantEnemy = false;
    }
}
