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
	[SerializeField]private Vector3 direction;
	
	SkinType skinType = SkinType.Normal;
	WeaponType weaponType = WeaponType.Candy_1;
	HatType hatType = HatType.Cap;
	AccessoryType accessoryType = AccessoryType.Headphone;
	PantType pantType = PantType.Pant_1;

	public override void OnInit() {
		OnTakeClothsData();
		base.OnInit();
		TF.rotation = Quaternion.Euler(Vector3.up * 180);
		indicator.SetName("YOU");
	}
	
	public void OnTakeClothsData()
	{
		// take old cloth data
		skinType = UserData.Ins.playerSkin;
		weaponType = UserData.Ins.playerWeapon;
		hatType = UserData.Ins.playerHat;
		accessoryType = UserData.Ins.playerAccessory;
		pantType = UserData.Ins.playerPant;
	}
	
	public override void WearClothes()
	{
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