using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPageSkins : MonoBehaviour, IChangePage
{
    public void ShowPage(ShopUIManager target)
    {
        target.ShowSkinShop();
    }

    public void HidePage(ShopUIManager target)
    {
        target.HideSkinShop();
    }
}
