using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    public virtual void ShowHatShop() { }
    public virtual void HideHatShop() { }
    public virtual void ShowPantsShop() { }
    public virtual void HidePantsShop() { }
    public virtual void ShowShieldShop() { }
    public virtual void HideShieldShop() { }
    public virtual void ShowSkinShop() { }
    public virtual void HideSkinShop() { }
    public virtual void ShowHammerShop() { }
    public virtual void HideHammerShop() { }
    public virtual void ShowCandyShop() { }
    public virtual void HideCandyShop() { }
    public virtual void ShowKnifeShop() { }
    public virtual void HideKnifeShop() { }

    protected IChangePage currentPage;
    public virtual void ChangePage(IChangePage page)
    {
        if (currentPage == page)
            return;
        if (currentPage != null)
        {
            currentPage.HidePage(this);
        }

        currentPage = page;

        if (currentPage != null)
        {
            currentPage.ShowPage(this);
        }
    }
}
