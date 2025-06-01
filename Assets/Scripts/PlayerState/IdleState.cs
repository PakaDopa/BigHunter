using UnityEngine;

public class IdleState : MonoBehaviour, IState<PlayerFSM>
{
    Animator animator;

    public void OperateEnter(PlayerFSM sender)
    {
        animator = sender.GetComponent<Animator>();

        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", false);
        animator.SetBool("isAttackSignal", false);

        if(sender.HandTransform.GetComponentInChildren<WeaponBehavoiur>() == null)
        {
            var weapon = sender.WeaponPooler.Rent();
            weapon.GetComponent<WeaponBehavoiur>().Setup(sender, Input.mousePosition);
        }
    }
    public void OperateExit(PlayerFSM sender)
    {
        animator = null;
    }

    public void OperateUpdate(PlayerFSM sender)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0))
        {
            var player = InputManager.Instance.GetObjectMouseClick(LayerMask.GetMask("Player"));
            // 플레이어 이외를 클릭했을 때 => 움직임
            if (player == null)
            {
                sender.ChangeState(PlayerStateType.Move);
            }
            // 플레이어를 클릭했을 때 => 조준 모션
            else
            {
                sender.ChangeState(PlayerStateType.Attack_Ready);
            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        
#endif
    }
}