using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Character : GameUnit {
	public Transform indicatorPoint;
	
	[SerializeField] GameObject mask;
	
	private List<Character> targets = new List<Character>();
	protected Character target;
	
	[SerializeField] private Vector3 targetPoint;

	#region Sound
	
	// [SerializeField] private Sound[] audioSources;
	[SerializeField] public AudioSource audioSource;
	//
	// private Dictionary<SoundType, Sound> sounds = new Dictionary<SoundType, Sound>();
	
	#endregion

	private int score;
	protected float size = 1;
	private string name;

	public string Name {
		get => name;
		set => name = value;
	}

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
		ClearTarget();
	}
	
	public virtual void OnDespawn() {
		TakeOffClothes();
	}
	
	public virtual void OnDeath() {
		ChangeAnim(Anim.die.ToString());
		LevelManager.Ins.CharacterDeath(this);
	}
	
	protected virtual void WearClothes() {

	}
	
	protected void TakeOffClothes() {
		currentSkin?.OnDespawn();
		Destroy(currentSkin.gameObject);
	}
	
	public void SetMask(bool isActive) {
		mask.SetActive(isActive);
	}
	
	public void SetScore(int score) {
		this.score = score > 0 ? score : 0;
		SetSize(1 + this.score * 0.1f);
	}
	
	protected virtual void SetSize(float size) {
		size = Mathf.Clamp(size, Constant.MIN_SIZE, Constant.MAX_SIZE);
		this.size = size;
		TF.localScale = size * Vector3.one;
	}
	
	public void ResetAnim() {
		animName = "";
	}

	public virtual void OnHit(Character character) {
		if (!IsDead) {
			IsDead = true;
			OnDeath();
		}
	}

	public virtual void OnAttack() {
		target = GetTargetInRange();

		if (IsCanAttack && target != null && !target.IsDead) {
			targetPoint = target.TF.position;
			targetPoint.y = TF.position.y;
			TF.LookAt(targetPoint);
			ChangeAnim(Anim.attack.ToString());
			
		}
	}
	
	public void Throw() {
		currentSkin.Weapon.Throw(this, targetPoint, size);
	}

	public virtual void OnStopMove() {
		ChangeAnim(Anim.idle.ToString());
	}
	
	public void AddScore(int amount = 1) {
		SetScore(score + amount);
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
	protected void ChangeSkin(int index) {
		currentSkin = Instantiate(SkinManager.Ins.skinData.GetPrefab(index), TF);
	}

	protected void ChangeWeapon(int index)
	{
		currentSkin.ChangeWeapon(index);
	}
	
	protected void ChangeAccessory(int index)
	{
		currentSkin.ChangeAccessory(index);
	}

	protected void ChangeHat(int index)
	{
		currentSkin.ChangeHat(index);
	}

	protected void ChangePant(int index)
	{
		currentSkin.ChangePant(index);
	}
	
	#endregion

	#region Sound
	
	protected void Play(SoundType type) {
		
	}

	#endregion
	
	protected void ClearTarget() {
		targets.Clear();
	}
	

	private Character GetTargetInRange() {
		Character res = null;
		float distance = float.PositiveInfinity;

		for (int i = 0; i < targets.Count; ++i) {
			if (targets[i] != null && targets[i] != this && !targets[i].IsDead && Vector3.Distance(TF.position, targets[i].TF.position) <= Constant.ATT_RANGE * size + targets[i].size) {
				float dis = Vector3.Distance(TF.position, targets[i].TF.position);

				if (dis < distance) {
					distance = dis;
					res = targets[i];
				}
			}
		}

		return res;
	}
}
