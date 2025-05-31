using System.Diagnostics;

public class StateMachine<TOwner>
{
    private TOwner m_sender;

    public IState<TOwner> CurState { get; set; }

    public StateMachine(TOwner sender, IState<TOwner> state)
    {
        m_sender = sender;
        SetState(state);
    }

    public void SetState(IState<TOwner> state)
    {
        if (m_sender == null)
            return;
        if (CurState == state)
            return;

        CurState?.OperateExit(m_sender);
        CurState = state;
        CurState?.OperateEnter(m_sender);
    }

    public void DoOperateUpdate()
    {
        CurState?.OperateUpdate(m_sender);
    }
}