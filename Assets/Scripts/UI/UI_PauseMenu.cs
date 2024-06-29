using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseMenu : MonoBehaviour
{
    public void OnResumePress()
    {
        UI.Instance.TogglePauseMenu();
    }

    public void OnResetPress()
    {
        Time.timeScale = 1.0f;
        Game.ResetGame();
    }

    public void OnExitPress()
    {
        Time.timeScale = 1.0f;
        Game.ExitGame();
    }
}
