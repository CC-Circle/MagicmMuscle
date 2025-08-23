using UnityEngine;
using UnityEngine.UI;

public class UIImageShakerTest : MonoBehaviour
{
    public RectTransform targetUI; // 震わせたい画像 (Image)
    public float threshold = 50f;  // この値を超えると震え始める
    public float shakeAmount = 5f; // 基本の震えの強さ
    public float shakeSpeed = 20f; // 震えの速さ

    private Vector3 initialPos;     
    private float testValue = 0;    // テスト用の値

    void Start()
    {
        if (targetUI == null) targetUI = GetComponent<RectTransform>();
        initialPos = targetUI.localPosition;
    }

    void Update()
    {
        // 🔹 テスト用に値を増やす
        testValue += Time.deltaTime * 20f;  // 秒ごとに増加

        if (testValue > threshold)
        {
            // 値が大きいほど震えが激しくなる
            float intensity = (testValue - threshold) * 0.1f;

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
}
