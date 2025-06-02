using UnityEngine;

public class AttackReadyState : MonoBehaviour, IState<PlayerFSM>
{

    Animator animator;

    
    public void OperateEnter(PlayerFSM sender)
    {
        animator = GetComponent<Animator>();

        animator.SetBool("isAttack", true);

        //weapon도 공격준비 자세
        WeaponBehavoiur weapon = sender.HandTransform.GetComponentInChildren<WeaponBehavoiur>();
        weapon.isAttackReady = true;
    }

    public void OperateExit(PlayerFSM sender)
    {
        animator.SetBool("isAttack", false);
        animator = null;
    }

    public void OperateUpdate(PlayerFSM sender)
    {
        if (Input.GetMouseButton(0) == false)
        {
            sender.ChangeState(PlayerStateType.Attack);
        }
#if UNITY_ANDROID || UNITY_IOS
#endif
    }
}