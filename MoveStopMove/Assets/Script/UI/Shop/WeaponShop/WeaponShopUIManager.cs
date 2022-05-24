using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponShopUIManager : ShopUIManager
{
    [SerializeField]
    private GameObject HammerCatalouge;
    [SerializeField]
    private GameObject BoomerangCatalouge;
    [SerializeField]
    private GameObject KnifeCatalouge;
    [SerializeField]
    private TMP_Text weaponName;
    [SerializeField]
    private TMP_Text weaponEffect;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField] private Player player;

    private void OnEnable()
    {
        ChangePage(new ShowPageHammer());
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
    }
    public override void HideKnifeShop()
    {
        KnifeCatalouge.SetActive(false);
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
