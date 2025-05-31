using UnityEngine;

public class EnemyFSM : FSM<EnemyStateType, EnemyFSM>
{
    [SerializeField] private float hp = 100;

    private void Start()
    {
        IState<EnemyFSM> idle = gameObject.AddComponent<EnemyIdleState>();
        IState<EnemyFSM> move = gameObject.AddComponent<EnemyMoveState>();
        IState<EnemyFSM> attackFirst = gameObject.AddComponent<EnemyAttackFirstState>();
        IState<EnemyFSM> attackSecond = gameObject.AddComponent<EnemyAttackSecondState>();
        IState<EnemyFSM> dead = gameObject.AddComponent<EnemyDeadState>();

        dicState.Add(EnemyStateType.Idle, idle);
        dicState.Add(EnemyStateType.Move, move);
        dicState.Add(EnemyStateType.Attack, attackFirst);
        dicState.Add(EnemyStateType.Attack_Second, attackSecond);
        dicState.Add(EnemyStateType.Dead, dead);

        // 기본 상태 셋팅
        sm = new StateMachine<EnemyFSM>(this, dicState[EnemyStateType.Idle]);
    }
    private void Update()
    {
        sm?.DoOperateUpdate();
    }
    public void ChangeState(EnemyStateType type) => sm.SetState(dicState[type]);
}
