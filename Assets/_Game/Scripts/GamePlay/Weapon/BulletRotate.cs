using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotate : Bullet {
    [SerializeField] private Transform child;
    private float range;
    
    public override void OnInit(Character character, Vector3 target, float size) {
        base.OnInit(character, target, size);
        range = Constant.ATT_RANGE * size - Vector3.Distance(TF.position, owner.TF.position);
    }

    private void Update() {
        if (range <= 0) {
            OnDespawn();
        } else {
            range -= moveSpeed * Time.deltaTime;
            TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
            child.Rotate(Vector3.up * -6, Space.Self);
        }
    }
}
