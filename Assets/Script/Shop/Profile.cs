using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    #region Singleton: Profile
    public static Profile Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public class Aitem
    {
        public Sprite _image;
    }
    public List<Aitem> AitemList;
    [SerializeField] private GameObject AitemUITemplate;
    [SerializeField] private Transform AitemsScrollView; //content

    private GameObject g;
    private int newSelectedIndex, previousSelectedIndex;

    [SerializeField] private Color ActiveAitemColor;
    [SerializeField] private Color DefaultAitemColor;
    [SerializeField] private Image CurrentAvatarItem;

    private void Start()
    {
        GetAvailableAitems();
        newSelectedIndex = previousSelectedIndex = 0;
    }

    private void GetAvailableAitems()
    {
        for (int i = 0; i < Shop.Instance.shopItemList.Count; i++)
        {
            if(Shop.Instance.shopItemList[i].isPurchased)
            {
                //add all perchased item to Avater item list
                AddAitems(Shop.Instance.shopItemList[i].image);
            }
        }

        SelectAvatarItem(newSelectedIndex);
    }

    public void AddAitems(Sprite img)
    {
        if(AitemList == null)
        {
            AitemList = new List<Aitem>();

        }

        Aitem ait = new Aitem() { _image = img };
        //add ait to avatar item List
        AitemList.Add(ait);

        // add item in UI scroll view
        g = Instantiate(AitemUITemplate, AitemsScrollView);
        g.transform.GetChild(0).GetComponent<Image>().sprite = ait._image;  // Avater name in heirarchey

        // add click event
        g.transform.GetComponent<Button>().AddEventListener(AitemList.Count - 1, OnAvatarClick);
    }

    void OnAvatarClick(int ItemIndex)
    {
        SelectAvatarItem(ItemIndex);
    }

    private void SelectAvatarItem(int AiIndex)
    {
        previousSelectedIndex = newSelectedIndex;
        newSelectedIndex = AiIndex;

        AitemsScrollView.GetChild(previousSelectedIndex).GetComponent<Image>().color = DefaultAitemColor;
        AitemsScrollView.GetChild(newSelectedIndex).GetComponent<Image>().color = ActiveAitemColor;

        // change the avatar item
        CurrentAvatarItem.sprite = AitemList[newSelectedIndex]._image;

    }

}
