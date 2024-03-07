using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
	public void OnEnter(Bot t) {
		t.OnStopMove();
		t.OnAttack();
		if (t.IsCanAttack) {
			t.Counter.Start(() => {
					t.Throw();
					t.Counter.Start(() => {
							t.ChangeState(Utilities.Chance(50) ? new IdleState() : new PatrolState());
						}, Constant.TIME_DELAY_THROW);
				}, Constant.TIME_DELAY_THROW
			);
		}
	}

	public void OnExecute(Bot t)
	{
		t.Counter.Execute();
	}

	public void OnExit(Bot t)
	{
	}

}