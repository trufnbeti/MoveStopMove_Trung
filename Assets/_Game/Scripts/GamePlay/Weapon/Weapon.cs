using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] GameObject child;
	[SerializeField] PoolType bulletType;
	public const float TIME_WEAPON_RELOAD = 0.5f;

	public bool IsCanAttack => child.activeSelf;

	public void SetActive(bool active) {
		child.SetActive(active);
	}

	private void OnEnable() {
		SetActive(true);
	}

	public void Throw(Character character, Vector3 target, float size) {
		SoundManager.Ins.Play(SoundType.ThrowWeapon, ref character.audioSource);
		Bullet bullet = SimplePool.Spawn<Bullet>(bulletType, transform.position, Quaternion.identity);
		bullet.OnInit(character, target, size);
		bullet.TF.localScale = size * Vector3.one;
		SetActive(false);

		StartCoroutine(WaitForReload(TIME_WEAPON_RELOAD));
	}

	private IEnumerator WaitForReload(float time) {
		yield return CacheComponent.GetWFS(time);
		SetActive(true);
	}
}