using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Reference")]
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject informationMenu;

    //Info Menu
    [Header("Information Menu")]
    [SerializeField] private Text TotalCoinsInfoM;
    [SerializeField] private Text TotalFinishesInfoM;
    [SerializeField] private Text CurrentLevelInfoM;
    //Shop menu
    [Header("Shop Menu")]
    [SerializeField] private Text TotalCoinsShopM;

    private void Start()
    {
        AudioManager.instance.PlaySound("MainMenu");
        AudioManager.instance.SoundPause("GameplayMusic");
    }
    public void StartGame(int _index)
    {
        SceneManager.LoadScene(_index);
        AudioManager.instance.PlaySound("Click");
    }
    public void QuiteGame()
    {
        Application.Quit();
    }

    public void InformationBtn()
    {
        shopMenu.SetActive(false);
        informationMenu.SetActive(true);

        TotalCoinsInfoM.text = "Total Coins: " + SaveGameData.instance.coins.ToString();
        TotalFinishesInfoM.text = "Total Finishes: " + SaveGameData.instance.finishes.ToString();
        CurrentLevelInfoM.text = "Current Level: " + SaveGameData.instance.currentLevel.ToString();

    }
    public void CloseInfoMenuBtn()
    {
        informationMenu.SetActive(false);
    }

    public void ShopMenuBtn()
    {
        informationMenu.SetActive(false);
        shopMenu.SetActive(true);

        UpdateCoinsInShopMenu();
    }
    public void CloseShopMenuBtn()
    {
        shopMenu.SetActive(false);
    }
    public void UpdateCoinsInShopMenu()
    {
        TotalCoinsShopM.text = SaveGameData.instance.coins.ToString();
    }
}
