using UnityEngine;
using UnityEngine.UI;

public class UIImageShaker : MonoBehaviour
{
    public RectTransform targetUI; // 震わせたい画像 (Image)
    public float threshold = 50f;  // この値を超えると震え始める
    public float shakeAmount = 5f; // 基本の震えの強さ
    public float shakeSpeed = 20f; // 震えの速さ

    private Vector3 initialPos;     // 元の位置
    private float currentValue = 0; // 外部から設定する値（例: スコア）

    void Start()
    {
        if (targetUI == null) targetUI = GetComponent<RectTransform>();
        initialPos = targetUI.localPosition;
    }

    void Update()
    {
        if (currentValue > threshold)
        {
            // 値が大きいほど震えが激しくなる
            float intensity = (currentValue - threshold) * 0.1f;

            float offsetX = Mathf.Sin(Time.time * shakeSpeed) * (shakeAmount + intensity);
            float offsetY = Mathf.Cos(Time.time * shakeSpeed * 1.3f) * (shakeAmount + intensity);

            targetUI.localPosition = initialPos + new Vector3(offsetX, offsetY, 0);
        }
        else
        {
            // 値が小さいときは元の位置に戻す
            targetUI.localPosition = initialPos;
        }
    }

    // 外部から値をセットする関数
    public void SetValue(float newValue)
    {
        currentValue = newValue;
    }
}
