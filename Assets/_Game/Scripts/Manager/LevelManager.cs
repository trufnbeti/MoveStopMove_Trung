using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager> {
    [SerializeField] private Level[] levels;
    public Player player;
    [HideInInspector] public Level currentLevel;
    private List<Bot> bots = new List<Bot>();
    private int botLeft;
    private bool isRevive;
    private int levelIdx;
    public int TotalCharater => botLeft + bots.Count + 1;

    #region Event

    private Action<object> actionPlay;
    private Action<object> actionHome;
    private Action<object> actionRevive;
    private Action<object> actionAddCoin;
    private Action<object> actionLose;
    private Action<object> actionNextLevel;

    #endregion

    private void Start() {
        levelIdx = 0;
        OnLoadLevel(levelIdx);
        OnInit();
    }

    private void OnEnable() {
        actionPlay = (param) => OnPlay();
        actionHome = (param) => Home();
        actionRevive = (param) => OnRevive();
        actionLose = (param) => OnLose();
        actionAddCoin = (param) => {
            DataManager.Ins.Coin += player.Coin;
        };
        actionNextLevel = (param) => {
            levelIdx++;
        };
        
        this.RegisterListener(EventID.Play, actionPlay);
        this.RegisterListener(EventID.Home, actionHome);
        this.RegisterListener(EventID.Revive, actionRevive);
        this.RegisterListener(EventID.AddCoin, actionAddCoin);
        this.RegisterListener(EventID.Lose, actionLose);
        this.RegisterListener(EventID.NextLevel, actionNextLevel);
    }

    private void OnDisable() {
        this.RemoveListener(EventID.Play, actionPlay);
        this.RemoveListener(EventID.Home, actionHome);
        this.RemoveListener(EventID.Revive, actionRevive);
        this.RemoveListener(EventID.AddCoin, actionAddCoin);
        this.RemoveListener(EventID.Lose, actionLose);
        this.RemoveListener(EventID.NextLevel, actionNextLevel);
    }

    private void OnInit() {
        player.OnInit();

        for (int i = 0; i < currentLevel.botReal; ++i) {
            SpawnBot(null);
        }

        isRevive = false;
        botLeft = currentLevel.totalCharacter - currentLevel.botReal - 1;
    }
    
    public void OnLoadLevel(int level) {
        if (currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
    }

    public void OnPlay() {
        for (int i = 0; i < bots.Count; ++i) {
            bots[i].ChangeState(new PatrolState());
        }
    }

    public Vector3 RandomPoint() {
        Vector3 randPoint = Vector3.zero;
        float size = Constant.ATT_RANGE + Constant.MAX_SIZE + 1f;
        do {
            randPoint = currentLevel.RandomPoint();
            if (Vector3.Distance(randPoint, player.TF.position) < size) {
                continue;
            }

            if (bots.Count == 0) {
                return randPoint;
            }

            for (int i = 0; i < bots.Count; ++i) {
                if (Vector3.Distance(randPoint, bots[i].TF.position) < size) {
                    break;
                }

                if (i == bots.Count - 1) {
                    return randPoint;
                }
            }
        } while (true);
    }

    public void CharacterDeath(Character character) {
        if (character is Player) {
            UIManager.Ins.CloseAll();

            if (!isRevive) {
                isRevive = true;
                UIManager.Ins.OpenUI<UIRevive>();
            } else {
                OnLose();
            }
        } else if (character is Bot) {
            bots.Remove(character as Bot);

            if (GameManager.Ins.IsState(GameState.Revive)) {
                SpawnBot(Utilities.Chance(50) ? new IdleState() : new PatrolState());
            } else {
                if (botLeft > 0) {
                    --botLeft;
                    SpawnBot(Utilities.Chance(50) ? new IdleState() : new PatrolState());
                }

                if (bots.Count == 0) {
                    OnWin();
                }
            }
        }
        this.PostEvent(EventID.CharacterDeath);
    }

    private void SpawnBot(IState<Bot> state) {
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, RandomPoint(), Quaternion.identity);
        bot.OnInit();
        bot.ChangeState(state);
        bots.Add(bot);
        
        bot.SetScore(player.Score > 0 ? Random.Range(player.Score - 2, player.Score + 2) : 1);
    }

    #region Using Event

    private void OnReset() {
        player.OnDespawn();
        for (int i = 0; i < bots.Count; i++) {
            bots[i].OnDespawn();
        }

        bots.Clear();
        SimplePool.CollectAll();
    }

    private void OnRevive() {
        player.TF.position = RandomPoint();
        player.OnRevive();
    }

    private void OnLose() {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UILose>().SetCoin(player.Coin); 
    }

    private void OnWin() {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIWin>().SetCoin(player.Coin);
        player.ChangeAnim(Anim.win.ToString());
    }

    #endregion

    private void Home() {
        UIManager.Ins.CloseAll();
        OnReset();
        OnLoadLevel(levelIdx);
        OnInit();
        UIManager.Ins.OpenUI<UIMainMenu>();
    }
}
