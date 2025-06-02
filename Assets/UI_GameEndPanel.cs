using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_GameEndPanel : MonoBehaviour
{
    [SerializeField] RectTransform gameEndPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameEndPanel.gameObject.SetActive(false);

        //event add
        EventManager.Instance.AddListener(MEventType.GameEnd, ShowGameEndPanel);
    }
    private void ShowGameEndPanel(MEventType MEventType, Component Sender, EventArgs args = null)
    {
        gameEndPanel.gameObject.SetActive(true);

        TransformEventArgs tArgs = args as TransformEventArgs;
        bool isWin = (bool)tArgs.value[0];
        gameEndPanel.GetComponentInChildren<TMP_Text>().text = isWin ? "Game Win!" : "Game End";
        //gameEndPanel.GetComponentInChildren<Button>().onClick.AddListener(RetryButton);
    }
    public void RetryButton()
    {
        GameManager.Instance.GameRetry();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
