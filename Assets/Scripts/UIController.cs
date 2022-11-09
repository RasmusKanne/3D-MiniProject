using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Creates fields in Unity to input all the UI elements used in the script
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI number;
    [SerializeField] private TextMeshProUGUI cheese;
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject IntroScreen;

    public void SetMaxHealth(int health) // Initially sets the maxhealth on the health bar
    {
        slider.maxValue = health;
        slider.value = health;
        number.text = $"{health}";
    }

    public void SetHealth(int health) // Used to sync the current health to the healthbar UI
    {
        slider.value = health;
        number.text = $"{health}";
    }

    public void SetCheese(int cheeseAmount, int winCondition) // Updates the cheese counter in the UI
    {
        cheese.text = $"{cheeseAmount} / {winCondition}";
    }

    public void Defeat() // Is called when the player loses, which stops the game and show an overlay with retry and exit buttons
    {
        defeatScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Victory() // Does the same as Defeat just with a victory message
    {
        victoryScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void retryButton() // Is used when the player presses a button on the end screens which reload the same scene starting the game over again
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton() // Exits the game when called
    {
        Application.Quit();
    }

    public void HideIntro() // Hides the intro text when called
    {
        IntroScreen.SetActive(false);
    }

}
