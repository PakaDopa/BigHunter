using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool isInGameEnd = false;
    public bool IsInGameEnd { get { return isInGameEnd; } }
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
    public void GameRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public override void Init()
    {
        isInGameEnd = false;
    }
}
