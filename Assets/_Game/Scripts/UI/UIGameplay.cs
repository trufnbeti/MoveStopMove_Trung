using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : UICanvas
{
    [SerializeField] private Text characterAmount;
    
    #region Event

    private Action<object> actionCharacterDeath;

    #endregion

    public override void Setup() {
        base.Setup();
        actionCharacterDeath = (param) => UpdateAmount();
        UpdateAmount();
    }

    public override void Open() {
        base.Open();
        
        this.RegisterListener(EventID.CharacterDeath, actionCharacterDeath);
        
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }

    public void OnBtnSettingClick() {
        UIManager.Ins.OpenUI<UISetting>();
    }
    
    public override void CloseDirectly() {
        this.RemoveListener(EventID.CharacterDeath, actionCharacterDeath);
        base.CloseDirectly();
    }

    private void UpdateAmount() {
        characterAmount.text = "Alive: " + LevelManager.Ins.TotalCharater;
    }
}
