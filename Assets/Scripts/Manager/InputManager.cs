using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public GameObject GetObjectTouch(LayerMask layerMask)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            //누른 상태로 가만 있거나, 움직이거나 (어쨋든 누르고 있는 상태인 것)
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
        // 마우스 위치를 월드 좌표로 변환
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 2D Raycast 실행 (Vector2.zero 방향은 포인트 클릭과 동일)
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