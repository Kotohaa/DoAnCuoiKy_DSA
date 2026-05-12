using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Quản lý trạng thái và luồng điều khiển chính của trò chơi.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _GameOverCanvas; //input: Canvas hiển thị khi kết thúc game

    /// <summary>
    /// Khởi tạo Singleton và đặt tốc độ game về bình thường.
    /// </summary>
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Kích hoạt trạng thái kết thúc game: Hiện UI và dừng thời gian.
    /// </summary>
    public void GameOver()
    {
        UIManager.Instance.PushMenu(_GameOverCanvas);
        Time.timeScale = 0f;
    }

}