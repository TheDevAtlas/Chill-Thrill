using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapePad : MonoBehaviour
{
    public string sceneName;
    public Animator sceneTransition;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            sceneTransition.Play("SceneOut");
            StartCoroutine(ChangeScene(2.1f));
        }
    }

    IEnumerator ChangeScene(float t)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(sceneName);
    }
}
