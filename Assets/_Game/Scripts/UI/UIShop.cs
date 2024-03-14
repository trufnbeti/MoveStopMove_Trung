using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UICanvas
{
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

	public ShopType shopType => currentBar.Type;
	public ItemState GetState(Enum t) => UserData.Ins.GetEnumData(t.ToString(), ItemState.Buy);

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
		playerCoinTxt.text = UserData.Ins.coin.ToString();
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
				InitShopItems(shopData.hats.List, ref itemEquipped);
				break;
			case ShopType.Pant:
				InitShopItems(shopData.pants.List, ref itemEquipped);
				break;
			case ShopType.Accessory:
				InitShopItems(shopData.accessories.List, ref itemEquipped);
				break;
			case ShopType.Skin:
				InitShopItems(shopData.skins.List, ref itemEquipped);
				break;
		}
	}

	public void SelectItem(ShopItem item) {
		if (currentItem != null) {
			currentItem.SetState(GetState(currentItem.Type));
		}

		currentItem = item;

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
		
		LevelManager.Ins.player.TryCloth(shopType, currentItem.Type);
		currentItem.SetState(ItemState.Selecting);

		coinTxt.text = item.data.cost.ToString();
	}

	#region BtnClick

	public void OnBtnBuyClick() {
		if (UserData.Ins.coin >= currentItem.data.cost) {
			UserData.Ins.SetIntData(PrefKey.Coin.ToString(), ref UserData.Ins.coin, UserData.Ins.coin - currentItem.data.cost);
			UserData.Ins.SetEnumData(currentItem.Type.ToString(), ItemState.Bought);
			SelectItem(currentItem);
		}
	}

	public void OnBtnEquipClick() {
		if (currentItem != null) {
			UserData.Ins.SetEnumData(currentItem.Type.ToString(), ItemState.Equipped);

			switch (shopType) {
				case ShopType.Hat:
					UserData.Ins.SetEnumData(UserData.Ins.playerHat.ToString(), ItemState.Bought);
					UserData.Ins.SetEnumData(PrefKey.PlayerHat.ToString(), ref UserData.Ins.playerHat, (HatType)currentItem.Type);
					break;
				case ShopType.Pant:
					UserData.Ins.SetEnumData(UserData.Ins.playerPant.ToString(), ItemState.Bought);
					UserData.Ins.SetEnumData(PrefKey.PlayerPant.ToString(), ref UserData.Ins.playerPant, (PantType)currentItem.Type);
					break;
				case ShopType.Accessory:
					UserData.Ins.SetEnumData(UserData.Ins.playerAccessory.ToString(), ItemState.Bought);
					UserData.Ins.SetEnumData(PrefKey.PlayerAccessory.ToString(), ref UserData.Ins.playerAccessory, (AccessoryType)currentItem.Type);
					break;
				case ShopType.Skin:
					UserData.Ins.SetEnumData(UserData.Ins.playerSkin.ToString(), ItemState.Bought);
					UserData.Ins.SetEnumData(PrefKey.PlayerSkin.ToString(), ref UserData.Ins.playerSkin, (SkinType)currentItem.Type);
					break;
			}
		}

		if (itemEquipped != null) {
			itemEquipped.SetState(ItemState.Bought);
			itemEquipped = currentItem;
		}
		
		buttonState.SetState(StateButton.Equipped);
	}

	#endregion

	private void InitShopItems<T>(List<ShopItemData<T>> itemDatas, ref ShopItem itemEquipped) where T : Enum {
		for (int i = 0; i < itemDatas.Count; ++i) {
			ItemState itemState = UserData.Ins.GetEnumData(itemDatas[i].type.ToString(), ItemState.Buy);
			ShopItem item = miniPool.Spawn();
			item.SetData(itemDatas[i], this);
			item.SetState(itemState);
			
			if (itemState == ItemState.Equipped) {
				itemEquipped = item;
			}
		}
	}
}
