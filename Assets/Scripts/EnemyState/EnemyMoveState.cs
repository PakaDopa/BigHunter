using UnityEngine;

public class EnemyMoveState : MonoBehaviour, IState<EnemyFSM>
{
    Animator animator;
    public void OperateEnter(EnemyFSM sender)
    {
        animator = sender.GetComponent<Animator>();

        if (sender.PlayerTransform != null)
            animator.SetBool("isWalk", true);
    }

    public void OperateExit(EnemyFSM sender)
    {
        animator.SetBool("isWalk", false);
    }

    public void OperateUpdate(EnemyFSM sender)
    {
        float distance = Vector2.Distance(transform.position, sender.PlayerTransform.position);

        // 공격 범위 안이 아니면 이동
        if (distance > sender.attackRange)
        {
            // 거리를 0~1 범위로 정규화 (멀면 1, 가까우면 0)
            float t = Mathf.Clamp01(distance / 15);

            // 속도 계산 (Lerp로 minSpeed ~ maxSpeed 사이로 보간)
            sender.moveSpeed = Mathf.Lerp(sender.minMoveSpeed, sender.maxMoveSpeed, t);

            // 이동 방향
            Vector2 direction = (sender.PlayerTransform.position - transform.position).normalized;

            // 이동
            transform.position += (Vector3)(sender.moveSpeed * Time.deltaTime * direction);
        }
        else
        {
            // 공격 상태 전환 가능
            sender.ChangeState(EnemyStateType.Attack_Second);
        }
    }
}