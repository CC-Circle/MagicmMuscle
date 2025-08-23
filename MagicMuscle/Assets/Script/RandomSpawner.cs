
using System.Collections;
using System.Numerics;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] spawnPrefabs; // 生成するオブジェクトの種類
    public float interval = 2f;       // 一定間隔（秒）
    public Transform spawnPoint;      // 生成位置（省略可）
    public int angle = 90;
    public int InitSpawn = 50;
    public UnityEngine.Vector3 InitDis;
    void Start()
    {
        StartCoroutine(SpawnLoop());
        for(int i = 0; i < InitSpawn; i++)
        {
            int index = UnityEngine.Random.Range(0, spawnPrefabs.Length);
            GameObject prefab = spawnPrefabs[index];
            UnityEngine.Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
            Instantiate(prefab, this.transform.position+ InitDis*i, prefab.transform.rotation);
        }
        SpawnRandomObject();
    }
    void Update()
    {

    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnRandomObject();
            yield return new WaitForSeconds(interval);
        }
    }

    void SpawnRandomObject()
    {
        int index = UnityEngine.Random.Range(0, spawnPrefabs.Length);
        GameObject prefab = spawnPrefabs[index];

        UnityEngine.Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        Instantiate(prefab, this.transform.position, prefab.transform.rotation);
    }
}
