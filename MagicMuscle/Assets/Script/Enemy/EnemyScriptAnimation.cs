using UnityEngine;
using System.Collections;

public class EnemyScriptAnimation : MonoBehaviour
{
    private int animation_cnt = 0;
    public int animation_speed = 20;

    public Color flashcolor;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        animation_cnt++;
        if (animation_cnt >= animation_speed)
        {
            animation_cnt = 0;

            if (spriteRenderer != null)
            {
                // 左右反転
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
           
            // または transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    /// <summary>
    /// 点滅処理開始
    /// </summary>
    public void StartFlashing(float duration, float interval)
    {
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashCoroutine(duration, interval));
        }
        
    }

    private IEnumerator FlashCoroutine(float duration, float interval)
    {
        float elapsed = 0f;
        bool isVisible = true;

        Color originalColor = spriteRenderer.color;

        while (elapsed < duration)
        {
            isVisible = !isVisible;

            if (isVisible)
            spriteRenderer.color = new Color(flashcolor.r,flashcolor.g,flashcolor.b, spriteRenderer.color.a); // 完全な赤＋元の透明度
            //spriteRenderer.color = flashcolor; // 白く点滅
            else
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // 透明

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        // 終了後に元の色に戻す
        spriteRenderer.color = originalColor;
    }
}
