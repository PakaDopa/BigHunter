using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MonsterHPbar : MonoBehaviour
{
    [SerializeField] EnemyFSM enemyFSM;
    [SerializeField] TMP_Text hpText;
    [SerializeField] Slider slider;

    private void Awake()
    {
        hpText.text = enemyFSM.hp.ToString() + " / " + enemyFSM.maxHp.ToString();
        slider.value = enemyFSM.hp / enemyFSM.maxHp;
    }

    private void Update()
    {
        hpText.text = enemyFSM.hp.ToString() + " / " + enemyFSM.maxHp.ToString();
        slider.value = enemyFSM.hp / enemyFSM.maxHp;
    }

}
