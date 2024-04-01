using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Character owner;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameTag.Character.ToString())) {
            Character target = CacheComponent.GetCharacter(other);
            if (!target.IsDead && !owner.IsDead) {
                owner.AddTarget(target);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(GameTag.Character.ToString())) {
            Character target = CacheComponent.GetCharacter(other);
            owner.RemoveTarget(target);
        }
    }
}
