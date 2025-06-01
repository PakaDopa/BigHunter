using NUnit.Framework.Constraints;
using System;
using UnityEngine;

public class EnemyFSM : FSM<EnemyStateType, EnemyFSM>
{
    [SerializeField] Transform playerTransform;
    public Transform PlayerTransform { get { return playerTransform; } }

    [Header("Status")]
    public float hp = 100;
    public float maxHp = 100;
    public float moveSpeed = 2;
    public float minMoveSpeed = 0.75f;
    public float maxMoveSpeed = 3.5f;
    public float attackRange = 2;

    [Header("Attack Setting")]
    [SerializeField] Collider2D shieldCollider;
    [SerializeField] ContactFilter2D contactFilter;
    public Collider2D ShieldCollider {  get { return shieldCollider; } }
    public ContactFilter2D ContactFilter {  get { return contactFilter; } }

    private void Start()
    {
        maxHp = hp;

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

        EventManager.Instance.AddListener(MEventType.ApplyDamage, TakeDamage);
    }
    private void Update()
    {
        sm?.DoOperateUpdate();
    }
    public void ChangeState(EnemyStateType type) => sm.SetState(dicState[type]);

    private void OnDrawGizmos()
    {
        if (playerTransform == null) return;

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        Gizmos.color = distance <= attackRange ? Color.red : Color.green;
        Gizmos.DrawLine(transform.position, playerTransform.position);

        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void TakeDamage(MEventType MEventType, Component Sender, EventArgs args = null)
    {
        TransformEventArgs tArgs = args as TransformEventArgs;
        int damage = (int)tArgs.value[0];

        hp -= damage;
        if (hp < 0)
            hp = 0;
        
        Debug.Log(hp);
        if (hp <= 0)
        {
            GameManager.Instance.GameWin();
            ChangeState(EnemyStateType.Dead);
        }
    }
}
