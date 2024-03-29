using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Character character;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameTag.Character.ToString())) {
            Character target = CacheComponent.GetCharacter(other);
            if (!target.IsDead && !character.IsDead) {
                character.AddTarget(target);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(GameTag.Character.ToString())) {
            Character target = CacheComponent.GetCharacter(other);
            character.RemoveTarget(target);
        }
    }
}
