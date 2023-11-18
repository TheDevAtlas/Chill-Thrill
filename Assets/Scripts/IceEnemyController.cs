using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceEnemyController : MonoBehaviour
{
    public bool isActive;
    GameObject player;
    public float speed;
    public float explodeDistance;
    public float health;
    public float timeToExplode = 2f;
    public float timer;
    public float timer2;

    public Animator iceAnim;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        iceAnim = GetComponent<Animator>();

        health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isActive)
        {
            return;
        }

        if(health <= 0)
        {
            iceAnim.SetBool("Explode", true);
            iceAnim.SetBool("Hurt", false);
            isActive = false;
            StartCoroutine(DestroyVFX(1.2f));
        }

        Vector3 moveDirection = (player.transform.position - transform.position);

        // Check If In Range / Time If Explode //
        if(moveDirection.magnitude <= explodeDistance)
        {
            // Explode w/ Destroy Object w/ Player Damage //
            iceAnim.SetBool("Hurt", true);
            timer += Time.fixedDeltaTime;

            if(timer >= timeToExplode)
            {
                iceAnim.SetBool("Explode", true);
                iceAnim.SetBool("Hurt", false);
                isActive = false;
                timer = 0;
                StartCoroutine(DestroyVFXWithHurt(1.2f));
            }
        }
        else
        {
            // Move towards player //
            moveDirection = moveDirection.normalized * speed;
            rb.MovePosition(rb.position + new Vector2(moveDirection.x, moveDirection.y / 2f) * Time.fixedDeltaTime);
            timer = 0f;
            timer2 += Time.fixedDeltaTime;
            if(timer2 >= timeToExplode)
            {
                iceAnim.SetBool("Hurt", false);
                timer2 = 0f;
            }
        }
    }

    IEnumerator DestroyVFX(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }

    IEnumerator DestroyVFXWithHurt(float t)
    {
        yield return new WaitForSeconds(t);
        // Hurt Player
        player.GetComponent<PlayerHealth>().health -= 10f;
        Destroy(gameObject);
    }

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
