using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPageHammer : IChangePage
{
    public void ShowPage(ShopUIManager target)
    {
        target.ShowHammerShop();
    }

    public void HidePage(ShopUIManager target)
    {
        target.HideHammerShop();
    }
}
