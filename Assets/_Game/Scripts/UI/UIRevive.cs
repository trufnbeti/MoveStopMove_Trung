using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRevive : UICanvas
{
    [SerializeField] private Text counterTxt;
    private float counter;

    public override void Setup() {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.Revive);
        counter = 5;
        StartCoroutine(Cout(counter));
    }

    public void OnBtnCloseClick() {
        CloseDirectly();
        this.PostEvent(EventID.Lose);
    }

    public void OnBtnReviveClick() {
        Time.timeScale = 1;
        GameManager.Ins.ChangeState(GameState.Gameplay);
        CloseDirectly();
        this.PostEvent(EventID.Revive);
        UIManager.Ins.OpenUI<UIGameplay>();
    }

    private IEnumerator Cout(float time) {
        for (int i = 0; i < time; ++i) {
            SoundManager.Ins.Play(SoundType.Count, ref SoundManager.Ins.audioSource);
            yield return CacheComponent.GetWFS(1f);
        }
    } 

    private void Update() {
        if (counter > 0) {
            counter -= Time.deltaTime;
            counterTxt.text = Mathf.Floor(counter).ToString();

            if (counter <= 0) {
                OnBtnCloseClick();
            }
        }
    }
}
