using System;
using TMPro;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public enum GameMode
    {
        Mode2,  //ȭ���� ���� ����
        Mode3,  //ȭ���� ���� ���� + ����
    }
    [SerializeField] private GameMode gameMode;
    [SerializeField] private int weaponCount = 15;
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        EventManager.Instance.AddListener(MEventType.Shoot, SetText);
    }

    private void SetText(MEventType MEventType, Component Sender, EventArgs args = null)
    {
        //���� �����ؾ���.
        //bug 1. weapon�� 0�� ���ڸ��� ������ �й��ϱ� ������, �� ���� â�� ������ ü�� 0�� ���� ���� ������� ����.
        //  -> �̺�Ʈ Ʈ���� �߻� ������ �ٽ� �����ؾ���.
        weaponCount -= 1;
        text.text = "X" + weaponCount.ToString(); 

        if (weaponCount == 0)
            GameManager.Instance.GameEnd();
    }
}
