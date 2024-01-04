using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject[] prefabToSpawn; // 생성할 프리팹
    public float spawnInterval = 10f; // 생성 간격 (초)

    private float timer = 0f;
    private float elapsedTime = 0f;
    public Transform target;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 3f) // 5초 경과 시간마다
        {
            if(spawnInterval > 1f)
			{
                spawnInterval -= 1f; // spawnInterval을 1씩 증가
                elapsedTime = 0f; // 경과 시간 초기화
            }
            else if (spawnInterval <= 1f)
            {
                spawnInterval -= .1f; // spawnInterval을 1씩 증가
                elapsedTime = 0f; // 경과 시간 초기화
            }
        }
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPrefab();
            timer = 0f;
        }
    }

    void SpawnPrefab()
    {
        int randomNumber = Random.Range(0,3);
        // 랜덤한 위치 생성
        float randomX = Random.Range(-30f, 30f);
        float randomZ = Random.Range(-30f, 30f);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, randomZ);
        GameObject newPrefab = Instantiate(prefabToSpawn[randomNumber], spawnPosition, Quaternion.identity);
        Enemy enemy = newPrefab.GetComponent<Enemy>();
        enemy.target = target;
    }
}
