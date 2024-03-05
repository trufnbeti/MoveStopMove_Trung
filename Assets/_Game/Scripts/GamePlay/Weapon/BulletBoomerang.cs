using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoomerang : Bullet {
    [SerializeField] private Transform child;
    
    private Vector3 target;
    private enum State {
        Forward,
        Backward,
        Stop
    }
    private State state;
    
    public override void OnInit(Character character, Vector3 target , float size)
    {
        base.OnInit(character, target, size);
        this.target = target;
        state = State.Forward;
    }

    private void Update() {
        switch (state) {
            case State.Forward:
                TF.position = Vector3.MoveTowards(TF.position, target, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(TF.position, target) < 0.1f) {
                    state = State.Backward;
                }
                child.Rotate(Vector3.up * -6, Space.Self);
                break;
            case State.Backward:
                TF.position = Vector3.MoveTowards(TF.position, character.TF.position, moveSpeed * Time.deltaTime);
                if (character.IsDead || Vector3.Distance(TF.position, this.character.TF.position) < 0.1f) {
                    OnDespawn();
                }
                child.Rotate(Vector3.up * -6, Space.Self);
                break;
        }
    }
}
