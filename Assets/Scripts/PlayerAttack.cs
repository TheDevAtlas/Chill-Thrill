using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    SpriteRenderer playerGFX;
    public Color iceColor;
    public bool canAttack;

    private void Start()
    {
        playerGFX = GetComponent<SpriteRenderer>();
    }

    public void Equip(string item)
    {
        if(item == "ice")
        {
            playerGFX.color = iceColor;
            canAttack = true;
        }
    }

    public void Shoot(Vector2 direction)
    {
        if(!canAttack)
        {
            return;
        }

        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

        newBullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        StartCoroutine(DestroyVFX(1f, newBullet));
    }

    IEnumerator DestroyVFX(float t, GameObject h)
    {
        yield return new WaitForSeconds(t);
        Destroy(h);
    }
}
