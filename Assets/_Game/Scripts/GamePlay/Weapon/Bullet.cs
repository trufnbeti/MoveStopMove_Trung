using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class Bullet : GameUnit
{
	protected Character owner;
	[SerializeField] protected float moveSpeed = 7f;

	public virtual void OnInit(Character character, Vector3 target, float size) {
		this.owner = character;
		TF.forward = (target - TF.position).normalized;
	}

	public void OnDespawn() {
		SimplePool.Despawn(this);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(GameTag.Character.ToString())) {
			Character hit = CacheComponent.GetCharacter(other);
			
			if (hit != null && hit != owner && !hit.IsDead && !owner.IsDead) {
				owner.AddScore();
				hit.OnHit(owner);
				
				if (owner is Player || hit is Player) {
					VibrationsManager.instance.TriggerMediumImpact();
					
					if (owner is Player) {
						int score = owner.Score;
						float distance = Mathf.Round(Vector3.Distance(owner.TF.position, hit.TF.position) * 10f) / 10f;
						SimplePool.Spawn<TextCombat>(PoolType.TextCombat, Vector3.zero, Quaternion.identity).OnInit(1, distance, score % 2 == 0);
					}
				}
				
				SimplePool.Despawn(this);
			}
			
		}

	}
}