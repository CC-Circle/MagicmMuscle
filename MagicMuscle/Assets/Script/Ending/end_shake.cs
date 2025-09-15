using UnityEngine;
using DG.Tweening;

public class end_shake : MonoBehaviour
{
    public RectTransform targetUI;

    void Start()
    {
        if (targetUI == null)
            targetUI = GetComponent<RectTransform>();
        
        // 振動（0.5秒、強さ=10、振動回数=20、ランダム性あり）
        targetUI.DOShakeAnchorPos(100f, 50f, 100, 90, true, true);
    }
    private void Update()
    {
        this.transform.SetAsLastSibling();  // 一番上（前面）
    }
}
