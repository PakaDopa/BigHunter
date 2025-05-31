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
        Debug_ThrowForceText();
    }

    private void Debug_ThrowForceText()
    {
        if(parent != null)
        {
            var weapon = parent.GetComponentInChildren<WeaponBehavoiur>();
            string text = "";
            if(weapon != null)
            {
                text = "thron force: " + weapon.currentForce.ToString();
            }

            throwForceText.text = text;
        }
    }
}
