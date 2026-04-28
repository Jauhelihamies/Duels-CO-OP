using UnityEngine;

public class EdestakaisinLiike : MonoBehaviour
{
    [Header("Asetukset")]
    public float nopeus = 2.0f;  // Kuinka nopeasti liikkuu
    public float matka = 3.0f;   // Kuinka kauas alkupisteest‰ liikkuu

    private Vector3 alkupiste;

    void Start()
    {
        // Tallennetaan paikka, josta liike alkaa
        alkupiste = transform.position;
    }

    void Update()
    {
        // Lasketaan uusi paikka k‰ytt‰m‰ll‰ Mathf.PingPong-funktiota
        // Se palauttaa arvon nollan ja matkan v‰lill‰, joka kasvaa ja laskee jatkuvasti
        float liike = Mathf.PingPong(Time.time * nopeus, matka);

        // P‰ivitet‰‰n objektin paikka (t‰ss‰ esimerkiss‰ X-akselilla)
        transform.position = alkupiste + new Vector3(0,0, liike);
    }
}