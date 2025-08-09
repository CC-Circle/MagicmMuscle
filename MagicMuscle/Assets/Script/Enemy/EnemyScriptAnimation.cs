using UnityEngine;
using System.Collections;
public class EnemyScriptAnimation : MonoBehaviour
{
    //画像を左右反転させて歩くアニメーション
    private int animation_cnt = 0;
    //アニメーションのスピード
    public int animation_speed = 20;

    // Update is called once per frame
    void FixedUpdate()
    {
        animation_cnt++;
        if (animation_speed==animation_cnt)
        {
            //カウンターの初期化
            animation_cnt = 0;
            //レンダラーのxを反転することで画像を反転
            Material mat = GetComponent<Renderer>().material;
            Vector2 tiling = mat.mainTextureScale;
            tiling.x *= -1; // x方向に反転
            mat.mainTextureScale = tiling;
        }
           
    }

    ///// <summary>
    ///// マテリアルを点滅させる関数
    ///// </summary>
    ///// <param name="duration">点滅する時間（秒）</param>
    ///// <param name="interval">点滅の間隔（秒）</param>
    //public void StartFlashingMaterial(float duration, float interval)
    //{
    //    StartCoroutine(FlashMaterialCoroutine(duration, interval));
    //}

    //private IEnumerator FlashMaterialCoroutine(float duration, float interval)
    //{
    //    float elapsed = 0f;
    //    bool isVisible = true;

    //    Material mat = GetComponent<Renderer>().material;
    //    Color originalColor = mat.color;

    //    while (elapsed < duration)
    //    {
    //        isVisible = !isVisible;
    //        Color newColor = originalColor;
    //        newColor.a = isVisible ? 1f : 0f;
    //        mat.color = newColor;

    //        yield return new WaitForSeconds(interval);
    //        elapsed += interval;
    //    }

    //    // 最後に元の色に戻す
    //    mat.color = originalColor;
    //}
    public void StartFlashingMaterial(float duration, float interval)
    {
        StartCoroutine(FlashMaterialCoroutine(duration, interval));
    }

    private IEnumerator FlashMaterialCoroutine(float duration, float interval)
    {
        float elapsed = 0f;
        bool isWhite = true;

        Material mat = GetComponent<Renderer>().material;
        Color originalColor = mat.GetColor("_Color");

        while (elapsed < duration)
        {
            // 白色（アルファ1）または透明（アルファ0）で切り替え
            if (isWhite)
                mat.SetColor("_Color", Color.white); // 明るく点滅
            else
                mat.SetColor("_Color", new Color(1f, 1f, 1f, 0f)); // 透明にする

            isWhite = !isWhite;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        // 終了後に元の色に戻す
        mat.SetColor("_Color", originalColor);
    }
}
