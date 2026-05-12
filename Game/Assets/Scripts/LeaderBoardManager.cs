using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Cấu hình dữ liệu")]
    [SerializeField] private Transform _scoreContainer; //input: Transform chứa các dòng điểm số trong UI
    [SerializeField] private GameObject _scoreRowPrefab; //input: Prefab dòng điểm số 
    private void OnEnable()
    {
        RenderLeaderboard();
    }

    /// <summary>
    /// Đọc dữ liệu, xử lý chuỗi để tách thành mảng điểm số, sau đó tạo và cập nhật UI cho từng điểm số trong bảng xếp hạng.
    /// Cung cấp cho người chơi một cái nhìn tổng quan về thành tích của họ so với các lần chơi trước đó hoặc so với những người chơi khác (nếu dữ liệu được chia sẻ).
    /// Đảm bảo rằng dữ liệu trong PlayerPrefs được cập nhật đúng cách khi người chơi đạt được điểm số mới để bảng xếp hạng luôn phản ánh chính xác thành tích của họ.
    /// </summary>
    /// </summary>
    public void RenderLeaderboard()
    {
        //Xóa các dòng cũ
        foreach (Transform child in _scoreContainer)
        {
            Destroy(child.gameObject);
        }

        //Truy xuất dữ liệu
        string rawData = PlayerPrefs.GetString("TopScores", "");
        if (string.IsNullOrEmpty(rawData)) return;

        //Xử lý dữ liệu (Tách chuỗi thành mảng)
        string[] scores = rawData.Split(',');

        //Khởi tạo UI
        for (int i = 0; i < scores.Length; i++)
        {
            GameObject row = Instantiate(_scoreRowPrefab, _scoreContainer);
            TMP_Text txt = row.GetComponent<TMP_Text>();

            if (float.TryParse(scores[i], out float scoreValue))
            {
                txt.text = $"TOP {i + 1} . . . . . {scoreValue:F0}m";
            }
            else
            {
                txt.text = $"TOP {i + 1} . . . . . 0m";
            }
        }
    }
}