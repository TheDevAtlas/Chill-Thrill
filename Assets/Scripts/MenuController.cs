using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    MenuInput menuActions;
    public string sceneName;
    public Animator sceneTransition;

    private void Awake() {
        menuActions = new MenuInput();

        menuActions.Menu.StartGame.performed += ctx => SceneTransition();
    }

    void SceneTransition()
    {
        sceneTransition.Play("SceneOut");
        StartCoroutine(ChangeScene(2.1f));
    }

    IEnumerator ChangeScene(float t)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(sceneName);
    }

    void OnEnable()
    {
        menuActions.Enable();
    }

    void OnDisable()
    {
        menuActions.Disable();
    }
}
