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

    private void Awake()
    {
        ChangePage(new ShowPageHammer());
    }

    public void ClickLeftArrow()
    {
        if (currentPage is ShowPageBoomerang)
        {
            ChangePage(new ShowPageHammer());
        }
        else if (currentPage is ShowPageKnife)
        {
            ChangePage(new ShowPageBoomerang());
        }
    }

    public void ClickRightArrow()
    {
        if (currentPage is ShowPageHammer)
        {
            ChangePage(new ShowPageBoomerang());
        }
        else if (currentPage is ShowPageBoomerang)
        {
            ChangePage(new ShowPageKnife());
        }
    }

    public override void ShowHammerShop()
    {
        HammerCatalouge.SetActive(true);
        weaponName.text = "HAMMER";
        weaponEffect.text = "+1 range";
    }
    public override void HideHammerShop()
    {
        HammerCatalouge.SetActive(false);
    }
    public override void ShowBoomerangShop()
    {
        BoomerangCatalouge.SetActive(true);
        weaponName.text = "BOOMERANG";
        weaponEffect.text = "+5 Range*2";
    }
    public override void HideBoomerangShop()
    {
        BoomerangCatalouge.SetActive(false);
    }
    public override void ShowKnifeShop()
    {
        KnifeCatalouge.SetActive(true);
        weaponName.text = "KNIFE";
        weaponEffect.text = "+5 Range*3";
    }
    public override void HideKnifeShop()
    {
        KnifeCatalouge.SetActive(false);
    }
}
