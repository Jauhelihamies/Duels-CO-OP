using UnityEngine;
using UnityEngine.AI;

public class AU : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private NavMeshAgent agent;
    private Transform player;

    [Header("Asetukset")]
    public string playerTag = "Player";
    public float updateRate = 0.2f; // Kuinka usein reitti p‰ivitet‰‰n (s‰‰st‰‰ suoritustehoa)

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Etsit‰‰n pelaaja skenen alussa tagin perusteella
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Aloitetaan reitin p‰ivitys toistuvasti
        InvokeRepeating("UpdatePath", 0f, updateRate);
    }

    void UpdatePath()
    {
        if (player != null && agent.enabled)
        {
            // Asetetaan NavMeshAgentin kohteeksi pelaajan sijainti
            agent.SetDestination(player.position);
        }
    }
}

