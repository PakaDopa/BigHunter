using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isInGameEnd = false;


    public void GameEnd()
    {
        Time.timeScale = 1f;
        isInGameEnd = true;
        EventManager.Instance.PostNotification(MEventType.GameEnd, this, new TransformEventArgs(transform, false));
    }
    public void GameWin()
    {
        Time.timeScale = 1f;
        isInGameEnd = true;
        EventManager.Instance.PostNotification(MEventType.GameEnd, this, new TransformEventArgs(transform, true));
        
    }
    public override void Init()
    {
    }
}
