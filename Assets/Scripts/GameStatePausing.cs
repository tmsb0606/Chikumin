using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameStateController
{

    public class GameStatePausing : GameStateBase
    {
        private static GameStateBase _prevState = null;
        public override void OnEnter(GameStateController owner, GameStateBase prevState)
        {
            print("�v���C�V�[��");
            Time.timeScale = 0;
            _prevState = prevState;
            owner._pausePanel.SetActive(true);
        }
        public override void OnUpdata(GameStateController owner)
        {
            print("�|�[�Y");
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                owner._pausePanel.SetActive(false);
                owner.ChangeState(_prevState);
            }
        }

        public override void OnExit(GameStateController owner, GameStateBase prevState)
        {

        }
    }
}