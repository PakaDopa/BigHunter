using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public void Setup(int damage, bool cirtical)
    {
        var text = GetComponent<TMP_Text>();
        text.text = damage.ToString();
        if (cirtical)
            text.fontStyle = FontStyles.Bold;
        else
            text.fontStyle = FontStyles.Normal;


        //effect
        var rt = GetComponent<RectTransform>();
        Vector2 startAnchoredPos = rt.anchoredPosition;
        Sequence seq = DOTween.Sequence();
        seq
            .Append(rt.DOAnchorPosY(startAnchoredPos.y + 40, 1f))
            .Join(text.DOFade(0f, 1f))
            .OnComplete(() => Destroy(gameObject));

    }
}
