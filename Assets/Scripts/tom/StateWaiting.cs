public partial class ChikuminBase
{
    public class StateWaiting : ChikuminStateBase
    {
        public override void OnEnter(ChikuminBase owner, ChikuminStateBase prevState)
        {
            print("waitstart");
            owner.agent.speed = 0;
            if(prevState == stateMoving)
            {

            }
        }

        public override void OnUpdata(ChikuminBase owner)
        {
            print("wait");
            //owner.agent.speed = 1;
           
            if(owner.carryObjectList.Count != 0)
            {
                owner.ChangeState(stateCarrying);
            }
             
            
        }
    }
}

