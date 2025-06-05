using System;
using TMPro;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public enum GameMode
    {
        Mode2,  //화살의 개수 제한
        Mode3,  //화살의 개수 제한 + 약점
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
        //로직 수정해야함.
        //bug 1. weapon이 0이 되자마자 게임이 패배하기 때문에, 그 던진 창이 몬스터의 체력 0이 됐을 때를 고려하지 않음.
        //  -> 이벤트 트리거 발생 순서를 다시 생각해야함.
        weaponCount -= 1;
        text.text = "X" + weaponCount.ToString(); 

        if (weaponCount == 0)
            GameManager.Instance.GameEnd();
    }
}
