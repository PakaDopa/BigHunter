using UnityEngine;

public class AttackState : MonoBehaviour, IState<PlayerFSM>
{
    Animator animator;

    public void OperateEnter(PlayerFSM sender)
    {
        animator = sender.GetComponent<Animator>();

        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", false);
        animator.SetBool("isAttackSignal", true);
    }

    public void OperateExit(PlayerFSM sender)
    {
        animator = null;
    }

    public void OperateUpdate(PlayerFSM sender)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 = Base Layer

        // 현재 재생 중인 클립이 끝났는지 확인 (1.0 이상이면 완료됨)
        if (stateInfo.normalizedTime >= 1f && stateInfo.IsName("player_attack"))
        {
            sender.ChangeState(PlayerStateType.Idle);
        }
    }
}