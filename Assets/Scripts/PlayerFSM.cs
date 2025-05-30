using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerFSM : FSM<PlayerStateType, PlayerFSM>
{
    public float moveSpeed = 0.25f;

    private void Start()
    {

        IState<PlayerFSM> idle = gameObject.AddComponent<IdleState>();
        IState<PlayerFSM> move = gameObject.AddComponent<MoveState>();
        IState<PlayerFSM> attackReady = gameObject.AddComponent<AttackReadyState>();
        IState<PlayerFSM> attack = gameObject.AddComponent<AttackState>();

        dicState.Add(PlayerStateType.Idle, idle);
        dicState.Add(PlayerStateType.Move, move);
        dicState.Add(PlayerStateType.Attack_Ready, attackReady);
        dicState.Add(PlayerStateType.Attack, attack);

        //상태 추가
        sm = new StateMachine<PlayerFSM>(this, dicState[PlayerStateType.Idle]);
    }
    private void Update()
    {
        sm?.DoOperateUpdate();
    }
    public void ChangeState(PlayerStateType type) => sm.SetState(dicState[type]);
}
