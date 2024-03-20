using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBar : MonoBehaviour
{
    [SerializeField] GameObject bg;
    [SerializeField] ShopType type;
    public ShopType Type => type;

    UIShop shop;

    public void SetShop(UIShop shop)
    {
        this.shop = shop;
    }

    public void Select()
    {
        SoundManager.Ins.Play(SoundType.Click, ref SoundManager.Ins.audioSource);
        shop.SelectBar(this);
    }

    public void Active(bool active)
    {
        bg.SetActive(!active);
    }
}
