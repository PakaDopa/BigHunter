using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DebugHelper : MonoBehaviour
{
    [Header("던지는 힘 디버깅 셋팅")]
    [SerializeField] TMP_Text throwForceText;
    [SerializeField] Transform parent;

    private void Update()
    {
        string text = "";
        text += Debug_ThrowForceText() + "\n";
        text += Debug_IsInGame();
        throwForceText.text = text;
    }
    private string Debug_IsInGame()
    {
        return "Game End? -> " + GameManager.Instance.IsInGameEnd.ToString();
    }
    private string Debug_ThrowForceText()
    {
        string text = "";
        if(parent != null)
        {
            var weapon = parent.GetComponentInChildren<WeaponBehavoiur>();
            if(weapon != null)
                text += "throw force: " + weapon.currentForce.ToString();
        }
        return text;
    }
}
