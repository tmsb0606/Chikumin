using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameStateController
{

    public class GameStatePlaying : GameStateBase
    {
        public override void OnEnter(GameStateController owner, GameStateBase prevState)
        {
            print("プレイシーン");
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        public override void OnUpdata(GameStateController owner)
        {
            print("プレイシーンupdate");
            owner.timeLimit -= Time.deltaTime;
            owner._timeText.text = owner.timeLimit.ToString("0");
            print(owner.timeLimit);
            if(owner.timeLimit <= 0)
            {
                owner.ChangeState(owner.endState);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                owner.ChangeState(owner.pauseState);
            }
            foreach(var keyValuePair in owner._keyToStates)
            {
                if (Input.GetKeyDown(keyValuePair.Key))
                {
                    owner._levelUpController.LevelUP(keyValuePair.Value);
                    
                }
            }
        }

        public override void OnExit(GameStateController owner, GameStateBase prevState)
        {

        }
    }
}
