using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas {
    private const string ANIM_OPEN = "Open";
    private const string ANIM_CLOSE = "Close";
    [SerializeField] Text playerCoinTxt;
    [SerializeField] Animation anim;

    public override void Open() {
        base.Open();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollow.Ins.ChangeState(CameraState.MainMenu);

        playerCoinTxt.text = UserData.Ins.coin.ToString();
        anim.Play(ANIM_OPEN);
    }

    public void OnBtnPlayClick() {
        this.PostEvent(EventID.Play);
        UIManager.Ins.OpenUI<UIGameplay>();
        CameraFollow.Ins.ChangeState(CameraState.Gameplay);
        Close(0.5f);
        anim.Play(ANIM_CLOSE);
    }

    public void OnBtnWeaponClick() {
        UIManager.Ins.OpenUI<UIWeapon>();
        CloseDirectly();
    }

    public void OnBtnShopClick() {
        UIManager.Ins.OpenUI<UIShop>();
        CloseDirectly();
    }

}
