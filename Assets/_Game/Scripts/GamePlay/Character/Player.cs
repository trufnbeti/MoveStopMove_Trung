using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	[SerializeField] private float moveSpeed;
	private CounterTime counter = new CounterTime();
	private bool IsCanUpdate => GameManager.Ins.IsState(GameState.Gameplay);
	private bool isMoving = false;
	
	[SerializeField]private SkinType skinType = SkinType.Normal;
	private WeaponType weaponType = WeaponType.Candy_1;
	private HatType hatType = HatType.Cap;
	private AccessoryType accessoryType = AccessoryType.Headphone;
	private PantType pantType = PantType.Pant_1;

	public override void OnInit() {
		OnTakeClothsData();
		base.OnInit();
		TF.rotation = Quaternion.Euler(Vector3.up * 180);
		indicator.SetName("YOU");
	}
	
	public void OnTakeClothsData() {
		skinType = DataManager.Ins.GetEnumData<SkinType>(DataManager.Ins.IdSkin);
		weaponType = DataManager.Ins.GetEnumData<WeaponType>(DataManager.Ins.IdWeapon);
		hatType = DataManager.Ins.GetEnumData<HatType>(DataManager.Ins.IdHat);
		accessoryType = DataManager.Ins.GetEnumData<AccessoryType>(DataManager.Ins.IdAccessory);
		pantType = DataManager.Ins.GetEnumData<PantType>(DataManager.Ins.IdPant);
	}

	#region Skin
	public override void WearClothes() {
		base.WearClothes(); 

		ChangeSkin(skinType);
		ChangeWeapon(weaponType);
		ChangeHat(hatType);
		ChangeAccessory(accessoryType);
		ChangePant(pantType);
	}

	public override void RemoveTarget(Character target) {
		base.RemoveTarget(target);
		target.SetMask(false);
	}

	public void TryCloth(ShopType shopType, Enum type) {
		switch (shopType) {
			case ShopType.Hat:
				currentSkin.DespawnHat();
				ChangeHat((HatType)type);
				break;
			case ShopType.Weapon:
				currentSkin.DespawnWeapon();
				ChangeWeapon((WeaponType)type);
				break;
			case ShopType.Skin:
				TakeOffClothes();
				skinType = (SkinType)type;
				WearClothes();
				break;
			case ShopType.Accessory:
				currentSkin.DespawnAccessory();
				ChangeAccessory((AccessoryType)type);
				break;
			case ShopType.Pant:
				ChangePant((PantType)type);
				break;
		}
	}
	#endregion

	public override void OnAttack() {
		base.OnAttack();
		if (target != null && currentSkin.Weapon.IsCanAttack) {
			counter.Start(Throw, Constant.TIME_DELAY_THROW);
			ResetAnim();
		}
	}

	public override void AddTarget(Character target) {
		base.AddTarget(target);
		if (!target.IsDead && !IsDead) {
			target.SetMask(true);
			if (counter.IsRunning && !isMoving) { //dang dung im thi co thang di vao
				OnAttack();
			}
		}
	}

	protected override void SetSize(float size) {
		base.SetSize(size);
		CameraFollow.Ins.SetRateOffset((this.size - Constant.MIN_SIZE) / (Constant.MAX_SIZE - Constant.MIN_SIZE));
	}

	private void Update() {
		if (IsCanUpdate && !IsDead) {
			if (Input.GetMouseButtonDown(0)) {
				counter.Cancel();
			}

			if (Input.GetMouseButton(0) && Vector3.Distance(JoystickControl.direct, Vector3.zero) > 0.1f) {
				TF.position += JoystickControl.direct * moveSpeed * Time.deltaTime;
				TF.forward = JoystickControl.direct;
				ChangeAnim(Anim.run.ToString());
				isMoving = true;
			} else {
				counter.Execute();
			}

			if (Input.GetMouseButtonUp(0)) {
				isMoving = false;
				OnStopMove();
				OnAttack();
			}
		}
	}
}