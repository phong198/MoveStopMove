using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPageHats : MonoBehaviour, IChangePage
{
    public void ShowPage(ShopUIManager target)
    {
        target.ShowHatShop();
    }

    public void HidePage(ShopUIManager target)
    {
        target.HideHatShop();
    }
}
