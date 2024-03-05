using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    public override void Open() {
        base.Open();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollow.Ins.ChangeState(CameraState.MainMenu);
    }

    public void OnBtnPlayClick() {
        
    }
}
