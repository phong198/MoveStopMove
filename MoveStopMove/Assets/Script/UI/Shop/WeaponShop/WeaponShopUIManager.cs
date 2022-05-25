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
        if (currentPage is ShowPageKnife)
        {
            ChangePage(new ShowPageHammer());
        }
        else if (currentPage is ShowPageCandy)
        {
            ChangePage(new ShowPageKnife());
        }
    }

    public void ClickRightArrow()
    {
        if (currentPage is ShowPageHammer)
        {
            ChangePage(new ShowPageKnife());
        }
        else if (currentPage is ShowPageKnife)
        {
            ChangePage(new ShowPageCandy());
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

    public void BuyWeapon()
    {
        switch (currentPage)
        {
            case ShowPageKnife:

                break;
            case ShowPageCandy:

                break;
        }
    }    

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
