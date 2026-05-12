using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameOver  : MonoBehaviour
{
    /// <summary>
    /// Lưu điểm và nhảy qua scene Menu
    /// </summary>
    public void LoadMainMenu()
    {
        FindObjectOfType<DisplayDistance>().SaveCurrentScore();
        SceneManager.LoadScene("Menu");
    }
}
