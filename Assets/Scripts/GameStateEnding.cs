using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameStateController
{

    public class GameStateEnding : GameStateBase
    {
        public override void OnEnter(GameStateController owner, GameStateBase prevState)
        {
            print("�G���h�V�[��");
        }
        public override void OnUpdata(GameStateController owner)
        {
            print("�G���hupdate");
        }

        public override void OnExit(GameStateController owner, GameStateBase prevState)
        {

        }
    }
}
