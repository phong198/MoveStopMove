using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPageKnife : IChangePage
{
    public void ShowPage(ShopUIManager target)
    {
        target.ShowKnifeShop();
    }

    public void HidePage(ShopUIManager target)
    {
        target.HideKnifeShop();
    }
}
