using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : UICanvas
{
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private ButtonState buttonState;
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text coinTxt;
    [SerializeField] private Text playerCoinTxt;
    
    private Weapon currentWeapon;
    private int currentIdx;
    private WeaponType weaponType;

    public override void Setup() {
        base.Setup();
        ChangeWeapon(DataManager.Ins.IdWeapon);
        playerCoinTxt.text = DataManager.Ins.Coin.ToString();
    }

    public override void CloseDirectly() {
        base.CloseDirectly();
        if (currentWeapon != null) {
            Destroy(currentWeapon.gameObject);
            currentWeapon = null;
        }
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    public void OnBtnNextClick() {
        ChangeWeapon(weaponData.NextWeaponIdx(currentIdx));
    }

    public void OnBtnPrevClick() {
        ChangeWeapon(weaponData.PrevWeaponIdx(currentIdx));
    }

    public void OnBtnBuyClick() {
        // if (UserData.Ins.coin >= weaponIte)
    }

    public void ChangeWeapon(int index) {
        currentIdx = index;

        if (currentWeapon != null ) {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = Instantiate(SkinManager.Ins.weaponData.GetWeapon(index), weaponPoint);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = Vector3.one;
        
        //check data dong
        int stateWeapon = DataManager.Ins.GetStateData(index, typeof(WeaponType));
        StateButton state = (StateButton)stateWeapon;
        buttonState.SetState(state);
        WeaponItem item = weaponData.GetWeaponItem(index);
        nameTxt.text = item.name;
        coinTxt.text = item.cost.ToString();
        
    }
}
