using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 振動の強さ
    public float shakeAmount = 0.1f;
    // 振動の時間
    public float shakeDuration = 0.3f;

    private Vector3 originalPos;

    void OnEnable()
    {
        originalPos = transform.localPosition;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
