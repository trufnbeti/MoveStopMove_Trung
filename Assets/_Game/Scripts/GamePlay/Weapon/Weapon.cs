using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
	[SerializeField] GameObject child;
	[SerializeField] BulletType bulletType;
	public const float TIME_WEAPON_RELOAD = 0.5f;

	public bool IsCanAttack => child.activeSelf;

	public void SetActive(bool active) {
		child.SetActive(active);
	}

	private void OnEnable() {
		SetActive(true);
	}

	public void Throw(Character character, Vector3 target, float size) {
		Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, TF.position, Quaternion.identity);
		bullet.OnInit(character, target, size);
		bullet.TF.localScale = size * Vector3.one;
		SetActive(false);

		StartCoroutine(WaitForReload(TIME_WEAPON_RELOAD));
	}

	private IEnumerator WaitForReload(float time) {
		yield return CacheComponent.GetWFS(time);
		OnEnable();
	}
}