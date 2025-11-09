using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour
{
    [SerializeField]
    private SceneFadeController sceneFadeController;

    private bool sceneTransitionBegun = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneFadeController.OnFadeOut += TransitionScene;
    }

    private void TransitionScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BeginGame()
    {
        if (sceneTransitionBegun)
        {
            return;
        }

        sceneTransitionBegun = true;
        sceneFadeController.FadeOut();
    }
}
