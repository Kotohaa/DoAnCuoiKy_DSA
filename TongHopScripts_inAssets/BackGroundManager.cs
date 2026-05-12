using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform mainCam; // input: Camara chính của scene
    public Transform midBg; // input: Tấm background chính 
    public Transform sideBg; // input: Tấm background bên cạnh
    public float length; // length của background

    /// <summary>
    /// Khởi tạo chiều rộng của background bằng cách lấy kích thước của SpriteRenderer của midBg.
    /// Thiết lập ban đầu cho hệ thống background, đảm bảo background có chiều rộng chính xác để cuộn mượt mà.
    /// </summary>
    void Start()
    {
        // Tự lấy chiều rộng chuẩn của Sprite
        length = midBg.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    /// <summary>
    /// Kiểm tra vị trí của camera chính và cập nhật vị trí background nếu cần thiết để tạo hiệu ứng cuộn.
    /// Là vòng lặp chính để duy trì background cuộn theo chuyển động của camera, đảm bảo trải nghiệm người chơi mượt mà.
    /// </summary>
    void Update()
    {
        if(mainCam.position.x > midBg.position.x)
        {
            UpdateBackgroundPosition(Vector3.right);
        }
        else if(mainCam.position.x < midBg.position.x)
        {
            UpdateBackgroundPosition(Vector3.left);
        }
    }

    /// <summary>
    /// Cập nhật vị trí của sideBg và hoán đổi vai trò giữa midBg và sideBg để tạo hiệu ứng cuộn background liên tục.
    /// Xử lý logic di chuyển background khi camera vượt qua vị trí hiện tại, đảm bảo background không bị đứt đoạn..
    /// </summary>
    void UpdateBackgroundPosition(Vector3 direction)
    {
        //nhân với (length - 0.36f) để hai tấm bg đè lên nhau một chút tránh kẻ hở.
        sideBg.position = midBg.position + direction * (length - 0.36f);
        
        Transform temp = midBg;
        midBg = sideBg;
        sideBg = temp;
    }
}