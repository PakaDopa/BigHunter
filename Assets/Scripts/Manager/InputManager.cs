using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public GameObject GetObjectTouch(LayerMask layerMask)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            //���� ���·� ���� �ְų�, �����̰ų� (��¶�� ������ �ִ� ������ ��)
            if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero, layerMask);
                return hit.collider.gameObject;
            }
        }
        return default;
    }
    public GameObject GetObjectMouseClick(LayerMask layerMask)
    {
        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 2D Raycast ���� (Vector2.zero ������ ����Ʈ Ŭ���� ����)
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null)
            return hit.collider.gameObject;
        else
            return default;
    }
    public override void Init()
    {
    }
}