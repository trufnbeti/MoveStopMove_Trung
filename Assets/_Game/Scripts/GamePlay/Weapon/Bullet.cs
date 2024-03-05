using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
	protected Character character;
	[SerializeField] protected float moveSpeed = 7f;

	public virtual void OnInit(Character character, Vector3 target, float size) {
		this.character = character;
		TF.forward = (target - TF.position).normalized;
	}

	public void OnDespawn() {
		SimplePool.Despawn(this);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(GameTag.Character.ToString())) {
			Character hit = CacheComponent.GetCharacter(other);
			
			if (hit != null && hit != character) {
				hit.OnHit();
			}
		}

	}
}