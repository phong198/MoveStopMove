using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPageCandy : IChangePage
{
    public void ShowPage(ShopUIManager target)
    {
        target.ShowCandyShop();
    }

    public void HidePage(ShopUIManager target)
    {
        target.HideCandyShop();
    }
}
