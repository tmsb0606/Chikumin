using UnityEngine;
public partial class ChikuminBase
{
    public class StateMoving : ChikuminStateBase
    {
        public override void OnEnter(ChikuminBase owner, ChikuminStateBase prevState)
        {
            print("movestart");
            owner.agent.speed = 5f;
        }

        public override void OnUpdata(ChikuminBase owner)
        {
            print("move");
            if (owner.agent != null)
            {
                owner.agent.SetDestination(owner.targetPlayer.transform.position);
                if (Vector3.Distance(owner.targetPlayer.transform.position, owner.transform.position) > 2)
                {

                }
                else
                {
                    owner.agent.velocity = Vector3.zero;
                }
            }

        }

/*        public override void OnUpdata(ChikuminBase owner)
        {
*//*            print("update");
            if (owner.agent != null)
            {
                owner.agent.SetDestination(owner.targetPlayer.transform.position);
                if (Vector3.Distance(owner.targetPlayer.transform.position, owner.transform.position) > 2)
                {
                    
                }
                else
                {
                    owner.agent.velocity = Vector3.zero;
                }
            }*//*

        }*/


    }
}

