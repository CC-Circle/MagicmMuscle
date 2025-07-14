using UnityEngine;

public class ShakeHund : MonoBehaviour
{
    public ScreenToWorldShot scw;
    public float amplitude = 0.1f;  // 振幅（揺れの大きさ）
    public float frequency = 10f;   // 周波数（揺れる速さ）
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (scw.charge)
        {
            float offset = Mathf.Sin(Time.time * frequency) * amplitude;
            transform.localPosition = startPos + new Vector3(offset, 0, 0);
        }
      
    }
}
