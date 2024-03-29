using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
	[SerializeField] private GameObject[] stateObjects;
	[SerializeField] Color[] colorBg;
	[SerializeField] Image icon;
	[SerializeField] Image bgIcon;
	
	public ItemState state;

	public Enum itemType;
	public ShopItemData data;
	private UIShop shop;

	public void OnSelect() {
		SoundManager.Ins.Play(SoundType.Click, ref SoundManager.Ins.audioSource);
		shop.SelectItem(this);
	}
	
	public void SetData<T>(ShopItemData<T> itemData, UIShop shop) where T : Enum {
		itemType = itemData.type;
		data = itemData;
		this.shop = shop;
		icon.sprite = itemData.icon;
		bgIcon.color = colorBg[(int)shop.shopType];
	}
	
	public void SetState(ItemState state) {
		for (int i = 0; i < stateObjects.Length; ++i) {
			stateObjects[i].SetActive(false);
		}

		stateObjects[(int)state].SetActive(true);
		this.state = state;
	}

	public void Equip() {
		
	}

	public void Selecting() {
		
	}
}
