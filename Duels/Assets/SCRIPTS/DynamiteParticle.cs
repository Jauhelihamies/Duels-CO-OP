using UnityEngine;
using System.Collections;

public class DynamiteEnemy : MonoBehaviour
{
    [Header("References")]
    public GameObject dynamitePrefab;
    public Transform throwPoint;
    public GameObject particleEffectPrefab;

    [Header("Settings")]
    public float throwForce = 20f;       // Increased for better distance
    public float upwardArc = 5f;        // Manual upward boost
    public float throwInterval = 3f;
    public float explosionRadius = 5f;
    public float damageAmount = 20f;
    public float fuseTime = 2f;

    private GameObject currentDynamite;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        StartCoroutine(ThrowCycle());
    }

    IEnumerator ThrowCycle()
    {
        while (true)
        {
            SpawnDynamiteInHand();
            yield return new WaitForSeconds(throwInterval);
            ThrowDynamite();
            yield return new WaitForSeconds(fuseTime + 1f);
        }
    }

    void SpawnDynamiteInHand()
    {
        currentDynamite = Instantiate(dynamitePrefab, throwPoint.position, throwPoint.rotation);
        currentDynamite.transform.SetParent(throwPoint);

        Rigidbody rb = currentDynamite.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        // FIX 1: Make sure it doesn't hit the enemy that threw it
        Collider enemyCol = GetComponent<Collider>();
        Collider dynCol = currentDynamite.GetComponent<Collider>();
        if (enemyCol && dynCol) Physics.IgnoreCollision(enemyCol, dynCol);
    }

    void ThrowDynamite()
    {
        if (currentDynamite == null) return;

        currentDynamite.transform.SetParent(null);
        Rigidbody rb = currentDynamite.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            if (player != null)
            {
                // FIX 2: Calculate a clean trajectory
                Vector3 targetDir = (player.position - throwPoint.position).normalized;

                // FIX 3: Use VelocityChange to bypass mass issues
                rb.AddForce(targetDir * throwForce + Vector3.up * upwardArc, ForceMode.VelocityChange);

                // Add some cool spinning
                rb.AddTorque(Random.insideUnitSphere * 10f, ForceMode.VelocityChange);
            }
        }

        StartCoroutine(ExplodeSequence(currentDynamite));
    }

    IEnumerator ExplodeSequence(GameObject dynamite)
    {
        yield return new WaitForSeconds(fuseTime);

        if (dynamite != null)
        {
            Vector3 explosionPos = dynamite.transform.position;

            if (particleEffectPrefab != null)
            {
                Instantiate(particleEffectPrefab, explosionPos, Quaternion.identity);
            }

            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            foreach (Collider hit in colliders)
            {
                if (hit.CompareTag("Player"))
                {
                    hit.SendMessage("TakeDamage", damageAmount, SendMessageOptions.DontRequireReceiver);
                }
            }

            Destroy(dynamite);
        }
    }
}