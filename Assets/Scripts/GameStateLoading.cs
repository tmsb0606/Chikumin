using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameStateController
{

    public class GameStateLoading : GameStateBase
    {

       
        public override async void OnEnter(GameStateController owner, GameStateBase prevState)
        {
            await owner._terrainController.endCreate();
            //owner.loadTimeline.gameObject.SetActive(true);
            await owner.loadTimeline.PlayAsync();
            print("フェード");
            owner.loadTimeline.gameObject.SetActive(false);
            owner.startTimeline.gameObject.SetActive(true);
            await owner.startTimeline.PlayAsync();
            print("スタート");
            owner.startTimeline.gameObject.SetActive(false);
            owner.ChangeState(owner.playState);
        }
        public override void OnUpdata(GameStateController owner)
        {
            
            if (owner.startTimeline.time >= owner.startTimeline.duration)
            {
                
            }
        }

        public async override void OnExit(GameStateController owner, GameStateBase prevState)
        {

        }
    }
}
