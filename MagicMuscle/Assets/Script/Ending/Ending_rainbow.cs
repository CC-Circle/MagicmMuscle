using UnityEngine;
using UnityEngine.UI;

public class Ending_rainbow : MonoBehaviour
{
    public Image targetImage;       // 対象のUI Image
    public float speed = 1f;        // 虹色の変化スピード
    public float blinkSpeed = 2f;   // 点滅スピード（速さ）

    void Update()
    {
        if (targetImage == null) return;

        // HSV色相(H)を時間で変化させて虹色に
        float h = Mathf.Repeat(Time.time * speed, 1f); // 0～1をループ
        Color rainbowColor = Color.HSVToRGB(h, 1f, 1f);

        // サイン波でアルファを点滅
        float alpha = (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f; // 0～1

        rainbowColor.a = alpha; // アルファに点滅を反映
        targetImage.color = rainbowColor;
    }
}
