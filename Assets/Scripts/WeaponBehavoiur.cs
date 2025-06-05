using System;
using DG.Tweening;
using UnityEngine;
using VInspector.Libs;

public class WeaponBehavoiur : MonoBehaviour
{
    private Vector3 originPosition;
    
    [SerializeField] private Vector3 offset = new(0.22f, -0.22f, -0.7f);
    [SerializeField] private float ratationOffset = 30f;
    private Rigidbody2D rb;

    private PlayerFSM parent;
    private Transform handTransform;

    public bool isAttackReady = false;
    public bool isAttacking = false;

    [SerializeField] private float minAngle = -80f;
    [SerializeField] private float maxAngle = 30f;
    [SerializeField] private float minForce = 5f;
    [SerializeField] private float maxForce = 20f;
    [SerializeField] private float angleSensitivity = 0.2f;
    [SerializeField] private float forceSensitivity = 0.1f;

    [SerializeField] private float bounceForce = 1f;
    [SerializeField] private int criticalDamage = 45;
    [SerializeField] private int normalDamage = 30;

    Vector2 dragStartPos;
    public float currentAngle, currentForce;

    public void Setup(PlayerFSM parent, Vector2 dragStartPos)
    {
        rb = GetComponent<Rigidbody2D>();
        this.dragStartPos = dragStartPos;
        //default setting
        this.parent = parent;
        handTransform = transform.parent;
        transform.position = new(transform.position.x, transform.position.y, -1);
        originPosition = transform.position;

        //setting angle & position
        SettingOffsetRotationPosition();
    }
    private void Update()
    {
        if(!isAttacking)
        {
            if (isAttackReady == false)
                SettingOffsetRotationPosition();
            else
                UpdateRotationMouseAngle();
        }
        else //창이 날라가고 있는 중
        {
            Vector2 velocity = rb.linearVelocity;

            if (velocity.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
    public void ThrowWeapon()
    {
        //궤적 보기 끄기
        parent.TrajectoryRenderer.HideTrajectory();

        //던져진 순간 오브젝트 풀러 parent로 이동!
        Transform pollStorage = parent.WeaponPooler.ParentTransform;
        transform.parent = pollStorage;
        rb.simulated = true;

        if (rb != null)
        {
            // 팔이 바라보는 방향으로 힘 주기
            Vector2 throwDirection = transform.right.normalized; // local X+ 방향
            rb.AddForce(throwDirection * currentForce, ForceMode2D.Impulse);
        }
    }
    private void SettingOffsetRotationPosition()
    {
        transform.rotation = handTransform.rotation;
        transform.rotation = Quaternion.Euler(0f, 0f, ratationOffset);
    }
    private void UpdateRotationMouseAngle()
    {
        Vector2 currentPos = Input.mousePosition;
        Vector2 delta = currentPos - dragStartPos;

        // 좌/우 드래그 → 각도 조정
        float deltaX = delta.x * angleSensitivity;
        currentAngle = Mathf.Clamp(currentAngle + deltaX, minAngle, maxAngle);

        // 상/하 드래그 → 힘 조정
        float deltaY = -1 * (delta.y * forceSensitivity);
        currentForce = Mathf.Clamp(currentForce + deltaY, minForce, maxForce);

        transform.rotation = Quaternion.Euler(0f, 0f, -currentAngle);
        
        dragStartPos = currentPos; // 다음 프레임 대비 갱신

        //투사체 궤적 표시
        Vector2 throwDirection = transform.right.normalized; // local X+ 방향
        parent.TrajectoryRenderer.ShowTrajectory(throwDirection * currentForce, rb.gravityScale);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string hitLayer = LayerMask.LayerToName(collision.gameObject.layer);
        Vector2 hitDirection = rb.linearVelocity.normalized;

        switch (hitLayer)
        {
            case "Shield":
                // 튕겨나가기 (반사 방향 = 반사 벡터)
                Vector2 reflectDir = Vector2.Reflect(hitDirection, collision.contacts[0].normal);
                rb.linearVelocity = reflectDir * bounceForce; // bounceForce는 적당한 값 설정
                parent.combo = 0;
                break;

            case "Head":
                StickToTarget(collision); // 박히는 처리
                ApplyDamage(critical: true);
                break;

            case "Body":
                StickToTarget(collision); // 박히는 처리
                ApplyDamage(critical: false);
                break;

            default:
                parent.combo = 0;
                // 무효한 충돌
                break;
        }
    }
    //데미지를 입힘
    private void ApplyDamage(bool critical)
    {
        ApplayEffect();
        parent.combo += 1;
        int damage = critical ? criticalDamage : normalDamage;
        damage += GetCombo();
        EventManager.Instance.PostNotification(MEventType.ApplyDamage, this, new TransformEventArgs(transform, damage, critical, parent.combo, transform.position));
        // 이펙트/사운드 등도 여기에
    }
    //창이 박히게
    void StickToTarget(Collision2D collision)
    {
        var obj = Instantiate(parent.EffectEffectPrefab);

        Vector2 direction = rb.linearVelocity.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 기본 Z회전이 90도이므로 +90도 보정
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle + 90f);

        obj.SetPositionAndRotation(rb.transform.position, rotation);
        Destroy(obj.gameObject, 1.5f);

        // 투사체를 충돌 지점에 고정
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false; // 시뮬레이션 멈춤
        transform.SetParent(collision.transform);
    }
    private void ApplayEffect()
    {
        //카메라 무빙
        Sequence seq = DOTween.Sequence();
        seq.Append(Camera.main.transform.DOShakePosition(
            duration: 0.15f,       // 흔들릴 시간
            strength: 0.75f,       // 흔들림 강도
            vibrato: 15,          // 진동 횟수
            randomness: 90f,      // 흔들리는 방향 다양성
            snapping: false,
            fadeOut: true         // 점점 흔들림 약해지기
        ));
    }
    private int GetCombo()
    {
        int combo = parent.combo;
        if (combo <= 1)
            return 0;
        if (combo <= 20)
        {
            // 점점 증가, 최대 20콤보까지 증가
            return Mathf.FloorToInt(Mathf.Log(combo + 1) * 7.5f);
        }
        else
        {
            // 20 이상이면 증가폭을 미미하게 또는 고정
            return Mathf.FloorToInt(Mathf.Log(21) * 7.5f + (combo - 20) * 0.1f);
        }
    }
}
