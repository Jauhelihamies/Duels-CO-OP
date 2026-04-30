using UnityEngine;

public class Arokierija : MonoBehaviour
{
    [Header("Liikeasetukset")]
    public float perusNopeus = 2f;
    public float pakoNopeus = 5f;
    public float suunnanVaihtoVali = 2f;

    [Header("Pelaajan havaitseminen")]
    public float detectionSade = 6f;

    [Header("Visualisointi")]
    public float pyoritysNopeus = 100f; // Kuinka nopeasti pallo pyörii
    public Transform visuaalinenMalli; // Vedä tähän arokierijän 3D-malli

    private Transform pelaaja;
    private Vector3 liikeSuunta;
    private float ajastin;
    private float nykyinenNopeus;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) pelaaja = playerObj.transform;

        // Jos visuaalista mallia ei ole asetettu, käytetään tätä objektia itseään
        if (visuaalinenMalli == null) visuaalinenMalli = transform;

        VaihdaSatunnainenSuunta();
    }

    void Update()
    {
        float etaisyys = pelaaja != null ? Vector3.Distance(transform.position, pelaaja.position) : float.MaxValue;
        Vector3 suuntaNyt;

        if (etaisyys < detectionSade)
        {
            // PAKO
            suuntaNyt = (transform.position - pelaaja.position).normalized;
            suuntaNyt.y = 0;
            nykyinenNopeus = pakoNopeus;
            ajastin = 0;
        }
        else
        {
            // VAPAA LIIKE
            suuntaNyt = liikeSuunta;
            nykyinenNopeus = perusNopeus;

            ajastin += Time.deltaTime;
            if (ajastin >= suunnanVaihtoVali)
            {
                VaihdaSatunnainenSuunta();
                ajastin = 0;
            }
        }

        // 1. Liikutetaan objektia
        transform.Translate(suuntaNyt * nykyinenNopeus * Time.deltaTime, Space.World);

        // 2. Pyöritetään mallia (pyörimisakseli on kohtisuorassa menosuuntaan)
        if (suuntaNyt != Vector3.zero)
        {
            // Lasketaan akseli, jonka ympäri pallo pyörii (ristitulo menosuunnan ja ylöspäin-vektorin välillä)
            Vector3 pyoritysAkseli = Vector3.Cross(Vector3.up, suuntaNyt);
            visuaalinenMalli.Rotate(pyoritysAkseli, pyoritysNopeus * nykyinenNopeus * Time.deltaTime, Space.World);
        }
    }

    void VaihdaSatunnainenSuunta()
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        liikeSuunta = new Vector3(x, 0, z).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionSade);
    }
}