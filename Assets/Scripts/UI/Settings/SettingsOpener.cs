using StarterAssets;
using System;
using UnityEngine;

public class SettingsOpener : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    private static bool isPaused = false;
    [SerializeField] bool needPause; //set this false during any scenes where you DONT want the player to pause the game.

    public static Action<bool> PausedGame;

    private void OnEnable()
    {
        StarterAssetsInputs.pausePressed += PausingGame;
    }
    private void OnDisable()
    {
        StarterAssetsInputs.pausePressed -= PausingGame;
    }
    private void PausingGame()
    {
        if (needPause)
        {
            if (isPaused)
            {
                OnResume();
            }
            else
            {
                OnPause();
            }
        }
    }
    public void OnPause()
    {
        PausedGame?.Invoke(true);
        Time.timeScale = 0f;
        isPaused = true;
        settingsMenu.SetActive(true);
        Debug.Log("Game Paused");
    }
    public void OnResume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        PausedGame?.Invoke(false);
        settingsMenu.SetActive(false);
        Debug.Log("Game Unpaused");
    }
}