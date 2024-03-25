using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameStateController
{

    public class GameStateLoading : GameStateBase
    {

       
        public override async void OnEnter(GameStateController owner, GameStateBase prevState)
        {
            owner._timeText.text = owner._timeLimit.ToString("0");
            ScoreDirector.Initialization();
            await owner._terrainController.endCreate();
            owner._loadTimeline.Play();
            owner._loadTimeline.gameObject.SetActive(true);
            await owner._loadTimeline.PlayAsync();
            print("フェード");
            owner._loadTimeline.gameObject.SetActive(false);
            owner._startTimeline.gameObject.SetActive(true);
            await owner._startTimeline.PlayAsync();
            print("スタート");
            owner._startTimeline.gameObject.SetActive(false);
            owner.ChangeState(owner.playState);
        }
        public override void OnUpdata(GameStateController owner)
        {
            
            if (owner._startTimeline.time >= owner._startTimeline.duration)
            {
                
            }
        }

        public async override void OnExit(GameStateController owner, GameStateBase prevState)
        {

        }
    }
}
