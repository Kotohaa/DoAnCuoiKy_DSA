using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    /// <summary>
    /// Khởi tạo Singleton để quản lý UI toàn cục, đảm bảo chỉ có một instance duy nhất của UIManager tồn tại trong suốt vòng đời của ứng dụng.
    /// </summary>
    private void Awake()
    {
        // Khởi tạo Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Đảm bảo chỉ có duy nhất 1 UIManager
        }
    }
    /// <summary>
    /// Cấu trúc dữ liệu Stack để lưu trữ lịch sử các Menu đã mở
    /// </summary>
    private Stack<GameObject> menuStack = new Stack<GameObject>();
    

    [Header("Cấu hình Panels")]
    [SerializeField] private GameObject mainMenuPanel; //input: Panel chính của menu
    [SerializeField] private GameObject gameOverPanel; //input: Panel hiển thị khi kết thúc game
    [SerializeField] private GameObject leaderboardPanel; //input: Panel hiển thị bảng xếp hạng
    [SerializeField] private GameObject pausedmenuPanel; //input: Panel hiển thị khi tạm dừng game
    [Header("Cấu hình Cảnh")]
    [SerializeField] private string gameplaySceneName = "Game"; // Tên scene chạy game


    // Hàm khởi đầu, hiển thị menu chính khi trò chơi bắt đầu.
    void Start()
    {
        PushMenu(mainMenuPanel);
    }

    // Mở bảng xếp hạng
    public void ToLeaderboard()
    {
        PushMenu(leaderboardPanel);
    }

    //Thoát bảng xếp hạng
    public void BackFromLeaderboard()
    {
        PopMenu();
    }

    //Kích hoạt menu pause game
    public void ToPausedMenu()
    {
        Time.timeScale = 0f;
        PushMenu(pausedmenuPanel);
    }

    //Thoát menu pause game
    public void BackformPausedMenu()
    {
        Time.timeScale = 1f;
        PopMenu();
    }


    // Hàm mở một Menu mới bằng cách push vào stack và hiển thị nó, đồng thời ẩn menu hiện tại nếu có.
    public void PushMenu(GameObject nextMenu)
    {
        if (nextMenu == null) return;

        // Nếu đang có menu nào đó mở, ẩn nó đi nhưng vẫn giữ trong Stack
        if (menuStack.Count > 0)
        {
            menuStack.Peek().SetActive(false);
        }

        // Đẩy menu mới vào đỉnh Stack và hiển thị nó
        menuStack.Push(nextMenu);
        nextMenu.SetActive(true);
    }

    // Hàm quay lại Menu trước đó (Pop khỏi Stack)
    public void PopMenu()
    {
        if (menuStack.Count > 0)
        {
            // Lấy menu hiện tại ra và tắt nó đi
            GameObject current = menuStack.Pop();
            current.SetActive(false);

            // Hiển thị lại menu nằm ngay bên dưới trong Stack
            menuStack.Peek().SetActive(true);
        }
    }

    //Hàm bắt đầu trò chơi (Chuyển Scene)
    public void StartGame()
    {
        // Xóa sạch Stack trước khi chuyển cảnh để giải phóng bộ nhớ
        menuStack.Clear();
        SceneManager.LoadScene(gameplaySceneName);
    }

    // Hàm thoát ứng dụng
    public void QuitGame()
    {
        Application.Quit();
    }
}