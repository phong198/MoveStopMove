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
    [SerializeField] private TMP_Text equippedText;

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
    //Start Change Page Region
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
                CheckHammerState();
                equipButton.SetActive(true);
                break;
            case ShowPageCandy:
                ChangePage(new ShowPageKnife());
                CheckKnifeState();
                break;
        }
    }

    public void ClickRightArrow()
    {
        switch (currentPage)
        {
            case ShowPageHammer:
                ChangePage(new ShowPageKnife());
                CheckKnifeState();
                break;
            case ShowPageKnife:
                ChangePage(new ShowPageCandy());
                CheckCandyState();
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
    //End Change Page Region
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
                    CheckKnifeState();
                }
                break;
            case ShowPageCandy:
                if (GameFlowManager.Instance.totalPlayerGold >= weaponPrice[1])
                {
                    GameFlowManager.Instance.totalPlayerGold -= weaponPrice[1];
                    PlayerPrefs.SetInt("totalPlayerGold", GameFlowManager.Instance.totalPlayerGold);
                    PlayerPrefs.SetInt("candyBuyState", 1);
                    PlayerPrefs.Save();
                    CheckCandyState();
                }
                break;
        }
    }

    private void CheckHammerState()
    {
        int equipedWeapon = PlayerPrefs.GetInt("equipedWeapon", 0);
        if (equipedWeapon == (int)Character.Weapon.Hammer)
        {
            equippedText.SetText("Equipped");
        }
        else equippedText.SetText("Select");
    }

    private void CheckKnifeState()
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
                int equipedWeapon = PlayerPrefs.GetInt("equipedWeapon", 0);
                if (equipedWeapon == (int)Character.Weapon.Knife)
                {
                    equippedText.SetText("Equipped");
                }
                else equippedText.SetText("Select");
                break;
        }
    }

    private void CheckCandyState()
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
                int equipedWeapon = PlayerPrefs.GetInt("equipedWeapon", 0);
                if (equipedWeapon == (int)Character.Weapon.Candy)
                {
                    equippedText.SetText("Equipped");
                }
                else equippedText.SetText("Select");
                break;
        }
    }
    //End Buy Weapon Region
    #endregion

    #region Change Weapon
    //Start Change Weapon Region
    public void ChangeWeapon()
    {
        switch (currentPage)
        {
            case ShowPageHammer:
                player.equipedWeapon = Character.Weapon.Hammer;
                player.EquipWeapon();
                PlayerPrefs.SetInt("equipedWeapon", (int)Character.Weapon.Hammer);
                PlayerPrefs.Save();
                CheckHammerState();
                break;
            case ShowPageKnife:
                player.equipedWeapon = Character.Weapon.Knife;
                player.EquipWeapon();
                PlayerPrefs.SetInt("equipedWeapon", (int)Character.Weapon.Knife);
                PlayerPrefs.Save();
                CheckKnifeState();
                break;
            case ShowPageCandy:
                player.equipedWeapon = Character.Weapon.Candy;
                player.EquipWeapon();
                PlayerPrefs.SetInt("equipedWeapon", (int)Character.Weapon.Candy);
                PlayerPrefs.Save();
                CheckCandyState();
                break;
        }
    }
    //End Change Weapon Region
    #endregion
}
