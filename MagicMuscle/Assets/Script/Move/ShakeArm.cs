using UnityEngine;
using UnityEngine.EventSystems;

public class ShakeArm : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private bool isPressed = false;

    [Header("振動の強さ")]
    public float vibrationStrength = 10f; // 振動幅（ピクセル）
    [Header("振動の速さ")]
    public float vibrationSpeed = 20f; // 振動スピード

    private Vector3 defaultPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        if (YourPower.isMeasuring)
        {
            // 振動：sin波を使うと滑らかに揺れる
            float offsetX = Mathf.Sin(Time.time * vibrationSpeed) * vibrationStrength;
            float offsetY = Mathf.Cos(Time.time * vibrationSpeed) * vibrationStrength * 0.5f; // Y方向は半分くらい
            rectTransform.anchoredPosition = defaultPos + new Vector3(offsetX, offsetY, 0f);
        }
        else
        {
            // 押していないときは元の位置に戻す
            rectTransform.anchoredPosition = defaultPos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
