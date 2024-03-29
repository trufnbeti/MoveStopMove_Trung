using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
	public void OnEnter(Bot t) {
		t.SetDestination(LevelManager.Ins.currentLevel.RandomPos());
	}

	public void OnExecute(Bot t) {
		if (t.IsDestination) {
			t.ChangeState(new IdleState());
		}
	}

	public void OnExit(Bot t) {

	}

}