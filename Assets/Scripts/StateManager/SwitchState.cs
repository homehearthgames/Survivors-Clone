using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchState : MonoBehaviour
{
    public void SwitchToPlayScene()
    {
        GameStateManager.Instance.LoadSceneAndSwitchState("PlayScene", GameStateManager.Instance.PlayState);
        Unpause();
    }

    
    public void SwitchToTitleScene()
    {
        GameStateManager.Instance.LoadSceneAndSwitchState("TitleScene", GameStateManager.Instance.TitleState);
        Unpause();
    }

    private void Unpause()
    {
        Time.timeScale = 1f; // Unpause the game
    }
}
