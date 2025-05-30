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
            // player�� �ƴ� ���� Ŭ������ ��
            if (player == null)
            {
                sender.ChangeState(PlayerStateType.Move);
            }
            // Ŭ���� ���� player�� ��
            else
            {
                Debug.Log("[Log] Attack Ready Enter!");
                sender.ChangeState(PlayerStateType.Attack_Ready);
            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        
#endif
    }
}