using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectFuel : MonoBehaviour
{
    /// <summary>
    /// Kiểm tra va chạm với "Player", nếu có thì gọi hàm FillFuel() của FuelController và xóa đối tượng này.
    ///Thu thập nhiên liệu khi người chơi va chạm với đối tượng này.
    ///Cung cấp nhiên liệu cho người chơi và loại bỏ đối tượng sau khi thu thập, đảm bảo gameplay mượt mà.
    ///Inputs: collision (Collider2D): Đối tượng va chạm với trigger này, được sử dụng để xác định nếu đó là "Player".
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            FuelController.instance.FillFuel();
            Destroy(gameObject);
        }
    }
}
