using UnityEditor;
using UnityEngine;

public class DeadState : MonoBehaviour, IState<PlayerFSM>
{
    Animator animator;

    public void OperateEnter(PlayerFSM sender)
    {
        animator = sender.GetComponent<Animator>();
        animator.Play("player_dead", 0, 0f); // 0번 레이어, 0프레임부터 시작
    }
    public void OperateExit(PlayerFSM sender)
    {

    }
    public void OperateUpdate(PlayerFSM sender)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 = Base Layer
        if (stateInfo.normalizedTime >= 1f && stateInfo.IsName("player_dead"))
        {
            Time.timeScale = 1f;
            GameManager.Instance.GameEnd();
        }
    }
}