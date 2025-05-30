public interface IState<TOwner>
{
    public void OperateEnter(TOwner sender);
    public void OperateUpdate(TOwner sender);
    public void OperateExit(TOwner sender);
}