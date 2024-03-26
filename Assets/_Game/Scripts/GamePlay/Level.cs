using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Level : MonoBehaviour
{
    [SerializeField] Transform minPoint,maxPoint;
    public int botReal = 10;
    public int totalCharacter = 50;

    private Vector3 randPoint;
    public Vector3 RandomPoint() {
        float size = Constant.ATT_RANGE + Constant.MAX_SIZE + 1f;
        randPoint = Random.Range(minPoint.position.x, maxPoint.position.x) * Vector3.right +
                    Random.Range(minPoint.position.z, maxPoint.position.z) * Vector3.forward;
        NavMeshHit hit;
        NavMesh.SamplePosition(randPoint, out hit, float.PositiveInfinity, NavMesh.AllAreas);
        randPoint = hit.position;

        return randPoint + Vector3.up;
    }
}
