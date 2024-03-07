using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	[SerializeField] private DynamicJoystick joystick;
	[SerializeField] private float moveSpeed;
	[SerializeField] private Rigidbody rb;
	private CounterTime counter = new CounterTime();
	private bool IsCanUpdate => GameManager.Ins.IsState(GameState.GamePlay);
	private bool isMoving = false;
	private float horizontal;
	private float vertical;
	private Vector3 direction;
	
	private SkinType skinType = SkinType.Normal;
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
		skinType = UserData.Ins.playerSkin;
		weaponType = UserData.Ins.playerWeapon;
		hatType = UserData.Ins.playerHat;
		accessoryType = UserData.Ins.playerAccessory;
		pantType = UserData.Ins.playerPant;
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

			direction = (Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal).normalized;
			if (Input.GetMouseButton(0) && Vector3.Distance(direction, Vector3.zero) > 0.1f) {
				TF.position += direction * moveSpeed * Time.deltaTime;
				TF.forward = direction;
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