using UnityEngine;
public partial class ChikuminBase
{
    public class StateAcachikuminCarrying : StateCarrying
    {
        public override void OnEnter(ChikuminBase owner, ChikuminStateBase prevState)
        {
            print("adachicarrystart");
            owner.agent.speed = 5;
            //owner.agent.SetDestination(owner.goalObject.transform.position);
        }

        public override void OnUpdata(ChikuminBase owner)
        {
            print("acachicarry");


        }


    }
}

