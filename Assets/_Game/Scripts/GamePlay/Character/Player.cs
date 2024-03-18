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
	
	[SerializeField]private int skinType = 0;
	private int weaponType = 0;
	private int hatType = 0;
	private int accessoryType = 0;
	private int pantType = 0;
	
	#region Event

	private Action<object> actionLoadSkin;
	private Action<object> actionTrySkin;
	private Action<object> actionRevive;

	#endregion
	
	private void OnEnable() {
		actionLoadSkin = (param) => {
			TakeOffClothes();
			OnTakeClothsData();
			WearClothes();
		};
		actionTrySkin = (param) => TryCloth((TrySkin)param);
		actionRevive = (param) => OnRevive();

	}

	public override void OnInit() {
		this.RegisterListener(EventID.LoadSkin, actionLoadSkin);
		this.RegisterListener(EventID.TrySkin, actionTrySkin);
		this.RegisterListener(EventID.Revive, actionRevive);
		
		OnTakeClothsData();
		base.OnInit();
		TF.rotation = Quaternion.Euler(Vector3.up * 180);
		indicator.SetName("YOU");
	}

	public override void OnDeath() {
		this.RemoveListener(EventID.LoadSkin, actionLoadSkin);
		this.RemoveListener(EventID.TrySkin, actionTrySkin);
		this.RemoveListener(EventID.Revive, actionRevive);

		base.OnDeath();
		counter.Cancel();
	}

	private void OnTakeClothsData() {
		skinType = DataManager.Ins.IdSkin;
		weaponType = DataManager.Ins.IdWeapon;
		hatType = DataManager.Ins.IdHat;
		accessoryType = DataManager.Ins.IdAccessory;
		pantType = DataManager.Ins.IdPant;
	}

	#region Skin
	protected override void WearClothes() {
		base.WearClothes(); 

		ChangeSkin(skinType);
		ChangeWeapon(weaponType);
		ChangeHat(hatType);
		ChangeAccessory(accessoryType);
		ChangePant(pantType);
	}
	
	private void TryCloth(TrySkin obj) {
		switch (obj.shopType) {
			case ShopType.Hat:
				currentSkin.DespawnHat();
				ChangeHat(obj.index);
				break;
			case ShopType.Weapon:
				currentSkin.DespawnWeapon();
				ChangeWeapon(obj.index);
				break;
			case ShopType.Skin:
				TakeOffClothes();
				skinType = obj.index;
				WearClothes();
				break;
			case ShopType.Accessory:
				currentSkin.DespawnAccessory();
				ChangeAccessory(obj.index);
				break;
			case ShopType.Pant:
				ChangePant(obj.index);
				break;
		}
	}
	#endregion
	
	public override void RemoveTarget(Character target) {
		base.RemoveTarget(target);
		target.SetMask(false);
	}

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

	private void OnRevive() {
		ChangeAnim(Anim.idle.ToString());
		IsDead = false;
		ClearTarget();
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

public class TrySkin {
	public int index;
	public ShopType shopType;

	public TrySkin(int index, ShopType shopType) {
		this.index = index;
		this.shopType = shopType;
	}
}