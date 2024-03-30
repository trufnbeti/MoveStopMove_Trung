using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
	[Header("References")]

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

	public void ChangeWeapon(int index) {
		currentWeapon = Instantiate(SkinManager.Ins.weaponData.GetWeapon(index), rightHand);
	}

	public void ChangeAccessory(int index) {
		if (isCanChange && index != 0) {
			currentAccessory = Instantiate(SkinManager.Ins.accessoryData.GetPrefab(index), leftHand);
		}
	}

	public void ChangeHat(int index) {
		if (isCanChange && index != 0) {
			currentHat = Instantiate(SkinManager.Ins.hatData.GetPrefab(index), head);
		}
	}

	public void ChangePant(int index) {
		pant.material = SkinManager.Ins.pantData.GetPrefab(index);
	}

	public void OnDespawn() {
		DespawnWeapon();
		DespawnAccessory();
		DespawnHat();
	}

	public void DespawnHat() {
		if (currentHat) {
			Destroy(currentHat.gameObject);
		}
	}
	public void DespawnAccessory() {
		if (currentAccessory) {
			Destroy(currentAccessory.gameObject);
		}
	}

	public void DespawnWeapon() {
		if (currentWeapon) {
			Destroy(currentWeapon.gameObject);
		}
	}
}