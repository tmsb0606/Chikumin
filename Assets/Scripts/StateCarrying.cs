using UnityEngine;
public partial class ChikuminBase
{
    public class StateCarrying : ChikuminStateBase
    {
        public override void OnEnter(ChikuminBase owner, ChikuminStateBase prevState)
        {
            print("carrystart");
            owner.agent.speed = 5;
            //owner.agent.SetDestination(owner.goalObject.transform.position);
        }

        public override void OnUpdata(ChikuminBase owner)
        {
            print("carry");

            //owner.carryObjectList.Add(owner.targetObject);
            if (owner.carryObjectList[0].GetComponent<Item>().maxCarryNum > owner.carryObjectList[0].GetComponent<Item>().carryObjects.Count)
            {
                owner.carryObjectList[0].GetComponent<ICarriable>().Carry(owner.gameObject);

                owner.carryObjectList[0].GetComponent<Rigidbody>().isKinematic = true;
                owner.carryObjectList[0].GetComponent<Rigidbody>().useGravity = false;
                owner.carryObjectList[0].transform.parent = owner.transform;
                owner.carryObjectList[0].transform.localPosition = new Vector3(0, 1, 1);
                owner.carryObjectList[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
                owner.agent.SetDestination(owner.goalObject.transform.position);
            }
                print(owner.carryObjectList[0].tag);

        }


    }
}

