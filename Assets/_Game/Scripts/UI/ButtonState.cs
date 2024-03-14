using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState : MonoBehaviour {
	[SerializeField] private GameObject[] btnObjs;

	public void SetState(StateButton state) {
		DeactiveBtn();

		btnObjs[(int)state].SetActive(true);
	}

	public void DeactiveBtn() {
		for (int i = 0; i < btnObjs.Length; i++) {
			btnObjs[i].SetActive(false);
		}
	}

}
