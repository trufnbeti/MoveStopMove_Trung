using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UICanvas {
	[SerializeField] private ShopData shopData;
	[SerializeField] private ShopItem shopItem;
	[SerializeField] private Transform content;
	[SerializeField] private ShopBar[] shopBars;
	[SerializeField] private Text playerCoinTxt;
	[SerializeField] private ButtonState buttonState;
	[SerializeField] private Text coinTxt;

	private MiniPool<ShopItem> miniPool = new MiniPool<ShopItem>();

	private ShopItem currentItem;
	private ShopBar currentBar;
	private ShopItem itemEquipped;
	private int currentIdx = 0;

	public ShopType shopType => currentBar.Type;

	private void Awake() {
		miniPool.OnInit(shopItem, 0, content);
		for (int i = 0; i < shopBars.Length; ++i) {
			shopBars[i].SetShop(this);
		}
	}

	public override void Open() {
		base.Open();
		SelectBar(shopBars[0]);
		CameraFollow.Ins.ChangeState(CameraState.Shop);

		currentItem = null;
		playerCoinTxt.text = DataManager.Ins.Coin.ToString();
	}

	public override void CloseDirectly() {
		base.CloseDirectly();
		UIManager.Ins.OpenUI<UIMainMenu>();
		
		LevelManager.Ins.player.TakeOffClothes();
		LevelManager.Ins.player.OnTakeClothsData();
		LevelManager.Ins.player.WearClothes();
	}

	public void SelectBar(ShopBar shopBar) {
		if (currentBar != null) {
			currentBar.Active(false);
			currentItem = null;
		}
		
		currentBar = shopBar;
		currentBar.Active(true);
		
		miniPool.Collect();
		itemEquipped = null;
		
		buttonState.DeactiveBtn();

		switch (currentBar.Type) {
			case ShopType.Hat:
				InitShopItems(shopData.hats.List);
				break;
			case ShopType.Pant:
				InitShopItems(shopData.pants.List);
				break;
			case ShopType.Accessory:
				InitShopItems(shopData.accessories.List);
				break;
			case ShopType.Skin:
				InitShopItems(shopData.skins.List);
				break;
		}
	}

	public void SelectItem(ShopItem item) {
		if (currentItem != null) {
			ItemState itemState = (ItemState)DataManager.Ins.GetStateData(currentIdx, shopType);
			currentItem.SetState(itemState);
		}

		currentItem = item;
		currentIdx = Convert.ToInt32(currentItem.itemType);

		switch (currentItem.state) {
			case ItemState.Buy:
				buttonState.SetState(StateButton.Buy);
				break;
			case ItemState.Bought:
				buttonState.SetState(StateButton.Equip);
				break;
			case ItemState.Equipped:
				buttonState.SetState(StateButton.Equipped);
				break;
		}
		
		LevelManager.Ins.player.TryCloth(shopType, currentIdx);
		currentItem.SetState(ItemState.Selecting);

		coinTxt.text = item.data.cost.ToString();
	}

	#region BtnClick

	public void OnBtnBuyClick() {
		if (DataManager.Ins.Coin >= currentItem.data.cost) {
			DataManager.Ins.Coin -= currentItem.data.cost;
			DataManager.Ins.SetStateData(currentIdx, 1, shopType);
			
			SelectItem(currentItem);

			playerCoinTxt.text = DataManager.Ins.Coin.ToString();
		}
	}

	public void OnBtnEquipClick() {
		if (currentItem != null) {
			int equippedIdx = itemEquipped ? Convert.ToInt32(itemEquipped.itemType) : 0;
			DataManager.Ins.SetStateData(currentIdx, 2, shopType);
			DataManager.Ins.SetStateData(equippedIdx, 1, shopType);
			
			switch (shopType) {
				case ShopType.Hat:
					DataManager.Ins.HatStatus[0] = 0;
					DataManager.Ins.IdHat = currentIdx;
					break;
				case ShopType.Pant:
					DataManager.Ins.IdPant = currentIdx;
					break;
				case ShopType.Accessory:
					DataManager.Ins.AccessoryStatus[0] = 0;
					DataManager.Ins.IdAccessory = currentIdx;
					break;
				case ShopType.Skin:
					DataManager.Ins.IdSkin = currentIdx;
					break;
			}
		}

		if (itemEquipped != null) {
			itemEquipped.SetState(ItemState.Bought);
		}
		
		itemEquipped = currentItem;
		buttonState.SetState(StateButton.Equipped);
	}

	#endregion

	private void InitShopItems<T>(List<ShopItemData<T>> itemDatas) where T : Enum {
		for (int i = 0; i < itemDatas.Count; ++i) {
			int id = Convert.ToInt32(itemDatas[i].type);
			ItemState itemState = (ItemState)DataManager.Ins.GetStateData(id, shopType);
			ShopItem item = miniPool.Spawn();
			item.SetData(itemDatas[i], this);
			item.SetState(itemState);
			
			if (itemState == ItemState.Equipped) {
				itemEquipped = item;
			}
		}
	}
}
