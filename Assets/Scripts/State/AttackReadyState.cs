using UnityEngine;

public class AttackReadyState : MonoBehaviour, IState<PlayerFSM>
{

    Animator animator;

    
    public void OperateEnter(PlayerFSM sender)
    {
        animator = GetComponent<Animator>();

        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", true);
        animator.SetBool("isAttackSignal", false);

        //weapon도 공격준비 자세
        WeaponBehavoiur weapon = sender.HandTransform.GetComponentInChildren<WeaponBehavoiur>();
        weapon.isAttackReady = true;

        Debug.Log("PlayerFSM, Attack_Ready OpeatorEnter");
    }

    public void OperateExit(PlayerFSM sender)
    {
        animator = null;
        Debug.Log("PlayerFSM, Attack_Ready OpeatorExit");
    }

    public void OperateUpdate(PlayerFSM sender)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0) == false)
        {
            sender.ChangeState(PlayerStateType.Attack);
        }
#elif UNITY_ANDROID || UNITY_IOS
#endif
    }
}