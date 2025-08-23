using UnityEngine;
using System.Collections;
public class EnemyStatus : MonoBehaviour
{
    ////アニメーションを取得
    public EnemyAnima enemyanimator;
    public EnemyScriptAnimation scriptanima;
    public int maxHP = 100;
    //自分の体力
    private int currentHP;
    //スコア
    public int scoreadd = 100;

    public bool IsDead => currentHP <= 0;

    public float activeTime = 7;

    private Slider slider;

    //マッスルモードのはやさ
    public float musclemodespeed = 0.02f;
    public  float TargetPointZ;

    public bool dieing = false;

    void Awake()
    {
        //HPを設定
        currentHP = maxHP;
        enemyanimator = GetComponent<EnemyAnima>();
        scriptanima = transform.Find("Main").GetComponent<EnemyScriptAnimation>();
        slider = GameObject.Find("dash_slider").GetComponent<Slider>();
        StartCoroutine(nonActive());
    }

    public void TakeDamage(int damage)
    {
        //scriptanima.StartFlashingMaterial(0.1f,0.1f);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    public void Update()
    {
        if (GameManager.muscleTime) {
            muscleMode();
        }

    }
    void Die()
    {
        dieing = true;
        slider.CollectObject();
        Score.score += scoreadd;
        enemyanimator.PlayRandomAnimation();
    }

    public int GetHP()
    {
        return currentHP;
    }
    IEnumerator nonActive ()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(this.gameObject);
    }

    public void muscleMode()
    {
        if(this.transform.position.z < TargetPointZ&&!dieing)
        {
            this.transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z - musclemodespeed);
        }
        
    }
}
