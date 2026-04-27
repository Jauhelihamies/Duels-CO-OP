using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    [Header("Asetukset")]
    public Transform[] bulletSpawnPoints; // Ved� t�h�n 3 tyhj�� GameObjectia (piippua)
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;

    [Header("Ajoitus")]
    public float minWaitTime = 1.5f;
    public float maxWaitTime = 4.0f;

    void Start()
    {
        // Aloitetaan jatkuva ammunta-looppi
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            // Odotetaan satunnainen aika ennen seuraavaa sarjaa
            float randomWait = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(randomWait);

            Shoot();
        }
    }

    void Shoot()
    {
        // K�yd��n l�pi kaikki kolme pistett�
        foreach (Transform spawnPoint in bulletSpawnPoints)
        {
            if (spawnPoint != null && bulletPrefab != null)
            {
                // Luodaan ammus ja annetaan sille vauhtia piipun suuntaan
                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = spawnPoint.forward * bulletSpeed;
                }
            }
        }
    }

}
