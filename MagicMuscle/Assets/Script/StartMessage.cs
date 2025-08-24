using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMessage : MonoBehaviour
{
    public Image targetImage;         // 入れ替えたいUIのImage
    public Sprite newSprite;          // 差し替え用の画像

    private bool isactive = true;
    public float WaitTime = 1;

    private void Update()
    {
        if (GameManager.GameStart&&isactive) {
            isactive = false;
                targetImage.sprite = newSprite;
            StartCoroutine(DestroyMy());
        }

    }
    IEnumerator DestroyMy()
    {
        yield return new WaitForSeconds(WaitTime);
        targetImage.enabled = false;  // 画像が非表示になる
    }
}
