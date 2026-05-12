using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FuelController : MonoBehaviour
{
    public static FuelController instance;

    [Header("UI Settings")]
    [SerializeField] private Image _FuelImage;
    [SerializeField] private Gradient _FuelGradient;

    [Header("Fuel Logic")]
    [SerializeField, Range(0.1f, 5f)] private float _FuelDrainSpeed = 1f;
    [SerializeField] private float _MaxFuel = 20f;
    private float _CurrentFuel;

    [Header("Spawn Settings")]
    [SerializeField] private GameObject _FuelPrefab;    //input: Prefab bình xăng
    [SerializeField] private Transform _Player;        //input: Player
    [SerializeField] private LayerMask _GroundLayer;   //input: Layer của mặt đất
    [SerializeField] private float _MinDistance = 15f; //input: Khoảng cách xuất hiện tối thiểu
    [SerializeField] private float _MaxDistance = 25f; //input: Khoảng cách xuất hiện tối đa

    /// <summary>
    /// Thiết lập singleton instance để dễ dàng truy cập từ các script khác.
    /// </summary>
    private void Awake()
    {
        if (instance == null) instance = this;
    }


    /// <summary>
    /// Khởi tạo nhiên liệu hiện tại bằng với nhiên liệu tối đa và cập nhật giao diện người dùng.
    /// </summary>
    private void Start()
    {
        _CurrentFuel = _MaxFuel;
        UpdateUI();
    }

    /// <summary>
    /// Cập nhật lượng nhiên liệu mỗi frame và kiểm tra xem nhiên liệu đã hết chưa.
    /// </summary>
    private void Update()
    {
        _CurrentFuel -= Time.deltaTime * _FuelDrainSpeed;
        UpdateUI();

        if (_CurrentFuel <= 0f)
        {
            _CurrentFuel = 0f;
            GameManager.instance.GameOver();
        }
    }
    /// <summary>
    /// Cập nhật giao diện người dùng dựa trên lượng nhiên liệu hiện tại, bao gồm việc điều chỉnh fillAmount của Image và thay đổi màu sắc dựa trên Gradient.
    /// </summary>
    private void UpdateUI()
    {
        float fillAmount = _CurrentFuel / _MaxFuel;
        _FuelImage.fillAmount = fillAmount;
        _FuelImage.color = _FuelGradient.Evaluate(fillAmount);
    }

    /// <summary>
    /// Hồi xăng và tạo bình mới ở phía trước.
    /// </summary>
    public void FillFuel()
    {
        _CurrentFuel = _MaxFuel;
        UpdateUI();
        SpawnNextFuel(); // Gọi hàm tạo bình xăng mới
    }



    /// <summary>
    /// Tìm vị trí X ngẫu nhiên phía trước người chơi, bắn tia Ray từ trên trời xuống để tìm tọa độ Y của mặt đất, và tạo bình xăng mới tại điểm chạm mặt đất.
    /// Nếu không tìm thấy đất (rơi vào hố), hàm sẽ tự gọi lại để thử tạo bình xăng ở một vị trí khác, đảm bảo rằng bình xăng luôn được tạo trên mặt đất và không bị lún xuống hố.
    /// </summary>
    private void SpawnNextFuel()
    {
        //Tìm vị trí X ngẫu nhiên phía trước người chơi
        float spawnX = _Player.position.x + Random.Range(_MinDistance, _MaxDistance);

        //Bắn tia Ray từ trên trời xuống để tìm tọa độ Y của mặt đất
        Vector2 rayOrigin = new Vector2(spawnX, 20f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 40f, _GroundLayer);

        if (hit.collider != null)
        {
            //Tạo bình xăng mới tại điểm chạm mặt đất (cộng thêm 0.5f để không bị lún)
            Vector3 spawnPos = new Vector3(hit.point.x, hit.point.y + 0.8f, 0);
            Instantiate(_FuelPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            //Nếu không tìm thấy đất thì thử lại ở một vị trí khác
            SpawnNextFuel();
        }
    }
}