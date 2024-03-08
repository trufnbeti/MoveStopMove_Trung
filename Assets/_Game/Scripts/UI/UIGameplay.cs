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

    private void OnEnable() {
        actionCharacterDeath = (param) => UpdateAmount();
    }

    public override void Setup() {
        base.Setup();
        UpdateAmount();
    }

    public override void Open() {
        base.Open();
        
        this.RegisterListener(EventID.CharacterDeath, actionCharacterDeath);
        
        GameManager.Ins.ChangeState(GameState.Gameplay);
        // LevelManager.Ins.SetTargetIndicatorAlpha(1);
    }
    
    public override void CloseDirectly() {
        this.RemoveListener(EventID.CharacterDeath, actionCharacterDeath);
        base.CloseDirectly();
        // LevelManager.Ins.SetTargetIndicatorAlpha(0);
    }

    private void UpdateAmount() {
        characterAmount.text = "Alive: " + LevelManager.Ins.TotalCharater;
    }
}
