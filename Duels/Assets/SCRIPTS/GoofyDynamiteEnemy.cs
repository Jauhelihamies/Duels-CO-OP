using UnityEngine;
using UnityEngine.AI;

public class GoofyDynamiteEnemy : MonoBehaviour
{
    [Header("Setup")]
    public Transform player;
    public GameObject dynamitePrefab;
    public Transform throwPoint;

    [Header("Stats")]
    public float throwRange = 12f;
    public float throwForceUp = 6f;
    public float throwForceForward = 12f;

    private NavMeshAgent agent;
    private bool hasThrown = false; // Tracks if he already used his one dynamite

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // If he already threw his dynamite, stop the script logic
        if (hasThrown || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Face the player
        Vector3 lookDirection = (player.position - transform.position).normalized;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 8f);
        }

        if (distance <= throwRange)
        {
            agent.isStopped = true;
            ThrowAndRetire();
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    void ThrowAndRetire()
    {
        hasThrown = true; // Mark as done so he never throws again

        GameObject dynamite = Instantiate(dynamitePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = dynamite.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 force = transform.forward * throwForceForward + transform.up * throwForceUp;
            rb.AddForce(force, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * 15f, ForceMode.Impulse);
        }

        // Optional: Make the agent start chasing again or just stand there
        agent.isStopped = false;

        // This disables the script so it stops calculating distance/logic entirely
        this.enabled = false;
    }
}