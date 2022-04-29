using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPagePants : MonoBehaviour, IChangePage
{
    public void ShowPage(ShopUIManager target)
    {
        target.ShowPantsShop();
    }

    public void HidePage(ShopUIManager target)
    {
        target.HidePantsShop();
    }
}
