using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;

[ExecuteInEditMode]
public class EnvironmentGenerator : MonoBehaviour
{
    public SpriteShapeController _SpriteShapeController;
    private CustomPerlin _customPerlin = new CustomPerlin();

    [Header("Terrain Settings")]
    [Range(3, 100)] public int _levelLength = 50;
    [Range(1f, 50f)] public float _xMultiplier = 2f;
    [Range(1f, 50f)] public float _yMultiplier = 5f;
    [Range(0, 1f)] public float _curveSmoothness = 0.5f;
    public float _NoiseStep = 0.1f;
    public float _bottom = 10f;

    [Header("Sync Settings")]
    public float _xOffset = 0f; // Dùng để nối các đoạn địa hình khớp nhau

    private List<Vector3> _terrainPoints = new List<Vector3>();

    /// <summary>
    /// OnValidate() được gọi mỗi khi có thay đổi trong Inspector.
    /// Sinh dữ liệu địa hình mới và áp dụng lên SpriteShape để cập nhật ngay lập tức trong Editor
     /// Cho phép xem kết quả của các thay đổi tham số ngay lập tức mà không cần phải chạy game
    /// </summary>
    public void OnValidate()
    {
        if (_SpriteShapeController == null) return;

        GenerateTerrainData();
        ApplyDataToSpline();
    }

    /// <summary>
    /// Sinh dữ liệu địa hình mới dựa trên Perlin Noise và các tham số đã thiết lập.
    /// Tạo ra một danh sách các điểm Vector3 đại diện cho bề mặt địa hình rồi thêm 2 điểm chốt ở đáy để đóng kín Polygon.
    /// Sử dụng Perlin Noise để tạo ra các điểm bề mặt tự nhiên và có thể nối tiếp nhau bằng cách sử dụng _xOffset, đảm bảo tính liên tục của địa hình khi tạo nhiều đoạn khác nhau.
    /// </summary>
    private void GenerateTerrainData()
    {
        _terrainPoints.Clear();

        //Sinh các điểm bề mặt (Top points)
        for (int i = 0; i < _levelLength; i++)
        {
            float x = i * _xMultiplier;
            // Perlin Noise dựa trên vị trí i và offset để có thể nối đoạn
            float noiseX = (i + _xOffset) * _NoiseStep;
            float y = _customPerlin.Perlin01(noiseX) * _yMultiplier;

            _terrainPoints.Add(new Vector3(x, y, 0));
        }

        //Tạo 2 điểm chốt ở đáy để đóng kín Polygon
        float lastX = (_levelLength - 1) * _xMultiplier;

        //Điểm góc dưới bên phải (Cùng X với điểm cuối bề mặt)
        _terrainPoints.Add(new Vector3(lastX, -_bottom, 0));

        //Điểm góc dưới bên trái (Trở về X = 0)
        _terrainPoints.Add(new Vector3(0, -_bottom, 0));
    }


    /// <summary>
    /// Áp dụng dữ liệu địa hình đã sinh lên SpriteShapeController bằng cách chèn các điểm vào spline.
    /// Xử lý logic để làm mượt các điểm bề mặt bằng cách đặt chế độ tangent là Continuous và thiết lập độ dài tangent dựa trên _xMultiplier và _curveSmoothness.
    /// Đảm bảo rằng các điểm góc và đáy được đặt ở chế độ Linear để giữ cho các cạnh sắc nét, trong khi các điểm bề mặt được làm mượt để tạo ra một địa hình tự nhiên hơn.
    /// </summary>
    private void ApplyDataToSpline()
    {
        Spline spline = _SpriteShapeController.spline;
        spline.Clear();

        for (int i = 0; i < _terrainPoints.Count; i++)
        {
            spline.InsertPointAt(i, _terrainPoints[i]);

            // Chỉ làm mượt các điểm nằm giữa của bề mặt
            // Không làm mượt điểm đầu, điểm cuối bề mặt và 2 điểm đáy
            if (i > 0 && i < _levelLength - 1)
            {
                spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                Vector3 tangentLength = Vector3.right * _xMultiplier * _curveSmoothness;
                spline.SetLeftTangent(i, -tangentLength);
                spline.SetRightTangent(i, tangentLength);
            }
            else
            {
                //Các điểm góc và đáy phải để chế độ Linear để cạnh thẳng và sắc nét
                spline.SetTangentMode(i, ShapeTangentMode.Linear);
            }
        }
    }
}