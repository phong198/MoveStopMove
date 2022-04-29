using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPageShield : MonoBehaviour, IChangePage
{
    public void ShowPage(ShopUIManager target)
    {
        target.ShowShieldShop();
    }

    public void HidePage(ShopUIManager target)
    {
        target.HideShieldShop();
    }
}
