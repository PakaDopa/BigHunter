using UnityEngine;

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
        else
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
}
