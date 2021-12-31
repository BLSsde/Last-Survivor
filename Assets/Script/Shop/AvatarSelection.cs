using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour
{
    [Header("Navigation Button")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Play/Buy Button")]
    [SerializeField] private Button play;
    [SerializeField] private Button buy;
    [SerializeField] private Text priceText;

    [SerializeField] private int[]avatarPrices;
    private int currAvatar;

    [SerializeField] private MainMenuManager mainMenuManager;

    private void Start()
    {
        currAvatar = SaveGameData.instance.currentAvatar;
        SelectAvatar(currAvatar);
    }

    private void SelectAvatar(int _index)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(i == _index);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (SaveGameData.instance.avatarUnlocked[currAvatar])
        {
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);

        }
        else
        {
            play.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            priceText.text = avatarPrices[currAvatar].ToString();
 
        }
    }

    private void Update()
    {
        // Check if we have enough money
        if (buy.gameObject.activeInHierarchy)
        {
            buy.interactable = (SaveGameData.instance.coins >= avatarPrices[currAvatar]);
        }
    }

    public void ChangeAvatar(int _change)
    {
        currAvatar += _change;
        AudioManager.instance.PlaySound("ButtonPress");

        if (currAvatar > transform.childCount - 1)
            currAvatar = 0;
        else if (currAvatar < 0)
            currAvatar = transform.childCount - 1;

        SaveGameData.instance.currentAvatar = currAvatar;
        SaveGameData.instance.Save();
        SelectAvatar(currAvatar);
    }

    public void BuyAvatarBtn()
    {
        SaveGameData.instance.coins -= avatarPrices[currAvatar];
        SaveGameData.instance.avatarUnlocked[currAvatar] = true;
        SaveGameData.instance.Save();
        UpdateUI();
        AudioManager.instance.PlaySound("Click");

        //Update CoinsUI
        mainMenuManager.UpdateCoinsInShopMenu();

    }
    public void StartGame(int _index)
    {
        SceneManager.LoadScene(_index);
        AudioManager.instance.PlaySound("Click");
    }
}