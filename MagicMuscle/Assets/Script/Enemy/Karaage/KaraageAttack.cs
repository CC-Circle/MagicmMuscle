using UnityEngine;
using System.Collections;
public class KaraageAttack : MonoBehaviour
{
    public EnemyAnima enemyanima;
    public GameObject shootObject;
    public float wattime;
    private EnemyStatus enemystate;
    public int muinusScore = 100;
    public Score score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Score>();
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
            score.ScoreRed(muinusScore);
        }
    }

   
}
