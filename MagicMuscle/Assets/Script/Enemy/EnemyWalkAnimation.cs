using UnityEngine;

public class EnemyWalkAnimation : MonoBehaviour
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
}
