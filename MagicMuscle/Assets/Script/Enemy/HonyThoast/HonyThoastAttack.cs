using UnityEngine;
using System.Collections;
public class HonyThoastAttack : MonoBehaviour
{
    public Vector3 fixVector3;
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
            Instantiate(shootObject, this.transform.position+fixVector3, Quaternion.identity);
            StartCoroutine(Attack(wattime));
        }

    }

   
}
