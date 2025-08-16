using UnityEngine;

public class Pointing : MonoBehaviour
{
    public RectTransform pointer; // UIのポインター画像

    // 0〜1で指定する正規化座標
    //public float normalizedX;
    public float normalizedY;

    void Update()
    {
        // 1920x1080のピクセル座標へ変換
        float pixelX = (OrangePointer.pointerX * 1920)-1920/2;
        float pixelY = (OrangePointer.pointerY * 1080)-1080f/2;

        // UIの座標に変換して適用
        pointer.anchoredPosition = new Vector2(pixelX, pixelY);

 }
}