using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    float lerpTimer;

    public float maxHealth;
    public float chipSpeed;
    public Image frontHealthBar;
    public Image backHealthBar;

    public Sprite damageSprite;
    public Sprite healSprite;

    void Update()
    {
        UpdateHealth();
    }

    void UpdateHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;

        float hFraction = health / maxHealth;

        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;

            backHealthBar.sprite = damageSprite;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if(fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;

            backHealthBar.sprite = healSprite;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0;
    }

    public void RestoreHealth(float heal)
    {
        health += heal;
        lerpTimer = 0;
    }
}
