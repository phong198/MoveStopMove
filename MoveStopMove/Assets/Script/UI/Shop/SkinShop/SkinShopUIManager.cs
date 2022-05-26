using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinShopUIManager : ShopUIManager
{
    [SerializeField] private GameObject HatsScrollRect;
    [SerializeField] private GameObject PantsScrollRect;
    [SerializeField] private GameObject ShieldScrollRect;
    [SerializeField] private GameObject SkinScrollRect;

    [SerializeField] private Image ChangePageHatsImage;
    [SerializeField] private Image ChangePagePantsImage;
    [SerializeField] private Image ChangePageShieldImage;
    [SerializeField] private Image ChangePageSkinsImage;
    [SerializeField] private GameObject playerName;
    [SerializeField] private Player player;

    private Color imageHatColor;
    private Color imagePantColor;
    private Color imageShieldColor;
    private Color imageSkinColor;

    public Button[] skinButtons;
    private int skinButtonIndex;
    public Outline[] skinButtonOutlines;
    private Outline lastOutline;
    public GameObject[] lockImages;

    private int[] price = { 500, 2500, 5000 };
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private TMP_Text equipText;
    [SerializeField] private GameObject equipButton;

    private void OnEnable()
    {
        playerName.SetActive(false);
        imageHatColor = ChangePageHatsImage.color;
        imagePantColor = ChangePagePantsImage.color;
        imageShieldColor = ChangePageShieldImage.color;
        imageSkinColor = ChangePageSkinsImage.color;

        foreach(Button button in skinButtons)
        {
            button.onClick.AddListener(delegate { OnSkinButtonClick(System.Array.IndexOf(skinButtons, button), button); });
        }

        foreach(GameObject lockImage in lockImages)
        {
            int state = PlayerPrefs.GetInt("clothesState" + System.Array.IndexOf(lockImages, lockImage));
            if (state != 0)
            {
                lockImage.SetActive(false);
            }    
                
        }    

        ChangePage(new ShowPageHats());
    }
    private void OnDisable()
    {
        playerName.SetActive(true);
    }

    public void CloseSkinShop()
    {
        gameObject.SetActive(false);
    }

    #region Change Page
    public void ChangePageHats()
    {
        ChangePage(new ShowPageHats());
    }
    public void ChangePagePants()
    {
        ChangePage(new ShowPagePants());
    }
    public void ChangePageShields()
    {
        ChangePage(new ShowPageShield());
    }
    public void ChangePageSkins()
    {
        ChangePage(new ShowPageSkins());
    }

    public override void ShowHatShop()
    {
        imageHatColor.a = 0;
        ChangePageHatsImage.color = imageHatColor;
        HatsScrollRect.SetActive(true);
    }

    public override void HideHatShop()
    {
        imageHatColor.a = 1;
        ChangePageHatsImage.color = imageHatColor;
        HatsScrollRect.SetActive(false);
    }

    public override void ShowPantsShop()
    {
        imagePantColor.a = 0;
        ChangePagePantsImage.color = imagePantColor;
        PantsScrollRect.SetActive(true);
    }

    public override void HidePantsShop()
    {
        imagePantColor.a = 1;
        ChangePagePantsImage.color = imagePantColor;
        PantsScrollRect.SetActive(false);
    }

    public override void ShowShieldShop()
    {
        imageShieldColor.a = 0;
        ChangePageShieldImage.color = imageShieldColor;
        ShieldScrollRect.SetActive(true);
    }

    public override void HideShieldShop()
    {
        imageShieldColor.a = 1;
        ChangePageShieldImage.color = imageShieldColor;
        ShieldScrollRect.SetActive(false);
    }

    public override void ShowSkinShop()
    {
        imageSkinColor.a = 0;
        ChangePageSkinsImage.color = imageSkinColor;
        SkinScrollRect.SetActive(true);
    }

    public override void HideSkinShop()
    {
        imageSkinColor.a = 1;
        ChangePageSkinsImage.color = imageSkinColor;
        SkinScrollRect.SetActive(false);
    }
    #endregion

    private void OnSkinButtonClick(int buttonIndex, Button button)
    {
        CheckClothesState(buttonIndex);
        skinButtonIndex = buttonIndex;
        if (lastOutline != null && lastOutline != skinButtonOutlines[buttonIndex])
        {
            lastOutline.enabled = false;
        }
        skinButtonOutlines[buttonIndex].enabled = true;
        lastOutline = skinButtonOutlines[buttonIndex];
    }

    #region Change Clothes
    public void OnEquipSkinButtonClick()
    {
        ChangeClothes(skinButtonIndex);
        PlayerPrefs.SetInt("clothesState" + skinButtonIndex, 2);
        PlayerPrefs.Save();
        CheckClothesState(skinButtonIndex);
    }

    private void ChangeClothes(int skinButtonIndex)
    {
        player.ChangeClothes((Character.Clothes)skinButtonIndex);
        PlayerPrefs.SetInt("clothes", skinButtonIndex);
    }
    #endregion

    #region Buy Clothes
    public void OnBuyClothesButtonClick()
    {
        BuyClothes(skinButtonIndex);
    }

    private void BuyClothes(int skinButtonIndex)
    {
        if (skinButtonIndex <= 20)
        {
            if (GameFlowManager.Instance.totalPlayerGold >= price[0])
            {
                PlayerPrefs.SetInt("clothesState" + skinButtonIndex, 1);
                GameFlowManager.Instance.totalPlayerGold -= price[0];
                PlayerPrefs.SetInt("totalPlayerGold", GameFlowManager.Instance.totalPlayerGold);
                PlayerPrefs.Save();
                CheckClothesState(skinButtonIndex);
            }    
        }
        else if (skinButtonIndex >= 21 && skinButtonIndex <= 23)
        {
            if (GameFlowManager.Instance.totalPlayerGold >= price[1])
            {
                PlayerPrefs.SetInt("clothesState" + skinButtonIndex, 1);
                GameFlowManager.Instance.totalPlayerGold -= price[1];
                PlayerPrefs.SetInt("totalPlayerGold", GameFlowManager.Instance.totalPlayerGold);
                PlayerPrefs.Save();
                CheckClothesState(skinButtonIndex);
            }    
        }    
        else if (skinButtonIndex > 23)
        {
            if (GameFlowManager.Instance.totalPlayerGold >= price[2])
            {
                PlayerPrefs.SetInt("clothesState" + skinButtonIndex, 1);
                GameFlowManager.Instance.totalPlayerGold -= price[2];
                PlayerPrefs.SetInt("totalPlayerGold", GameFlowManager.Instance.totalPlayerGold);
                PlayerPrefs.Save();
                CheckClothesState(skinButtonIndex);
            }
        }    
    }

    private void CheckClothesState(int skinButtonIndex)
    {
        int state = PlayerPrefs.GetInt("clothesState" + skinButtonIndex, 0);
        switch (state)
        {
            case 0: //chưa mua
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                if (skinButtonIndex <= 20)
                {
                    priceText.SetText(price[0].ToString());
                }   
                else if (skinButtonIndex >= 21 && skinButtonIndex <= 23)
                {
                    priceText.SetText(price[1].ToString());
                }    
                else if (skinButtonIndex > 23)
                {
                    priceText.SetText(price[2].ToString());
                }    
                break;
            case 1: //đã mua, chưa equip
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                equipText.SetText("Select");
                lockImages[skinButtonIndex].SetActive(false);
                break;
            case 2://đã equip
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                lockImages[skinButtonIndex].SetActive(false);
                equipText.SetText("Equipped");
                break;
        }
    }
    #endregion
}
