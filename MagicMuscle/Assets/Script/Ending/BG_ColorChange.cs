using UnityEngine;
using UnityEngine.UI;
public class BG_ColorChange : MonoBehaviour
{
    public Color startColor = Color.white;   // 開始色
    public Color endColor = Color.red;       // 終了色
    public float duration = 2f;              // 変化にかける時間（秒）

    private RawImage image;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<RawImage>();
        if (image != null)
        {
            image.color = startColor; // 初期色設定
        }
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
       

       
    }
    public void ColorLerp()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration); // 0→1 に正規化
            image.color = Color.Lerp(startColor, endColor, t);
        }
    }
}
