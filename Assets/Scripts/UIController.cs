using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI number;
    [SerializeField] private TextMeshProUGUI cheese;
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject victoryScreen;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        number.text = $"{health}";
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        number.text = $"{health}";
    }

    public void SetCheese(int cheeseAmount, int winCondition)
    {
        cheese.text = $"{cheeseAmount} / {winCondition}";
    }

    public void Defeat()
    {
        defeatScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Victory()
    {
        victoryScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void retryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
