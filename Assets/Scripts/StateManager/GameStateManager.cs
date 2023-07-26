using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public GameBaseState currentState;
    public GamePlayState PlayState = new GamePlayState();
    public GamePauseState PauseState = new GamePauseState();
    public GameTitleState TitleState = new GameTitleState();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        Debug.Log("GameStateManager Started");
        currentState = TitleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void LoadSceneAndSwitchState(string sceneName, GameBaseState nextState)
    {
        StartCoroutine(LoadSceneAsync(sceneName, nextState));
    }

    private IEnumerator LoadSceneAsync(string sceneName, GameBaseState nextState)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        SwitchState(nextState);
    }
}
