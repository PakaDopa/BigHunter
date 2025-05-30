using System.Collections.Generic;

public class StateMachine<TEnum, TOwner>
{
    private Dictionary<TEnum, IState<TEnum, TOwner>> states = new();
    private IState<TEnum, TOwner> currentState;
    private TOwner owner;

    public TEnum CurrentStateType => currentState.StateType;

    public StateMachine(TOwner owner)
    {
        this.owner = owner;
    }

    public void AddState(IState<TEnum, TOwner> state)
    {
        if (!states.ContainsKey(state.StateType))
            states.Add(state.StateType, state);
    }

    public void ChangeState(TEnum newStateType)
    {
        if(states.TryGetValue(newStateType, out var newState))
        {
            currentState?.StateExit(owner);
            currentState = newState;
            currentState.StateEnter(owner);
        }
    }

    public void Update()
    {
        currentState?.StateUpdate(owner);
    }
}