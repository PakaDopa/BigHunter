using UnityEngine;

public class InitManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.Init();
        EventManager.Instance.Init();
    }
}
