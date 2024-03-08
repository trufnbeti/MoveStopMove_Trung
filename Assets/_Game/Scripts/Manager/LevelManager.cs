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

    #endregion

    private void Start() {
        levelIdx = 0;
        OnLoadLevel(levelIdx);
        OnInit();
    }

    private void OnEnable() {
        actionPlay = (param) => OnPlay();
        
        this.RegisterListener(EventID.Play, actionPlay);
    }

    private void OnDisable() {
        this.RemoveListener(EventID.Play, actionPlay);
    }

    private void OnInit() {
        player.OnInit();

        for (int i = 0; i < currentLevel.botReal; ++i) {
            SpawnBot(null);
        }

        isRevive = false;
        botLeft = currentLevel.botTotal - currentLevel.botReal - 1;
        SetTargetIndicatorAlpha(0);
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

        SetTargetIndicatorAlpha(1f);
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
    
    public void SetTargetIndicatorAlpha(float alpha) {
        List<GameUnit> list = SimplePool.GetAllUnitIsActive(PoolType.TargetIndicator);

        for (int i = 0; i < list.Count; i++)
        {
            (list[i] as TargetIndicator).SetAlpha(alpha);
        }
    }

    public void CharacterDeath(Character character) {
        if (character is Player) {
            Debug.Log("LOSE");
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
                    Debug.Log("WIN");
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
}
