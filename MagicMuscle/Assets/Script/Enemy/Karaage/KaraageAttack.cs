using UnityEngine;
using System.Collections;
public class KaraageAttack : MonoBehaviour
{
    public EnemyAnima enemyanima;
    public GameObject shootObject;
    public float wattime;
    private EnemyStatus enemystate;
    public int muinusScore = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemystate = GetComponent<EnemyStatus>();
        StartCoroutine(Attack(wattime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Attack(float waittime)
    {
        yield return new WaitForSeconds(waittime);

        if (!enemystate.IsDead) {
            enemyanima.Splash();
        }


    }

    public void SplachAttack() {
        if (!enemystate.IsDead)
        {
            OilControle.isOil = true;
            Score.score -= muinusScore;
        }
    }

   
}
