using UnityEngine;

public class EnemyIdleState : MonoBehaviour, IState<EnemyFSM>
{
    public void OperateEnter(EnemyFSM sender)
    {
        Animator animator = sender.GetComponent<Animator>();
    }

    public void OperateExit(EnemyFSM sender)
    {
    }

    public void OperateUpdate(EnemyFSM sender)
    {
        if(sender.PlayerTransform != null && GameManager.Instance.IsInGameEnd == false)
        {
            sender.ChangeState(EnemyStateType.Move);
        }
    }
}