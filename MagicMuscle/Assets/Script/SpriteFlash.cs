using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    public SliderCharge slidercharge;
    public Color setColor;
    public float flashDuration = 0.1f; // 白く光っている時間
    private bool isFlashing = false;
    private float timer = 0f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // 外部から呼ぶ（ダメージ時など）
    public void Flash()
    {
        isFlashing = true;
        timer = 0f;
    }

    void Update()
    {

        if (isFlashing)
        {
            timer += Time.deltaTime;
            float t = timer / flashDuration;

            // t=0 のとき白、t=1 のとき元の色になるよう補間
            if (slidercharge.charge==1)
            {
                sr.color = Color.Lerp(Color.yellow, originalColor, t);
            }
            else if (slidercharge.charge == 2)
            {
                sr.color = Color.Lerp(Color.green, originalColor, t);
            }else if (slidercharge.charge == 3)
            {
                sr.color = Color.Lerp(Color.red, originalColor, t);
            }

            if (t >= 1f)
            {
                isFlashing = false;
                sr.color = originalColor;
            }
        }
    }
}
