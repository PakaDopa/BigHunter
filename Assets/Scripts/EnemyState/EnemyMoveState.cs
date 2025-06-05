using UnityEngine;

public class EnemyMoveState : MonoBehaviour, IState<EnemyFSM>
{
    Animator animator;
    float ratio;
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
            ratio = (sender.maxHp - sender.hp) / sender.maxHp; //받은 데미지 계산
            float chance = Mathf.Lerp(0.001f, 0.01f, ratio);
            if (Random.value < chance)
            {
                Debug.Log("방패를 들어올립니다!");
                sender.ChangeState(EnemyStateType.Attack); //방패를 들어올리는 패턴
                return;
            }

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