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

	[SerializeField]private ShopItem currentItem;
	private ShopBar currentBar;
	[SerializeField]private ShopItem itemEquipped;
	private int currentIdx = 0;

	public ShopType shopType => currentBar.Type;
	// public ItemState GetState(Enum t) => (ItemState)DataManager.Ins.GetStateData<>(t);

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
			ItemState itemState = (ItemState)DataManager.Ins.GetStateData(currentIdx, shopType.GetType()); //TODO truyền shopType vào xong so sánh với từng phần tử trong shopType
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
			switch (shopType) {
				case ShopType.Hat:
					DataManager.Ins.HatStatus[currentItem.data.id] = 1;
					break;
				case ShopType.Accessory:
					DataManager.Ins.AccessoryStatus[currentItem.data.id] = 1;
					break;
				case ShopType.Pant:
					DataManager.Ins.PantStatus[currentItem.data.id] = 1;
					break;
				case ShopType.Skin:
					DataManager.Ins.SkinStatus[currentItem.data.id] = 1;
					break;
			}
			
			SelectItem(currentItem);

			playerCoinTxt.text = DataManager.Ins.Coin.ToString();
		}
	}

	public void OnBtnEquipClick() {
		if (currentItem != null) {
			UserData.Ins.SetEnumData(currentItem.itemType.ToString(), ItemState.Equipped);

			switch (shopType) {
				case ShopType.Hat:
					DataManager.Ins.HatStatus[DataManager.Ins.IdHat] = 1;
					DataManager.Ins.HatStatus[0] = 0;
					DataManager.Ins.HatStatus[currentItem.data.id] = 2;
					DataManager.Ins.IdHat = currentItem.data.id;
					break;
				case ShopType.Pant:
					DataManager.Ins.PantStatus[DataManager.Ins.IdPant] = 1;
					DataManager.Ins.PantStatus[currentItem.data.id] = 2;
					DataManager.Ins.IdPant = currentItem.data.id;
					break;
				case ShopType.Accessory:
					DataManager.Ins.AccessoryStatus[DataManager.Ins.IdAccessory] = 1;
					DataManager.Ins.AccessoryStatus[0] = 0;
					DataManager.Ins.AccessoryStatus[currentItem.data.id] = 2;
					DataManager.Ins.IdAccessory = currentItem.data.id;
					break;
				case ShopType.Skin:
					DataManager.Ins.SkinStatus[DataManager.Ins.IdSkin] = 1;
					DataManager.Ins.SkinStatus[currentItem.data.id] = 2;
					DataManager.Ins.IdSkin = currentItem.data.id;
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
			ItemState itemState = (ItemState)DataManager.Ins.GetStateData(id, shopType.GetType());
			ShopItem item = miniPool.Spawn();
			item.SetData(itemDatas[i], this);
			item.SetState(itemState);
			
			if (itemState == ItemState.Equipped) {
				itemEquipped = item;
			}
		}
	}
}
