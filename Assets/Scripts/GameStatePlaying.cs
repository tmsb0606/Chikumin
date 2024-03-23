using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameStateController
{

    public class GameStatePlaying : GameStateBase
    {
        public override void OnEnter(GameStateController owner, GameStateBase prevState)
        {
            print("�v���C�V�[��");
            Time.timeScale = 1;
        }
        public override void OnUpdata(GameStateController owner)
        {
            print("�v���C�V�[��update");
            owner._timeLimit -= Time.deltaTime;
            owner._timeText.text = owner._timeLimit.ToString("0");
            print(owner._timeLimit);
            if(owner._timeLimit <= 0)
            {
                owner.ChangeState(owner.endState);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                owner.ChangeState(owner.pauseState);
            }
        }

        public override void OnExit(GameStateController owner, GameStateBase prevState)
        {

        }
    }
}