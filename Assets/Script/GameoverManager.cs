using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverManager : MonoBehaviour
{
    public GameObject playerObject; // �÷��̾� ������Ʈ
    public GameObject gameOverPanel; // ���� ���� UI �г�
    int hp;

    private bool isGameOver = false;

    void Update()
    {
        if (playerObject != null)
        {
            Player player = playerObject.GetComponent<Player>(); // ���⼭ �÷��̾� ��ũ��Ʈ�� �����ɴϴ�.
            if (player != null)
            {
                hp = player.health; // �÷��̾� ��ũ��Ʈ���� ü���� �����ɴϴ�.
            }
        }

        if (hp <= 0 && !isGameOver)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        isGameOver = true;
    }
}
