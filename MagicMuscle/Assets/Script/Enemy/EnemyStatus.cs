using UnityEngine;
public class EnemyStatus : MonoBehaviour
{
    ////アニメーションを取得
    public EnemyAnima enemyanimator;
    public int maxHP = 100;
    //自分の体力
    private int currentHP;
    //スコア
    public int scoreadd = 100;

    public bool IsDead => currentHP <= 0;



    void Awake()
    {
        //HPを設定
        currentHP = maxHP;
        enemyanimator = GetComponent<EnemyAnima>();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Score.score += scoreadd;
        enemyanimator.PlayRandomAnimation();
    }

    public int GetHP()
    {
        return currentHP;
    }



   
}
