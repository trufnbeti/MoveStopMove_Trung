using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UICanvas
{
    public override void Setup() {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.Setting);
        UIManager.Ins.CloseUI<UIGameplay>();
        Time.timeScale = 0;
    }

    public void OnBtnHomeClick() {
        Time.timeScale = 1;
        this.PostEvent(EventID.Home);
    }
    
    public void OnBtnContinueClick() {
        Time.timeScale = 1;
        GameManager.Ins.ChangeState(GameState.Gameplay);
        UIManager.Ins.OpenUI<UIGameplay>();
        CloseDirectly();
    }
}
