public interface IState<TEnum, TOwner>
{
    public TEnum StateType { get; }
    public void StateEnter(TOwner sender);
    public void StateUpdate(TOwner sender);
    public void StateExit(TOwner sender);
}