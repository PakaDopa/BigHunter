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

        WeaponBehavoiur weapon = sender.HandTransform.GetComponentInChildren<WeaponBehavoiur>();
        weapon.ThrowWeapon();
        weapon.isAttackReady = false;
        weapon.isAttacking = true;


        Debug.Log("PlayerFSM, Attack OpeatorEnter");
    }

    public void OperateExit(PlayerFSM sender)
    {
        animator = null;
        Debug.Log("PlayerFSM, Attack OpeatorExit");
    }

    public void OperateUpdate(PlayerFSM sender)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 = Base Layer

        // 애니메이션 끝 -> Idle 모드로
        if (stateInfo.normalizedTime >= 1f && stateInfo.IsName("player_attack"))
        {
            sender.ChangeState(PlayerStateType.Idle);
        }
    }
}