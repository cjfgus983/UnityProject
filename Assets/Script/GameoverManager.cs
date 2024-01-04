using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverManager : MonoBehaviour
{
    public GameObject playerObject; // 플레이어 오브젝트
    public GameObject gameOverPanel; // 게임 오버 UI 패널
    int hp;

    private bool isGameOver = false;

    void Update()
    {
        if (playerObject != null)
        {
            Player player = playerObject.GetComponent<Player>(); // 여기서 플레이어 스크립트를 가져옵니다.
            if (player != null)
            {
                hp = player.health; // 플레이어 스크립트에서 체력을 가져옵니다.
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
