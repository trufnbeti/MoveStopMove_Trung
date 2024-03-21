using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float moveSpeed;
	private DynamicJoystick joystick;

	public DynamicJoystick Joystick {
		set {
			joystick = value;
		}
	}
	private CounterTime counter = new CounterTime();
	private bool IsCanUpdate => GameManager.Ins.IsState(GameState.Gameplay);
	private bool isMoving = false;
	
	[SerializeField]private int skinType = 0;
	private int weaponType = 0;
	private int hatType = 0;
	private int accessoryType = 0;
	private int pantType = 0;
	
	private Character attacker;
	public Character Attacker => attacker;

	public int Coin => Score * 10;
	private int ranking;
	public int Ranking => ranking;

	private Vector3 direct;
	
	#region Event

	private Action<object> actionLoadSkin;
	private Action<object> actionTrySkin;

	#endregion
	
	private void OnEnable() {
		actionLoadSkin = (param) => {
			TakeOffClothes();
			OnTakeClothsData();
			WearClothes();
		};
		actionTrySkin = (param) => TryCloth((TrySkin)param);

	}

	public override void OnInit() {
		OnReset();
		
		OnTakeClothsData();
		base.OnInit();
		TF.rotation = Quaternion.Euler(Vector3.up * 180);
		SetSize(Constant.MIN_SIZE);
		Name = "YOU";
	}

	public override void OnHit(Character character) {
		if (!IsDead) {
			attacker = character;
			ranking = LevelManager.Ins.TotalCharater;
		}
		base.OnHit(character);
	}

	public override void OnDeath() {
		this.RemoveListener(EventID.LoadSkin, actionLoadSkin);
		this.RemoveListener(EventID.TrySkin, actionTrySkin);

		base.OnDeath();
		counter.Cancel();
	}

	public override void OnStopMove() {
		base.OnStopMove();
		rb.velocity = Vector3.zero;
	}

	#region Skin
	
	private void OnTakeClothsData() {
		skinType = DataManager.Ins.IdSkin;
		weaponType = DataManager.Ins.IdWeapon;
		hatType = DataManager.Ins.IdHat;
		accessoryType = DataManager.Ins.IdAccessory;
		pantType = DataManager.Ins.IdPant;
	}

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
		if (this.size > 1) {
			SoundManager.Ins.Play(SoundType.SizeUp, ref audioSource);
			ParticlePool.Play(ParticleType.SizeUp, TF.position, Quaternion.identity);
		}
		CameraFollow.Ins.SetRateOffset((this.size - Constant.MIN_SIZE) / (Constant.MAX_SIZE - Constant.MIN_SIZE));
	}

	public void OnRevive() {
		OnReset();
		ChangeAnim(Anim.idle.ToString());
		IsDead = false;
		ClearTarget();
	}

	private void OnReset() {
		this.RegisterListener(EventID.LoadSkin, actionLoadSkin);
		this.RegisterListener(EventID.TrySkin, actionTrySkin);
	}
	
	private void Update() {
		if (IsCanUpdate && !IsDead) {
			if (Input.GetMouseButtonDown(0)) {
				counter.Cancel();
			}

			direct = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
			
			if (Input.GetMouseButton(0) && Vector3.Distance(direct, Vector3.zero) > 0.1f) {
				// rb.MovePosition(rb.position + direct * moveSpeed);
				rb.velocity = direct * moveSpeed;
				// TF.position = rb.position;
				TF.forward = direct;
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