using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCrate : MonoBehaviour
{
    public Animator healthBar;
    public Animator spaceTip;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerAttack>().Equip("ice");

            healthBar.SetBool("IsHealth", true);
            spaceTip.SetBool("isHealth", true);

            Destroy(gameObject);
        }
    }
}
