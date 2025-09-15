using UnityEngine;

public class UI_Forword : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.SetAsLastSibling();  // 一番上（前面）
    }
}
