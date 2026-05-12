using UnityEngine;

/// <summary>
///Thuật toán Perlin Noise để tạo ra các giá trị noise mượt mà, tự nhiên
///CustomPerlin cung cấp một phương thức Perlin01 để trả về giá trị noise trong khoảng [0,1] để dễ dàng tích hợp vào hệ thống sinh địa hình
/// </summary>
public class CustomPerlin
{
    /// <summary>
    /// Làm mượt interpolation.
    /// </summary>
    private float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    /// <summary>
    /// Nội suy tuyến tính.
    /// </summary>
    private float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }

    /// <summary>
    /// Sinh gradient pseudo-random.
    /// </summary>
    private float Gradient(int x)
    {
        x = (x << 13) ^ x;

        return 1.0f -
              ((x * (x * x * 15731 + 789221)
              + 1376312589)
              & 0x7fffffff)
              / 1073741824f;
    }

    /// <summary>
    /// Sinh Perlin Noise 1D.
    /// </summary>
    public float Perlin1D(float x)
    {
        int x0 = Mathf.FloorToInt(x);
        int x1 = x0 + 1;

        float t = x - x0;

        float fadeT = Fade(t);

        float g0 = Gradient(x0);
        float g1 = Gradient(x1);

        float d0 = t;
        float d1 = t - 1f;

        float v0 = g0 * d0;
        float v1 = g1 * d1;

        return Lerp(v0, v1, fadeT);
    }

    /// <summary>
    /// Trả về Perlin Noise trong khoảng [0,1].
    /// </summary>
    public float Perlin01(float x)
    {
       return (Perlin1D(x) + 1f) * 0.5f;
    }
}