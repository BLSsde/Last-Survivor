using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Singleton: Shop
    public static Shop Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [System.Serializable]public class ShopItem
    {
        public Sprite image;
        public int PriceInCoins;
        public bool isPurchased = false;
    }

    public List<ShopItem> shopItemList; // accesing in prifile script

    [SerializeField] private Animator noEnoughCD; //animator

    [SerializeField] private GameObject itemTemplate;
    GameObject g;
    [SerializeField] Transform shopScrollView;
    private Button buyBtn;

    [SerializeField] private GameObject Shop_Panel;

    void Start()
    {
        int len = shopItemList.Count;

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(itemTemplate, shopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = shopItemList[i].image;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = shopItemList[i].PriceInCoins.ToString();
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            if(shopItemList[i].isPurchased)
            {
                DisableBuyBtn();
            }
            
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);
        }

    }

    private void OnShopItemBtnClicked(int itemIndex)
    {
        if (ShopManager.Instance.HasEnoughCoins(shopItemList[itemIndex].PriceInCoins))
        {
            ShopManager.Instance.UseCoins(shopItemList[itemIndex].PriceInCoins);
            // purchase item
            shopItemList[itemIndex].isPurchased = true;

            // disable the button
            buyBtn = shopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            DisableBuyBtn();

            //Change UI text:coins
            ShopManager.Instance.UpdateAllCD_UIText();

            // add avatar
            Profile.Instance.AddAitems(shopItemList[itemIndex].image);
        }
        else
        {
            // Animator called
            noEnoughCD.SetTrigger("NoEnoughCD");
            
        }
        
    }

    private void DisableBuyBtn()
    {
        buyBtn.interactable = false;
        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
    }
   

    // Open & Close Shop
    public void OpenShop()
    {
        Shop_Panel.SetActive(true);
    }

    public void CloseShop()
    {
        Shop_Panel.SetActive(false);
    }
    
}
