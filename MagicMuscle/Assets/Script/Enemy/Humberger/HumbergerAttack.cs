using UnityEngine;
using System.Collections;
public class HumbergerAttack : MonoBehaviour
{
    public GameObject shootObject;
    public float wattime;
    private EnemyStatus enemystate;
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
            Instantiate(shootObject, this.transform.position, Quaternion.identity);
        }
    }

   
}
