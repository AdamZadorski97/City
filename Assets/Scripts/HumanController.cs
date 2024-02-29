using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        // Ensure the NavMeshAgent component is attached and enabled.
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }

        // Attempt to place the agent on the NavMesh.
        PlaceAgentOnNavMesh();
    }

    private void PlaceAgentOnNavMesh()
    {
        // Find the closest point on the NavMesh to the agent and place the agent there.
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 100f, NavMesh.AllAreas))
        {
            navMeshAgent.enabled = false; // Temporarily disable the agent to move it.
            transform.position = hit.position;
            navMeshAgent.enabled = true; // Re-enable the agent.
        }
        else
        {
            Debug.LogWarning("Could not find a position on the NavMesh!");
        }
    }

    public void GoTo(Vector3 position)
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(position);
        }
        else
        {
            Debug.LogError("NavMeshAgent is not active and enabled.");
        }
    }
}