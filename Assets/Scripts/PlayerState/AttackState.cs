using UnityEngine;

public class AttackState : MonoBehaviour, IState<PlayerFSM>
{
    Animator animator;

    public void OperateEnter(PlayerFSM sender)
    {
        animator = sender.GetComponent<Animator>();

        animator.SetBool("isAttackSignal", true);

        WeaponBehavoiur weapon = sender.HandTransform.GetComponentInChildren<WeaponBehavoiur>();
        weapon.ThrowWeapon();
        weapon.isAttackReady = false;
        weapon.isAttacking = true;

        EventManager.Instance.PostNotification(MEventType.Shoot, this, null);
    }

    public void OperateExit(PlayerFSM sender)
    {
        animator.SetBool("isAttackSignal", false);
        animator = null;
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