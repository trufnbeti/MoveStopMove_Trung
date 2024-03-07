using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : GameUnit
{

	[SerializeField] private PantData pantData;

	[SerializeField] private Transform head;
	[SerializeField] private Transform rightHand;
	[SerializeField] private Transform leftHand;
	[SerializeField] private Renderer pant;

	[SerializeField] private bool isCanChange = false;

	private Weapon currentWeapon;
	private Accessory currentAccessory;
	private Hat currentHat;

	[SerializeField] private Animator anim;
	public Animator Anim => anim;

	public Weapon Weapon => currentWeapon;

	public void ChangeWeapon(WeaponType weaponType) {
		currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, rightHand);
	}

	public void ChangeAccessory(AccessoryType accessoryType) {
		if (isCanChange && accessoryType != AccessoryType.None) {
			currentAccessory = SimplePool.Spawn<Accessory>((PoolType)accessoryType, leftHand);
		}
	}

	public void ChangeHat(HatType hatType) {
		if (isCanChange && hatType != HatType.None) {
			currentHat = SimplePool.Spawn<Hat>((PoolType)hatType, head);
		}
	}

	public void ChangePant(PantType pantType) {
		pant.material = pantData.GetPant(pantType);
	}

	public void OnDespawn() {
		SimplePool.Despawn(currentWeapon);
		if (currentAccessory) SimplePool.Despawn(currentAccessory);
		if (currentHat) SimplePool.Despawn(currentHat);
	}

	public void DespawnHat() {
		if (currentHat) SimplePool.Despawn(currentHat);
	}
	public void DespawnAccessory() {
		if (currentAccessory) SimplePool.Despawn(currentAccessory);
	}

	internal void DespawnWeapon() {
		if (currentWeapon) SimplePool.Despawn(currentWeapon);
	}
}