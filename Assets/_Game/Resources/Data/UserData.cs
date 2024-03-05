using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class UserData : ScriptableObject
{
    private static UserData ins;
    public static UserData Ins
    {
        get
        {
            if (ins == null)
            {
                UserData[] datas = Resources.LoadAll<UserData>("");

                if (datas.Length == 1)
                {
                    ins = datas[0];
                }
                else
                if (datas.Length == 0)
                {
                    Debug.LogError("Can find Scriptableobject UserData");
                }
                else
                {
                    Debug.LogError("have multiple Scriptableobject UserData");
                }
            }

            return ins;
        }
    }

    public const string Keys_Weapon_Data = "WeaponDatas";
    public const string Keys_Hat_Data = "HatDatas";
    public const string Keys_Pant_Data = "PantDatas";
    public const string Keys_Accessory_Data = "AccessoryDatas";
    public const string Keys_Skin_Data = "SkinDatas";


    public int level = 0;
    public int coin = 0;

    public bool soundIsOn = true;
    public bool vibrate = true;
    public bool removeAds = false;
    public bool tutorialed = false;

    public WeaponType playerWeapon;
    public HatType playerHat;
    public PantType playerPant;
    public AccessoryType playerAccessory;
    public SkinType playerSkin;

    //Example
    // UserData.Ins.SetInt(UserData.Key_Level, ref UserData.Ins.level, 1);

    //data for list
    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public void SetDataState(string key, int ID, int state)
    {
        PlayerPrefs.SetInt(key + ID, state);
    }
    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public int GetDataState(string key, int ID, int state = 0)
    {
        return PlayerPrefs.GetInt(key + ID, state);
    }

    public void SetDataState(string key, int state)
    {
        PlayerPrefs.SetInt(key, state);
    }

    public int GetDataState(string key, int state = 0)
    {
        return PlayerPrefs.GetInt(key, state);
    }

    /// <summary>
    /// Key_Name
    /// if(bool) true == 1
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetIntData(string key, ref int variable, int value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value);
    } 
    
    public void SetBoolData(string key, ref bool variable, bool value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public void SetFloatData(string key, ref float variable, float value)
    {
        variable = value;
        PlayerPrefs.GetFloat(key, value);
    }

    public void SetStringData(string key, ref string variable, string value)
    {
        variable = value;
        PlayerPrefs.SetString(key, value);
    }

    public void SetEnumData<T>(string key, ref T variable, T value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    public void SetEnumData<T>(string key,T value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    public T GetEnumData<T>(string key, T defaultValue) where T : Enum
    {
        return (T)Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
    }


#if UNITY_EDITOR
    [Space(10)]
    [Header("---- Editor ----")]
    public bool isTest;
#endif

    public void OnInitData()
    {

#if UNITY_EDITOR
        if (isTest) return;
#endif

        level = PlayerPrefs.GetInt(PrefKey.Level.ToString(), 0);
        coin = PlayerPrefs.GetInt(PrefKey.Coin.ToString(), 0);

        removeAds = PlayerPrefs.GetInt(PrefKey.RemoveAds.ToString(), 0) == 1;
        tutorialed =  PlayerPrefs.GetInt(PrefKey.Tutorial.ToString(), 0) == 1;
        soundIsOn =  PlayerPrefs.GetInt(PrefKey.SoundIsOn.ToString(), 0) == 1;
        vibrate =  PlayerPrefs.GetInt(PrefKey.VibrateIsOn.ToString(), 0) == 1;

        playerWeapon = GetEnumData(PrefKey.PlayerWeapon.ToString(), WeaponType.Hammer_1);
        playerHat = GetEnumData(PrefKey.PlayerHat.ToString(), HatType.Arrow);
        playerPant = GetEnumData(PrefKey.PlayerPant.ToString(), PantType.Pant_1);
        playerAccessory = GetEnumData(PrefKey.PlayerAccessory.ToString(), AccessoryType.None);
        playerSkin = GetEnumData(PrefKey.PlayerSkin.ToString(), SkinType.Normal);
    }

    public void OnResetData()
    {
        PlayerPrefs.DeleteAll();
        OnInitData();
    }

}



#if UNITY_EDITOR

[CustomEditor(typeof(UserData))]
public class UserDataEditor : Editor
{
    UserData userData;

    private void OnEnable()
    {
        userData = (UserData)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Load Data"))
        {
            userData.OnInitData();
            EditorUtility.SetDirty(userData);
        }
       
    }
}

#endif