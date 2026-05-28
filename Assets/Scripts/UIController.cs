using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    public TextMeshProUGUI BalanceDisplay;

    public GameObject PauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //BalanceDisplay = GetComponent<TextMeshProUGUI>();
        Bank.MoneyChanged.AddListener(UpdateBalance);
    }

    void UpdateBalance(int newBalance) {
        BalanceDisplay.text = newBalance.ToString();
    }

    public void Pause() {
        // TODO: actually pause the gameplay
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void Unpause() {
        // TODO: actually unpause the gameplay
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    public void ToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void DebugLog(string buttonName) {
        Debug.Log(buttonName + " has been pressed");
    }
}
