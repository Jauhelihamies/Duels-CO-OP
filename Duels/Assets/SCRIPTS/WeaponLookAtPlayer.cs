using UnityEngine;

public class WeaponLookAtPlayer : MonoBehaviour
{
    private Transform player;

    [Header("Asetukset")]
    public string playerTag = "Player";
    public float rotationSpeed = 5f; // Kuinka nopeasti ase k‰‰ntyy
    public bool lockVertical = false; // Jos haluat, ett‰ ase ei nouse/laske pystysuunnassa

    void Start()
    {
        // Etsit‰‰n pelaaja
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Lasketaan suunta pelaajaan
            Vector3 targetDirection =  transform.position - player.position;

            // Jos haluat aseen pysyv‰n vaakatasossa, nollataan Y-akselin ero
            if (lockVertical)
            {
                targetDirection.y = 0;
            }

            // Luodaan rotaatio kohti pelaajaa
            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                // K‰‰nnet‰‰n ase sulavasti kohti kohdetta
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}