using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private SceneFadeController fadeController;

    [SerializeField]
    private List<SelectableMenuOption> menuOptions;

    private int menuIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeController.FadeIn();
        menuOptions[menuIndex].BeginHighlight();
    }

    public void ChangeSelection(Direction direction)
    {
        int offset = 0;

        if (direction == Direction.UP)
        {
            offset = -1;
        }
        else if (direction == Direction.DOWN)
        {
            offset = 1;
        }
        else
        {
            return;
        }

        menuOptions[menuIndex].StopHighlight();
        menuIndex = Math.Abs((menuIndex + offset) % menuOptions.Count);

        menuOptions[menuIndex].BeginHighlight();
    }

    public void Select()
    {
        menuOptions[menuIndex].Select();
    }
}
