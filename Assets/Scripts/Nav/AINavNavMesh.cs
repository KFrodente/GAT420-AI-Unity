using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavNavMesh : MonoBehaviour
{
    public NavMeshAgent agent;

    private AINavNode currentNode;

    private bool hasLocation;

    public float minDistance;

    private void Start()
    {
        SetNextDestination();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, currentNode.transform.position) < minDistance && hasLocation)
        {
            SetNextDestination();
        }
        if (!hasLocation)
        {
            SetNextDestination();
        }
    }

    private void SetNextDestination()
    {
        if (NodeHolder.Instance.nodes.Count > 0)
        {

            currentNode = NodeHolder.Instance.nodes[Random.Range(0, NodeHolder.Instance.nodes.Count)];
            agent.SetDestination(currentNode.transform.position);
            print("Picked: " + currentNode.name + ", position:" + currentNode.transform.position);
            hasLocation = true;
        }
    }

}
