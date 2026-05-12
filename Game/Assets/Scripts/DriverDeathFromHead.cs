using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DriverDeathFromHead : MonoBehaviour
{
    /// <summary>
    /// Kiểm tra va chạm với "Ground", nếu có thì gọi hàm GameOver() của GameManager.
    /// Để Xử lý logic khi người lái va chạm với mặt đất và kết thúc trò chơi.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            GameManager.instance.GameOver();
        }
    }
}
