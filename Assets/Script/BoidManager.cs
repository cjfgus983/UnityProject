using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject[] prefabToSpawn; // ������ ������
    public float spawnInterval = 10f; // ���� ���� (��)

    private float timer = 0f;
    private float elapsedTime = 0f;
    public Transform target;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 3f) // 5�� ��� �ð�����
        {
            if(spawnInterval > 1f)
			{
                spawnInterval -= 1f; // spawnInterval�� 1�� ����
                elapsedTime = 0f; // ��� �ð� �ʱ�ȭ
            }
            else if (spawnInterval <= 1f)
            {
                spawnInterval -= .1f; // spawnInterval�� 1�� ����
                elapsedTime = 0f; // ��� �ð� �ʱ�ȭ
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
        // ������ ��ġ ����
        float randomX = Random.Range(-30f, 30f);
        float randomZ = Random.Range(-30f, 30f);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, randomZ);
        GameObject newPrefab = Instantiate(prefabToSpawn[randomNumber], spawnPosition, Quaternion.identity);
        Enemy enemy = newPrefab.GetComponent<Enemy>();
        enemy.target = target;
    }
}
