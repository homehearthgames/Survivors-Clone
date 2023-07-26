using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseState : GameBaseState
{
    public bool pauseMode;

    public override void EnterState(GameStateManager state)
    {
        Debug.Log("Entered the Pause State.");
        pauseMode = true;
        Time.timeScale = 0;
    }

    public override void UpdateState(GameStateManager state)
    {
        // nothing to update
    }

    public override void ExitState(GameStateManager state)
    {
        pauseMode = false;
    }
}
