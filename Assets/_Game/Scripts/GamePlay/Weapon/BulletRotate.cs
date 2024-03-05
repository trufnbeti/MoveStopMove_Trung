using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotate : Bullet {
    [SerializeField] private Transform child;
    public const float TIME_ALIVE = 1f;

    private CounterTime counter = new CounterTime();
    
    public override void OnInit(Character character, Vector3 target, float size) {
        base.OnInit(character, target, size);
        counter.Start(OnDespawn, TIME_ALIVE * size);
    }

    private void Update() {
        counter.Execute();
        TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
        child.Rotate(Vector3.up * -6, Space.Self);
    }
}
