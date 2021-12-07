using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    #region Singleton: ShopManager
    public static ShopManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private Text[] allCoinsUIText;
    [SerializeField] private Text[] allDaimondUIText;

    public int coins;
    public int daimonds;

    private void Start()
    {
        UpdateAllCD_UIText();
    }
    public void UseCoins(int amount)
    {
        coins -= amount;
    }
    public void UseDaimond(int amount)
    {
        daimonds -= amount;
    }


    public bool HasEnoughCoins(int amount)
    {
        return (coins >= amount);
    }
    public bool HasEnoughDaimonds(int amount)
    {
        return (daimonds >= amount);
    }

    public void UpdateAllCD_UIText()
    {
        for (int i = 0; i < allCoinsUIText.Length; i++)
        {
            allCoinsUIText[i].text = coins.ToString();
            allDaimondUIText[i].text = daimonds.ToString();
        }
    }
}
