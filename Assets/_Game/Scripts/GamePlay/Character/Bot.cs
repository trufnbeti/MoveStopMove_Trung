using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character {
	[SerializeField] private NavMeshAgent agent;
	private IState<Bot> currentState;
	private Vector3 destination;
	public bool IsDestination => Vector3.Distance(TF.position, destination) - Mathf.Abs(TF.position.y - destination.y) < 0.1f;
	private bool IsCanRunning => GameManager.Ins.IsState(GameState.Gameplay) || GameManager.Ins.IsState(GameState.Revive);

	private CounterTime counter = new CounterTime();
	public CounterTime Counter => counter;

	public override void OnInit() {
		base.OnInit();
		
		SetMask(false);
		ResetAnim();
		Name = NameUtility.GetRandomName();
	}

	public override void OnDespawn() {
		base.OnDespawn();
		SimplePool.Despawn(this);
	}
	
	public override void OnDeath() {
		ChangeState(null);
		OnStopMove();
		base.OnDeath();
		SetMask(false);
		StartCoroutine(WaitForDespawn(2f));
	}

	protected override void WearClothes() {
		base.WearClothes();

		ChangeSkin(0);
		ChangeWeapon(0);
		ChangeHat(0);
		ChangeAccessory(0);
		ChangePant(0);
	}
	
	public void SetDestination(Vector3 point) {
		destination = point;
		agent.enabled = true;
		agent.SetDestination(destination);
		ChangeAnim(Anim.run.ToString());
	}

	public override void OnStopMove() {
		base.OnStopMove();
		agent.enabled = false;
	}

	public void ChangeState(IState<Bot> state)
	{
		if (currentState != null)
		{
			currentState.OnExit(this);
		}
		currentState = state;
		if (currentState != null)
		{
			currentState.OnEnter(this);
		}
	}
	
	public override void AddTarget(Character target) {
		base.AddTarget(target);
		if (!IsDead && Utilities.Chance(50) && IsCanRunning) {
			ChangeState(new AttackState());
		}
	}

	private IEnumerator WaitForDespawn(float time) {
		yield return CacheComponent.GetWFS(time);
		OnDespawn();
	}
	
	private void Update() {
		if (IsCanRunning && currentState != null && !IsDead) {
			currentState.OnExecute(this);
		}
	}
}