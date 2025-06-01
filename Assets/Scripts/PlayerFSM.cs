using UnityEngine;

public class PlayerFSM : FSM<PlayerStateType, PlayerFSM>
{
    [Header("공격 관련 컴포넌트")]
    [SerializeField] private ObjectPooler weaponPooler;
    [SerializeField] private Transform handTransform;
    [SerializeField] private TrajectoryHehaviour trajectoryRenderer;

    public ObjectPooler WeaponPooler { get { return weaponPooler; } }
    public Transform HandTransform { get { return handTransform; } }
    public TrajectoryHehaviour TrajectoryRenderer { get { return trajectoryRenderer; } }

    public float moveSpeed = 0.25f;
    private void Start()
    {

        IState<PlayerFSM> idle = gameObject.AddComponent<IdleState>();
        IState<PlayerFSM> move = gameObject.AddComponent<MoveState>();
        IState<PlayerFSM> attackReady = gameObject.AddComponent<AttackReadyState>();
        IState<PlayerFSM> attack = gameObject.AddComponent<AttackState>();
        IState<PlayerFSM> dead = gameObject.AddComponent<DeadState>();

        dicState.Add(PlayerStateType.Idle, idle);
        dicState.Add(PlayerStateType.Move, move);
        dicState.Add(PlayerStateType.Attack_Ready, attackReady);
        dicState.Add(PlayerStateType.Attack, attack);
        dicState.Add(PlayerStateType.Dead, dead);

        // 기본 상태 셋팅
        sm = new StateMachine<PlayerFSM>(this, dicState[PlayerStateType.Idle]);
    }
    private void Update()
    {
        sm?.DoOperateUpdate();
    }
    public void ChangeState(PlayerStateType type) => sm.SetState(dicState[type]);
}
