using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPageBoomerang : IChangePage
{
    public void ShowPage(ShopUIManager target)
    {
        target.ShowBoomerangShop();
    }

    public void HidePage(ShopUIManager target)
    {
        target.HideBoomerangShop();
    }
}
