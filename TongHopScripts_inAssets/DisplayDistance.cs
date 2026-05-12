using UnityEngine;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Quản lý tính toán quãng đường di chuyển và hệ thống lưu trữ điểm cao nhất (High Score) cho trò chơi.
/// Cung cấp chức năng hiển thị quãng đường hiện tại và điểm cao nhất trên giao diện người dùng, đồng thời lưu trữ và quản lý danh sách điểm cao nhất một cách hiệu quả.
/// </summary>
public class DisplayDistance : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _DistanceText; //input: Text hiển thị quãng đường hiện tại
    [SerializeField] private TextMeshProUGUI _HighScoreText; //input: Text hiển thị điểm cao nhất (High Score)

    [Header("Player Settings")]
    [SerializeField] private Transform _PlayerTrans; //input: Transform của người chơi để tính toán quãng đường di chuyển
    
    private Vector2 _StartPosition;
    private float _currentDistance = 0f;

    /// <summary>
    /// Danh sách động lưu trữ 5 mức điểm cao nhất.
    /// Áp dụng cấu trúc dữ liệu List để tối ưu việc thêm và sắp xếp phần tử.
    /// </summary>
    private List<float> _topScores = new List<float>();

    /// <summary>
    /// start() được gọi một lần khi đối tượng này được kích hoạt.
    /// Thiết lập vị trí bắt đầu của người chơi và tải dữ liệu điểm cao nhất từ bộ nhớ, đồng thời cập nhật giao diện người dùng với điểm cao nhất hiện tại.
    /// </summary>
    private void Start()
    {
        _StartPosition = _PlayerTrans.position;
        LoadTopScores();
        DisplayHighScore();
    }

    private void Update()
    {
        CalculateDistance();
    }

    /// <summary>
    /// Tính toán khoảng cách theo trục X và cập nhật lên giao diện người dùng.
    /// </summary>
    private void CalculateDistance()
    {
        float distanceX = _PlayerTrans.position.x - _StartPosition.x;
        _currentDistance = Mathf.Max(0, distanceX); // Đảm bảo quãng đường không âm
        _DistanceText.text = _currentDistance.ToString("F0") + "m";
    }

    /// <summary>
    /// Xử lý cập nhật điểm mới vào bảng xếp hạng. 
    /// Sử dụng Sort và duy trì kích thước danh sách cố định (Top 5).
    /// </summary>
    public void SaveCurrentScore()
    {
        _topScores.Add(_currentDistance);

        // Sắp xếp danh sách giảm dần để lấy các mốc khoảng cách lớn nhất
        _topScores.Sort((a, b) => b.CompareTo(a));

        // Giải thuật cắt tỉa: Chỉ giữ lại 5 điểm cao nhất để tối ưu bộ nhớ
        if (_topScores.Count > 5)
        {
            _topScores.RemoveAt(_topScores.Count - 1);
        }

        SaveToMemory();
        DisplayHighScore();
    }

    /// <summary>
    /// Cập nhật giá trị điểm cao nhất lên UI.
    /// </summary>
    private void DisplayHighScore()
    {
        float topScore = (_topScores.Count > 0) ? _topScores[0] : 0f;
        _HighScoreText.text = $"High Score: {topScore:F0}m";
    }

    /// <summary>
    /// Chuyển đổi danh sách điểm sang chuỗi định dạng CSV để lưu vào PlayerPrefs.
    /// </summary>
    private void SaveToMemory()
    {
        string saveData = string.Join(",", _topScores);
        PlayerPrefs.SetString("TopScores", saveData);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Tải dữ liệu từ PlayerPrefs và giải mã chuỗi thành danh sách điểm cao.
    /// </summary>
    private void LoadTopScores()
    {
        string rawData = PlayerPrefs.GetString("TopScores", "");
        if (string.IsNullOrEmpty(rawData)) return;

        string[] scores = rawData.Split(',');
        _topScores.Clear();

        foreach (string s in scores)
        {
            if (float.TryParse(s, out float result))
                _topScores.Add(result);
        }
    }
}