using System;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UI_ShowDamage : MonoBehaviour
{
    [SerializeField] TMP_Text damagePrefab;

    private void Awake()
    {
        EventManager.Instance.AddListener(MEventType.ApplyDamage, ShowDamage);
    }
    private void ShowDamage(MEventType MEventType, Component Sender, EventArgs args = null)
    {
        TransformEventArgs tArgs = args as TransformEventArgs;
        int damage = (int)tArgs.value[0]; ;
        bool cirtical = (bool)tArgs.value[1];

        Vector3 worldPos = (Vector3)tArgs.value[2]; // 월드 좌표
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos); // 스크린 좌표

        RectTransform canvasRect = transform.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            Camera.main,
            out Vector2 localPoint
        );
        var prefab = Instantiate(damagePrefab, transform);
        RectTransform rect = prefab.GetComponent<RectTransform>();
        rect.anchoredPosition = localPoint;
        prefab.GetComponent<DamageText>().Setup(damage, cirtical);
    }
}
