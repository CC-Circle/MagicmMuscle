//using UnityEngine;
//using System.Collections;

//public class MuscleModeAttack : MonoBehaviour
//{
//    private BoxCollider BC;
//    [Header("停止時間(秒)")]
//    public float stopDuration = 2f; // 停止する時間

//    void Start()
//    {
//        BC = GetComponent<BoxCollider>();
//    }

//    void Update()
//    {
//        if (ArmAnimation.isRotating)
//        {
//            BC.enabled = true;
//        }
//        else
//        {
//            BC.enabled = false;
//        }
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Enemy"))
//        {
//            Debug.Log("Enemyに接触 → 一時停止開始");
//            StartCoroutine(PauseGame());
//        }
//    }

//    private IEnumerator PauseGame()
//    {
//        Time.timeScale = 0f; // ゲーム停止
//        yield return new WaitForSecondsRealtime(stopDuration); // 現実の時間で待つ
//        Time.timeScale = 1f; // ゲーム再開
//        Debug.Log("一時停止終了 → ゲーム再開");
//    }
//}
using UnityEngine;
using System.Collections;

public class MuscleModeAttack : MonoBehaviour
{
    private BoxCollider BC;
    [Header("停止時間(秒)")]
    public float stopDuration = 2f; // 停止する時間

    [Header("カメラシェイク設定")]
    public Camera mainCamera;          // 対象カメラ
    public float shakeMagnitude = 0.2f; // 揺れの強さ
    public float shakeSpeed = 20f;      // 揺れの速さ

    void Start()
    {
        BC = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (ArmAnimation.isRotating)
            BC.enabled = true;
        else
            BC.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Enemyに接触 → 一時停止開始");
            StartCoroutine(PauseGame());
        }
    }

    private IEnumerator PauseGame()
    {
        Time.timeScale = 0f; // ゲーム停止

        // カメラシェイク開始
        yield return StartCoroutine(ShakeCamera(stopDuration));

        // 停止時間後にゲーム再開
        Time.timeScale = 1f;
        Debug.Log("一時停止終了 → ゲーム再開");
    }

    private IEnumerator ShakeCamera(float duration)
    {
        if (mainCamera == null) yield break;

        Vector3 originalPos = mainCamera.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Mathf.Sin(elapsed * shakeSpeed) * shakeMagnitude;
            float offsetY = Mathf.Cos(elapsed * shakeSpeed) * shakeMagnitude;

            mainCamera.transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.unscaledDeltaTime; // 停止中でも動く
            yield return null;
        }

        mainCamera.transform.localPosition = originalPos; // 元に戻す
    }
}
