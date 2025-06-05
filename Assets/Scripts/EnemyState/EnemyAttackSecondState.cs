
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngineInternal;

public class EnemyAttackSecondState : MonoBehaviour, IState<EnemyFSM>
{
    Animator animator;
    bool hasAttacked = false;
    List<Collider2D> result;
    public void OperateEnter(EnemyFSM sender)
    {
        result = new List<Collider2D>();

        animator = GetComponent<Animator>();
        animator.SetBool("isAttack_second", true);
    }

    public void OperateExit(EnemyFSM sender)
    {
        animator.SetBool("isAttack_second", false);
        hasAttacked = false;
    }

    public void OperateUpdate(EnemyFSM sender)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 = Base Layer

        // 애니메이션 끝 -> Idle 모드로
        if ((stateInfo.normalizedTime >= 1f && stateInfo.IsName("monster_attack_second_first")) || 
            GameManager.Instance.IsInGameEnd == true)
        {
            sender.ChangeState(EnemyStateType.Idle);
        }

        // "Attack" 애니메이션이 재생 중인지 확인 (State 이름은 애니메이터에 설정된 이름과 일치해야 함)
        if (stateInfo.IsName("monster_attack_second_first"))
        {
            float t = stateInfo.normalizedTime % 1; // 루프 애니메이션 방지

            // 특정 프레임 구간 (예: 0.3 ~ 0.5)에서만 공격 판정 실행
            if (t > 0.8f && t < 0.95f && !hasAttacked)
            {
                Sequence seq = DOTween.Sequence();
                seq.Append(Camera.main.transform.DOShakePosition(
                    duration: 0.15f,       // 흔들릴 시간
                    strength: 0.9f,       // 흔들림 강도
                    vibrato: 20,          // 진동 횟수
                    randomness: 90f,      // 흔들리는 방향 다양성
                    snapping: false,
                    fadeOut: true         // 점점 흔들림 약해지기
                ));

                int hits = sender.ShieldCollider.Overlap(sender.ContactFilter, result);
                if(hits > 0)
                {
                    Collider2D hit = result[0];
                    if (hit.CompareTag("Player"))
                    {
                        Debug.Log("공격 성공!");
                        Time.timeScale = 0.3f;
                        hit.gameObject.GetComponent<PlayerFSM>().ChangeState(PlayerStateType.Dead);
                    }
                }
                hasAttacked = true; // 1번만 공격
            }
        }
    }
}