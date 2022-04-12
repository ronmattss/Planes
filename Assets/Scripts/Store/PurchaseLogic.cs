using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PurchaseType
{
    RemoveAds,Coin100
}
public class PurchaseLogic : MonoBehaviour
{

    public PurchaseType type;
    public void ClickPurchaseButton()
    {
        switch (type)
        {
            case PurchaseType.RemoveAds:
                IAPManager.instance.BuyRemoveAds();
                break;
            case PurchaseType.Coin100:
                IAPManager.instance.BuyCoin100();
                break;
        }
    }
}
