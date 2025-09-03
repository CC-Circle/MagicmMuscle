using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("回転設定")]
    public Vector3 rotationAxis = Vector3.up; // 回転方向（デフォルトはY軸）
    public float rotationSpeed = 90f;         // 回転速度（度/秒）
    public float rotationTime = 0f;           // 回転時間（秒、0なら無制限）

    private float elapsedTime = 0f;
    private bool isRotating = true;

    void Update()
    {
        if (!isRotating) return;

        // 回転
        transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime);

        // 制限時間がある場合
        if (rotationTime > 0f)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= rotationTime)
            {
                isRotating = false;
            }
        }
    }

    /// <summary>
    /// 回転を再開（任意の時間指定も可能）
    /// </summary>
    public void StartRotation(float time = 0f)
    {
        rotationTime = time;
        elapsedTime = 0f;
        isRotating = true;
    }

    /// <summary>
    /// 回転を停止
    /// </summary>
    public void StopRotation()
    {
        isRotating = false;
    }
}
