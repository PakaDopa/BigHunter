using UnityEngine;

public class EnemyAttackFirstState : MonoBehaviour, IState<EnemyFSM>
{
    Animator animator;
    public void OperateEnter(EnemyFSM sender)
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAttack_first", true);
    }

    public void OperateExit(EnemyFSM sender)
    {
        animator.SetBool("isAttack_first", false);
        animator = null;
    }

    public void OperateUpdate(EnemyFSM sender)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 = Base Layer

        // 애니메이션 끝 -> Idle 모드로
        if ((stateInfo.normalizedTime >= 1f && stateInfo.IsName("monster_attack_first_first")) ||
            GameManager.Instance.IsInGameEnd == true)
        {
            sender.ChangeState(EnemyStateType.Idle);
        }
    }
}