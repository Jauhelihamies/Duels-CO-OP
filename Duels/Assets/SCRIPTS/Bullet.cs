using UnityEngine;

public class Bullet : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        // Tarkistetaan osuuko ammus pelaajaan tagin perusteella

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject, 2f);
        }
    }
}
