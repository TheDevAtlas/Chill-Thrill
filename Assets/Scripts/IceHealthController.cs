using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHealthController : MonoBehaviour
{
    public IceEnemyController healthController;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            healthController.health -= 10f;
            healthController.timer = 0f;
            healthController.iceAnim.SetBool("Hurt", true);
        }
    }
}
