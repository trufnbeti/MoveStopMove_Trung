using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine.Serialization;

public class DataManager : Singleton<DataManager>
{
    public bool isLoaded = false;
    public PlayerData playerData;
    // public const string PLAYER_DATA = "PLAYER_DATA";
    // public List<int> list_IDHairNonVIP;
    // public List<int> list_IDHairMan;
    // public List<int> list_IDHairWoman;
    // public List<int> list_IDBodyMan;
    // public List<int> list_IDBodyWoman;
    // public List<int> list_IDHairVIP;

    // private void OnApplicationPause(bool pause) { SaveData(); FirebaseManager.Ins.OnSetUserProperty();  }
    // private void OnApplicationQuit() { SaveData(); FirebaseManager.Ins.OnSetUserProperty();  }
    
    //cache string
    private const string WEAPON_TYPE = "WeaponType";
    private const string SKIN_TYPE = "SkinType";
    private const string HAT_TYPE = "HatType";
    private const string ACC_TYPE = "AccessoryType";
    private const string PANT_TYPE = "PantType";
    
    private void Awake() {
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    public void LoadData() {
        // string d = PlayerPrefs.GetString(PLAYER_DATA, "");
        string d = JsonUtility.ToJson(Pref.PlayerData);
        if (d != "") {
            playerData = JsonUtility.FromJson<PlayerData>(d);
        }
        else {
            playerData = new PlayerData();
        }

        //loadskin
        //load pet

        // sau khi hoàn thành tất cả các bước load data ở trên
        isLoaded = true;
        // FirebaseManager.Ins.OnSetUserProperty();  
    }

    #region Get set data
    
    public int Coin {
        set {
            playerData.coin = value;
            SaveData();
        }
        get => playerData.coin;
    }

    public int Level {
        set {
            playerData.level = value;
            SaveData();
        }
        get => playerData.level;
    }
    
    public int IdSkin {
        set {
            playerData.idSkin = value;
            SaveData();
        }
        get => playerData.idSkin;
    }
    
    public int IdHat {
        set {
            playerData.idHat = value;
            SaveData();
        }
        get => playerData.idHat;
    }
    
    public int IdPant {
        set {
            playerData.idPant = value;
            SaveData();
        }
        get => playerData.idPant;
    }
    
    public int IdWeapon {
        set {
            playerData.idWeapon = value;
            SaveData();
        }
        get => playerData.idWeapon;
    }
    
    public int IdAccessory {
        set {
            playerData.idAccessory = value;
            SaveData();
        }
        get => playerData.idAccessory;
    }
    
    public bool IsSound {
        set {
            playerData.isSound = value;
            SaveData();
        }
        get => playerData.isSound;
    }
    
    public bool IsMusic {
        set {
            playerData.isMusic = value;
            SaveData();
        }
        get => playerData.isMusic;
    }
    
    public bool IsVibrate {
        set {
            playerData.isVibrate = value;
            SaveData();
        }
        get => playerData.isVibrate;
    }
    
    public int[] SkinStatus {
        set {
            playerData.skinStatus = value;
            SaveData();
        }
        get => playerData.skinStatus;
    }
    
    public int[] HatStatus {
        set {
            playerData.hatStatus = value;
            SaveData();
        }
        get => playerData.hatStatus;
    }
    
    public int[] PantStatus {
        set {
            playerData.pantStatus = value;
            SaveData();
        }
        get => playerData.pantStatus;
    }
    
    public int[] WeaponStatus {
        set {
            playerData.weaponStatus = value;
            SaveData();
        }
        get => playerData.weaponStatus;
    }
    
    public int[] AccessoryStatus {
        set {
            playerData.accessoryStatus = value;
            SaveData();
        }
        get => playerData.accessoryStatus;
    }

    public void SetStateData(int index, int value, ShopType type) {
        switch (type) {
            case ShopType.Skin:
                SkinStatus[index] = value;
                break;
            case ShopType.Weapon:
                WeaponStatus[index] = value;
                break;
            case ShopType.Accessory:
                AccessoryStatus[index] = value;
                break;
            case ShopType.Hat:
                HatStatus[index] = value;
                break;
            case ShopType.Pant:
                PantStatus[index] = value;
                break;
        }
        
        SaveData();
    }

    public int GetStateData(int index, ShopType type) {
        int res = 0;
        switch (type) {
            case ShopType.Skin:
                res = SkinStatus[index];
                break;
            case ShopType.Weapon:
                res = WeaponStatus[index];
                break;
            case ShopType.Accessory:
                res = AccessoryStatus[index];
                break;
            case ShopType.Hat:
                res = HatStatus[index];
                break;
            case ShopType.Pant:
                res = PantStatus[index];
                break;
        }

        return res;
    }
    
    #endregion
    
    #if UNITY_EDITOR

    public void Reset() {
        isLoaded = true;
        playerData = new PlayerData();
        SaveData();
    }
    
    public void LoadDataTest() {
        isLoaded = true;
        SaveData();
    }

    #endif

    public void SaveData() {
        if (!isLoaded) return;
        Pref.PlayerData = playerData;
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(DataManager))]
public class DataManagerEditor : Editor
{
    private DataManager dataManager;
        
    private void OnEnable() {
        dataManager = (DataManager) target;
    }
        
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
            
        if (GUILayout.Button("Clear Data")) {
            dataManager.Reset();
            EditorUtility.SetDirty(dataManager);
        }
            
        if (GUILayout.Button("Load Data Test")) {
            dataManager.LoadDataTest();
            EditorUtility.SetDirty(dataManager);
        }
    }
}
#endif


[System.Serializable]
public class PlayerData {
    [Header("--------- Game Setting ---------")]
    public bool isNew = true;
    public bool isMusic = true;
    public bool isSound = true;
    public bool isVibrate = true;
    // public bool isNoAds = false;
    // public int starRate = -1;
    // public float volumeSound = 80f;


    [Header("--------- Game Params ---------")]
    public int coin = 20000;
    // public int cup = 0;
    public int level = 0;//Level hiện tại
    // public int season = 0;
    public int idSkin = 0; //Skin
    public int idHat = 0;
    public int idPant = 0;
    public int idWeapon = 0;
    public int idAccessory = 0;
    // public bool[] skinStatus = new bool[]{true, true,  true, true, true, true, true, 
    // true, true, true, true, true, true, true, 
    // true, true, true, true, true, true, true, true, true, true};
    public int[] skinStatus = new int[]{ 2, 0, 0, 0, 0, 0 };
    public int[] hatStatus = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] pantStatus = new int[] { 2, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] weaponStatus = new int[] { 2, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] accessoryStatus = new int[] { 0, 0, 0, 0, 0 };


    // [Header("--------- Firebase ---------")]
    // public string timeInstall;//Thời điểm cài game
    // public int timeLastOpen;//Thời điểm cuối cùng mở game. Tính số ngày kể từ 1/1/1970
    // public int timeInstallforFirebase; //Dùng trong hàm bắn Firebase UserProperty. Số ngày tính từ ngày 1/1/1970
    // public int daysPlayed = 0;//Số ngày đã User có mở game lên
    // public int sessionCount = 0;//Tống số session
    // public int playTime = 0;//Tổng số lần nhấn play game
    // public int playTime_Session = 0;//Số lần nhấn play game trong 1 session
    // public int dieCount_levelCur = 0;//Số lần chết tại level hiện tại
    // public int firstDayLevelPlayed = 0;  //Số level đã chơi ở ngày đầu tiên

    //--------- Others ---------

    // public int rw_watched = 0;
    // public int inter_watched = 0;
    // public int level_played_1stday = 0;
}