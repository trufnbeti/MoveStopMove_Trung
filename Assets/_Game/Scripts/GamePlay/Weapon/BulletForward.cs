using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : Bullet {
    private float range;
    
    public override void OnInit(Character character, Vector3 target, float size) {
        base.OnInit(character, target, size);
        range = Constant.ATT_RANGE * size;
    }

    private void Update() {
        if (range <= 0) {
            OnDespawn();
        } else {
            range -= moveSpeed * Time.deltaTime;
            TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
