using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Character : GameUnit {
	[SerializeField] Transform indicatorPoint;
	protected TargetIndicator indicator;
	
	[SerializeField] GameObject mask;
	
	private List<Character> targets = new List<Character>();
	protected Character target;
	
	private Vector3 targetPoint;

	private int score;

	protected float size = 1;

	public int Score => score;
	public float Size => size;
	public bool IsDead { get; protected set; }

	[SerializeField] protected Skin currentSkin;
	public bool IsCanAttack => currentSkin.Weapon.IsCanAttack;

	private string animName;

	public virtual void OnInit() {
		IsDead = false;
		score = 0;
		
		WearClothes();

		indicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
		indicator.SetTarget(indicatorPoint);
	}
	
	public virtual void OnDespawn() {
		TakeOffClothes();
		SimplePool.Despawn(indicator);
	}
	
	public virtual void OnDeath() {
		ChangeAnim(Anim.die.ToString());
		// LevelManager.Ins.CharecterDeath(this);
	}
	
	public virtual void WearClothes() {

	}
	
	public virtual void TakeOffClothes() {
		currentSkin?.OnDespawn();
		SimplePool.Despawn(currentSkin);
	}
	
	public void SetMask(bool isActive) {
		mask.SetActive(isActive);
	}
	
	public void ResetAnim() {
		animName = "";
	}

	public virtual void OnHit() {
		
	}

	public virtual void OnAttack() {
		target = GetTargetInRange();

		if (IsCanAttack && target != null && !target.IsDead) {
			targetPoint = target.TF.position;
			TF.LookAt(targetPoint);
			ChangeAnim(Anim.attack.ToString());
		}
	}

	public virtual void OnStopMove() {
		ChangeAnim(Anim.idle.ToString());
	}
	
	public virtual void AddTarget(Character target) {
		targets.Add(target);
	}
	
	public virtual void RemoveTarget(Character target) {
		targets.Remove(target);
		this.target = null;
	}

	public void ChangeAnim(string anim) {
		if (animName != anim) {
			currentSkin.Anim.ResetTrigger(animName);
			animName = anim;
			currentSkin.Anim.SetTrigger(animName);
		}
	}

	#region Skin
	protected void ChangeSkin(SkinType skinType) {
		currentSkin = SimplePool.Spawn<Skin>((PoolType)skinType, TF);
	}

	protected void ChangeWeapon(WeaponType weaponType)
	{
		currentSkin.ChangeWeapon(weaponType);
	}
	
	protected void ChangeAccessory(AccessoryType accessoryType)
	{
		currentSkin.ChangeAccessory(accessoryType);
	}

	protected void ChangeHat(HatType hatType)
	{
		currentSkin.ChangeHat(hatType);
	}

	protected void ChangePant(PantType pantType)
	{
		currentSkin.ChangePant(pantType);
	}
	

	#endregion
	

	private Character GetTargetInRange() {
		Character res = null;
		float distance = float.PositiveInfinity;

		for (int i = 0; i < targets.Count; ++i) {
			if (targets[i] != null && targets[i] != this && !targets[i].IsDead && Vector3.Distance(TF.position, targets[i].TF.position) <= Constant.ATT_RANGE * size + targets[i].size) {
				float dis = Vector3.Distance(TF.position, targets[i].TF.position);

				if (dis < distance) {
					distance = dis;
					target = targets[i];
				}
			}
		}

		return res;
	}
}
