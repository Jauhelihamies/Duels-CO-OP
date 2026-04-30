using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SatunnainenAani : MonoBehaviour
{
    [Header("Asetukset")]
    public List<AudioClip> aanet;         // Lista ‰‰nitiedostoista
    public float minOdotus = 2.0f;       // Lyhin mahdollinen tauko
    public float maxOdotus = 10.0f;      // Pisin mahdollinen tauko

    private AudioSource audioSource;

    void Start()
    {
        // Haetaan AudioSource-komponentti
        audioSource = GetComponent<AudioSource>();

        // Aloitetaan toistokierre
        if (aanet.Count > 0)
        {
            StartCoroutine(SoitaAania());
        }
    }

    IEnumerator SoitaAania()
    {
        while (true)
        {
            // Odotetaan satunnainen aika ennen seuraavaa ‰‰nt‰
            float odotusaika = Random.Range(minOdotus, maxOdotus);
            yield return new WaitForSeconds(odotusaika);

            // Valitaan satunnainen ‰‰ni listasta
            int indeksi = Random.Range(0, aanet.Count);
            AudioClip valittuAani = aanet[indeksi];

            // Soitetaan ‰‰ni kerran
            audioSource.PlayOneShot(valittuAani);
        }
    }
}