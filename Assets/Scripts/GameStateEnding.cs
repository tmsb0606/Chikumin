using UnityEngine;

public partial class GameStateController
{

    public class GameStateEnding : GameStateBase
    {
        public override async void OnEnter(GameStateController owner, GameStateBase prevState)
        {
            print("エンドシーン");
            Cursor.visible = true;
            //owner._uiPanel.SetActive(false);
            owner._endTimeline.gameObject.SetActive(true);
            await owner._endTimeline.PlayAsync();
            print("end");
            owner._resultTimeline.transform.parent.gameObject.SetActive(true);
            await owner._resultTimeline.PlayAsync();
            await owner._resultPanelController.CreateResultPanel();

        }
        public override void OnUpdata(GameStateController owner)
        {
            print("エンドupdate");
        }

        public override void OnExit(GameStateController owner, GameStateBase prevState)
        {

        }


    }
}
