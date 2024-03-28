using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/ShopData", order = 1)]
public class ShopData : ScriptableObject {
	public ShopItemDatas<HatType> hats;
	public ShopItemDatas<PantType> pants;
	public ShopItemDatas<AccessoryType> accessories;
	public ShopItemDatas<SkinType> skins;
}

[Serializable]
public class ShopItemDatas<T> where T: System.Enum {
	[SerializeField] List<ShopItemData<T>> list;
	public List<ShopItemData<T>> List => list;

	public ShopItemData<T> GetHat(T t)
	{
		return list.Single(q => q.type.Equals(t));
	}
}

[System.Serializable]
public class ShopItemData <T> : ShopItemData where T : Enum {
	public T type;
}

public class ShopItemData {
	public Sprite icon;
	public int cost;
}