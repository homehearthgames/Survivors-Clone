using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitleState : GameBaseState
{

    public override void EnterState(GameStateManager state)
    {
        Debug.Log("Entered the Title State.");
        Time.timeScale = 1f;
    }
    public override void UpdateState(GameStateManager state)
    {
        Cursor.visible = true;
    }
    public override void ExitState(GameStateManager state)
    {
    }
}
