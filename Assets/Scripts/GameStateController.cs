using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public partial class GameStateController : MonoBehaviour
{
    public  GameStateBase loadState = new GameStateLoading();
    public  GameStateBase playState = new GameStatePlaying();
    public  GameStateBase endState = new GameStateEnding();
    public  GameStateBase pauseState = new GameStatePausing();
    public GameStateBase currentState { get; private set; }



    [SerializeField] private float _timeLimit = 18f;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private TerrainController _terrainController;
    [SerializeField] private PlayableDirector startTimeline;
    [SerializeField] private PlayableDirector loadTimeline;

    void Start()
    {
        currentState = loadState;
        currentState.OnEnter(this,null);
        
    }
    void Update()
    {
        currentState.OnUpdata(this);    
    }

    public void ChangeState(GameStateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }


}
