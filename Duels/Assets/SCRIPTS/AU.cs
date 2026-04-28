using UnityEngine;
using UnityEngine.AI;

public class AU : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [Header("Asetukset")]
    public string playerTag = "Player";
    public float updateRate = 0.2f;

    [Header("Liikkuminen")]
    [Tooltip("M‰‰ritt‰‰ agentin liikkumisnopeuden.")]
    public float speed = 3.5f; // Uusi muuttuja nopeudelle

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Asetetaan nopeus agentille heti alussa
        if (agent != null)
        {
            agent.speed = speed;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        InvokeRepeating("UpdatePath", 0f, updateRate);
    }

    // P‰ivitet‰‰n nopeus myˆs pelin aikana, jos muutat sit‰ Inspectorissa
    void Update()
    {
        if (agent != null && agent.speed != speed)
        {
            agent.speed = speed;
        }
    }

    void UpdatePath()
    {
        if (player != null && agent.enabled)
        {
            agent.SetDestination(player.position);
        }
    }
}
