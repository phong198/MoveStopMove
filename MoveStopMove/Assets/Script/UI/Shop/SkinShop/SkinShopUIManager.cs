using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinShopUIManager : ShopUIManager
{
    [SerializeField]
    private GameObject HatsScrollRect;
    [SerializeField]
    private GameObject PantsScrollRect;
    [SerializeField]
    private GameObject ShieldScrollRect;
    [SerializeField]
    private GameObject SkinScrollRect;

    [SerializeField]
    private Image ChangePageHatsImage;
    [SerializeField]
    private Image ChangePagePantsImage;
    [SerializeField]
    private Image ChangePageShieldImage;
    [SerializeField]
    private Image ChangePageSkinsImage;
    [SerializeField] private GameObject playerName;

    private Color imageHatColor;
    private Color imagePantColor;
    private Color imageShieldColor;
    private Color imageSkinColor;


    private void OnEnable()
    {
        playerName.SetActive(false);
        imageHatColor = ChangePageHatsImage.color;
        imagePantColor = ChangePagePantsImage.color;
        imageShieldColor = ChangePageShieldImage.color;
        imageSkinColor = ChangePageSkinsImage.color;

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
}
