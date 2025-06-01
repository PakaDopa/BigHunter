using UnityEngine;

public class EnemyDeadState : MonoBehaviour, IState<EnemyFSM>
{
    Animator animator;
    public void OperateEnter(EnemyFSM sender)
    {
        animator = GetComponent<Animator>();
        animator.Play("monster_dead");
    }

    public void OperateExit(EnemyFSM sender)
    {
    }

    public void OperateUpdate(EnemyFSM sender)
    {
    }
}