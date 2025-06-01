using UnityEngine;

public class MoveState : MonoBehaviour, IState<PlayerFSM>
{
    Animator animator;
    public PlayerStateType StateType { get; }

    public void OperateEnter(PlayerFSM sender)
    {
        animator = sender.GetComponent<Animator>();

        animator.SetBool("isWalk", true);
    }

    public void OperateExit(PlayerFSM sender)
    {
        animator.SetBool("isWalk", false);
        animator = null;
    }

    public void OperateUpdate(PlayerFSM sender)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0) == false)
        {
            sender.ChangeState(PlayerStateType.Idle);
        }
        transform.Translate(sender.moveSpeed * Time.deltaTime * Vector3.left);
#elif UNITY_ANDROID || UNITY_IOS
#endif
    }
}