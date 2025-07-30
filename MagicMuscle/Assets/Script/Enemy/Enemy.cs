using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int animation_cnt = 0;
    public int animation_speed = 20;
    public int scoreadd = 200;
    private CameraShake camerashake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreadd = 5;
        camerashake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animation_cnt++;
        if (animation_speed==animation_cnt)
        {
            animation_cnt = 0;
            Material mat = GetComponent<Renderer>().material;
            Vector2 tiling = mat.mainTextureScale;
            tiling.x *= -1; // x方向に反転
            mat.mainTextureScale = tiling;
        }
           
    }
}
