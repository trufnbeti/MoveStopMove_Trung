using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "CharacterSkin/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
	[SerializeField] private List<WeaponItem> weaponItems;
	
	public Weapon GetWeapon(int index) {
		return GetWeaponItem(index).weapon;
	}

	public WeaponItem GetWeaponItem(int index) {
		return weaponItems[index];
	}

	public int NextWeaponIdx(int index) {
		return index == weaponItems.Count - 1 ? 0 : index + 1;
	}
	
	public int PrevWeaponIdx(int index) {
		return index == 0 ? weaponItems.Count - 1 : index - 1;
	}
}

[System.Serializable]
public class WeaponItem {
	public string name;
	public Weapon weapon;
	public int cost;
}
