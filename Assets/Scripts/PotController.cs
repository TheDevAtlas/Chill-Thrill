using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    public GameObject vfx;
    SpriteRenderer sr;
    PolygonCollider2D pg;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        pg = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet")
        {
            GameObject v = Instantiate(vfx, transform.position, Quaternion.identity);
            StartCoroutine(DestroyVFX(2f, v));
            Destroy(other.gameObject);
            sr.color = Color.clear;
            pg.enabled = false;
        }
    }

    IEnumerator DestroyVFX(float t, GameObject v)
    {
        yield return new WaitForSeconds(t);
        Destroy(v);
        Destroy(gameObject);
    }
}
