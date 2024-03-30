using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform minPoint,maxPoint;
    public int botReal = 10;
    public int totalCharacter = 50;

    private Vector3 randPoint;
    public Vector3 RandomPos() {
        randPoint = Random.Range(minPoint.position.x, maxPoint.position.x) * Vector3.right +
                    Random.Range(minPoint.position.z, maxPoint.position.z) * Vector3.forward;
        NavMeshHit hit;
        NavMesh.SamplePosition(randPoint, out hit, float.PositiveInfinity, NavMesh.AllAreas);
        randPoint = hit.position;
        randPoint.y = 0;

        return randPoint;
    }
}
