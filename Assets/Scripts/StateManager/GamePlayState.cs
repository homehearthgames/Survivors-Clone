using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : GameBaseState
{
    public bool playMode;

    public override void EnterState(GameStateManager state)
    {
        Debug.Log("Entered the Play State.");
        playMode = true;
        Time.timeScale = 1f;
    }
    public override void UpdateState(GameStateManager state)
    {
        
    }
    public override void ExitState(GameStateManager state)
    {
        playMode = false;
    }
}
