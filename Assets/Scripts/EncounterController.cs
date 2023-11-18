using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject goal;
    public bool setTrueOrFalse;

    void Update()
    {
        if(enemies.Count <= 0)
        {
            goal.SetActive(setTrueOrFalse);

            Destroy(gameObject);
            return;
        }
        foreach(GameObject g in enemies)
        {
            if(g == null)
            {
                enemies.Remove(g);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            if(enemies.Count <= 0)
            {
                return;
            }
            foreach(GameObject g in enemies)
            {
                g.GetComponent<IceEnemyController>().isActive = true;
            }
        }
    }
}
