using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas {
    private const string ANIM_OPEN = "Open";
    private const string ANIM_CLOSE = "Close";
    [SerializeField] private Text playerCoinTxt;
    [SerializeField] private Animation anim;
    [SerializeField] private Image imgSoundOff;
    [SerializeField] private Image imgVibrateOn;
    [SerializeField] private InputField inputField;

    public override void Setup() {
        base.Setup();
        inputField.text = DataManager.Ins.Name;
        imgSoundOff.gameObject.SetActive(!DataManager.Ins.IsSound);
        imgVibrateOn.gameObject.SetActive(DataManager.Ins.IsVibrate);
        SoundManager.Ins.StopAllEfxSound();
    }

    public override void Open() {
        base.Open();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollow.Ins.ChangeState(CameraState.MainMenu);

        playerCoinTxt.text = DataManager.Ins.Coin.ToString();
        anim.Play(ANIM_OPEN);
    }

    #region BtnClick

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

    #endregion

    public void OnChangeName() {
        DataManager.Ins.Name = inputField.text;
    }

}
