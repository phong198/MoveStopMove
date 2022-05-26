using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponShopUIManager : ShopUIManager
{
    public enum WeaponState { CantBuy, CanBuy, Select, Equipped }
    public int[] weaponPrice = { 800, 2000 };
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject equippedButton;

    [SerializeField] private TMP_Text priceText;
    [SerializeField] private GameObject HammerCatalouge;
    [SerializeField] private GameObject BoomerangCatalouge;
    [SerializeField] private GameObject KnifeCatalouge;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponEffect;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Player player;
    [SerializeField] private GameObject playerName;

    private void OnEnable()
    {
        ChangePage(new ShowPageHammer());
        playerName.SetActive(false);
        equipButton.SetActive(true);
    }

    private void OnDisable()
    {
        playerName.SetActive(true);
    }

    #region Change Page

    public void CloseWeaponShop()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ClickLeftArrow()
    {
        switch (currentPage)
        {
            case ShowPageKnife:
                ChangePage(new ShowPageHammer());
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                break;
            case ShowPageCandy:
                ChangePage(new ShowPageKnife());
                CheckKnifeBuyState();
                break;
        }
    }

    public void ClickRightArrow()
    {
        switch (currentPage)
        {
            case ShowPageHammer:
                ChangePage(new ShowPageKnife());
                CheckKnifeBuyState();
                break;
            case ShowPageKnife:
                ChangePage(new ShowPageCandy());
                CheckCandyBuyState();
                break;
        }    
    }

    public override void ShowHammerShop()
    {
        HammerCatalouge.SetActive(true);
        weaponName.text = "HAMMER";
        weaponEffect.text = "+3 Damage";
    }
    public override void HideHammerShop()
    {
        HammerCatalouge.SetActive(false);
    }
    public override void ShowCandyShop()
    {
        BoomerangCatalouge.SetActive(true);
        weaponName.text = "CANDY";
        weaponEffect.text = "+2 Damage, Burns Enemies";
        priceText.text = weaponPrice[1].ToString();
    }
    public override void HideCandyShop()
    {
        BoomerangCatalouge.SetActive(false);
    }
    public override void ShowKnifeShop()
    {
        KnifeCatalouge.SetActive(true);
        weaponName.text = "KNIFE";
        weaponEffect.text = "+1 Damage, x2 Bullet speed";
        priceText.text = weaponPrice[0].ToString();
    }
    public override void HideKnifeShop()
    {
        KnifeCatalouge.SetActive(false);
    }

    #endregion

    #region Buy Weapon
    //Start Buy Weapon Region
    public void BuyWeapon()
    {
        switch (currentPage)
        {
            case ShowPageKnife:
                if (GameFlowManager.Instance.totalPlayerGold >= weaponPrice[0])
                {
                    GameFlowManager.Instance.totalPlayerGold -= weaponPrice[0];
                    PlayerPrefs.SetInt("totalPlayerGold", GameFlowManager.Instance.totalPlayerGold);
                    PlayerPrefs.SetInt("knifeBuyState", 1);
                    PlayerPrefs.Save();
                    CheckKnifeBuyState();
                }
                break;
            case ShowPageCandy:
                if (GameFlowManager.Instance.totalPlayerGold >= weaponPrice[1])
                {
                    GameFlowManager.Instance.totalPlayerGold -= weaponPrice[1];
                    PlayerPrefs.SetInt("totalPlayerGold", GameFlowManager.Instance.totalPlayerGold);
                    PlayerPrefs.SetInt("candyBuyState", 1);
                    PlayerPrefs.Save();
                    CheckCandyBuyState();
                }
                break;
        }
    }

    private void CheckKnifeBuyState()
    {
        int hammerBuyState = PlayerPrefs.GetInt("knifeBuyState", 0);
        switch (hammerBuyState)
        {
            case 0:
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                break;
            case 1:
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                break;
        }
    }

    private void CheckCandyBuyState()
    {
        int hammerBuyState = PlayerPrefs.GetInt("candyBuyState", 0);
        switch (hammerBuyState)
        {
            case 0:
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                break;
            case 1:
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                break;
        }
    }
    //End Buy Weapon Region
    #endregion

    #region Change Weapon
    public void ChangeWeapon()
    {
        switch (currentPage)
        {
            case ShowPageHammer:
                player.equipedWeapon = Character.Weapon.Hammer;
                player.EquipWeapon();
                PlayerPrefs.SetInt("equipedWeapon", (int)Character.Weapon.Hammer);
                PlayerPrefs.Save();
                break;
            case ShowPageKnife:
                player.equipedWeapon = Character.Weapon.Knife;
                player.EquipWeapon();
                PlayerPrefs.SetInt("equipedWeapon", (int)Character.Weapon.Knife);
                PlayerPrefs.Save();
                break;
            case ShowPageCandy:
                player.equipedWeapon = Character.Weapon.Candy;
                player.EquipWeapon();
                PlayerPrefs.SetInt("equipedWeapon", (int)Character.Weapon.Candy);
                PlayerPrefs.Save();
                break;
        }
    }
    #endregion
}
