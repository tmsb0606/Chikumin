public abstract class ChikuminStateBase
{
    public virtual void OnEnter(ChikuminBase owner, ChikuminStateBase prevState) {}
    public virtual void OnUpdata(ChikuminBase owner){  }
    public virtual void OnExit(ChikuminBase owner,ChikuminStateBase prevState) {    }



}
