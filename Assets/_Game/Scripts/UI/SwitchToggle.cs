using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
	[SerializeField] private RectTransform rectTransform;
	[SerializeField] private Image checkMark;
	[SerializeField] private Sprite imgCheckOn;
	[SerializeField] private Sprite imgCheckOff;
	[SerializeField] private Toggle toggle;
	[SerializeField] private SettingType type;

	private Vector2 handlePos;

	private void OnEnable() {
		handlePos = rectTransform.anchoredPosition;
		Debug.Log(handlePos);
		toggle.onValueChanged.AddListener(OnSwitch);
		switch (type) {
			case SettingType.Sound:
				toggle.isOn = DataManager.Ins.IsSound;
				OnSwitch(toggle.isOn);
				break;
			case SettingType.Vibrate:
				toggle.isOn = DataManager.Ins.IsVibrate;
				OnSwitch(toggle.isOn);
				break;
		}
	}

	private void OnDisable() {
		toggle.onValueChanged.RemoveListener(OnSwitch);
	}

	private void OnSwitch(bool isOn) {
		checkMark.sprite = isOn ? imgCheckOn : imgCheckOff;
		handlePos.x = isOn ? Mathf.Abs(handlePos.x) : -Mathf.Abs(handlePos.x);
		rectTransform.anchoredPosition = handlePos;
		switch (type) {
			case SettingType.Sound:
				DataManager.Ins.IsSound = isOn;
				break;
			case SettingType.Vibrate:
				DataManager.Ins.IsVibrate = isOn;
				break;
		}
	}
}