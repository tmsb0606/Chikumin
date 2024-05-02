public class GameStateBase 
{
    public virtual async void OnEnter(GameStateController owner, GameStateBase prevState) { }
    public virtual async void OnUpdata(GameStateController owner) { }
    public virtual async void OnExit(GameStateController owner, GameStateBase prevState) { }
}
