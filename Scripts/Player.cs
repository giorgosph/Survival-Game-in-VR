using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public RawImage bloodOverlay;
    public GameObject deathCanvas;

    private float alpha = 0f; 
    private Color currColor;

    private float currentHealth = 0f;
    private int money = 0, totalMoney = 0;

    private TextMeshProUGUI deathText;

    [SerializeField] private float health = 6f;

    void Start()
    {
        currColor = bloodOverlay.color;

        currColor.a = alpha;
        bloodOverlay.color = currColor;

        deathText = deathCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (health >= 6f || health < currentHealth)
            CancelInvoke(nameof(Healing));

        if (health <= 0f) Death();
        else if (health <= 6f) BloodOverlayMananger();
    }

    private void BloodOverlayMananger()
    {
        if (health == 6f) alpha = 0f;
        else alpha = 1 / health;

        currColor.a = alpha;
        bloodOverlay.color = currColor;
    }

    private void Death()
    {
        PauseMenu.Pause();
        deathCanvas.SetActive(true);
        deathText.text = "Your Score: " + totalMoney;
    }

    public void HealPlayer()
    {
        currentHealth = health;
        InvokeRepeating(nameof(Healing), 2f, 1.5f);
    }

    private void Healing()
    {
        health += 1f;
        currentHealth += 1f;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        // image and sound effect
    }

    public void TakeMoney(int amount)
    {
        money += amount;
        if (amount > 0)
            totalMoney += amount;
    }

    public int GetCurrentMoney()
    {
        return money;
    }
}
