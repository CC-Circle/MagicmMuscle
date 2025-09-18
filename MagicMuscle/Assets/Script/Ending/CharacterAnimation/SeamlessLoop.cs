using UnityEngine;

public class SeamlessLoop : MonoBehaviour
{
    public AudioSource audioSource;
    public float loopStart; // ループ開始位置(秒)
    public float loopEnd;   // ループ終了位置(秒)

    void Update()
    {
        if (audioSource.time >= loopEnd)
        {
            audioSource.time = loopStart;
        }
    }
}
