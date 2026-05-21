using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;  //Zeit in Sekunden, bis Plattform fällt
    public float destroyDelay = 2f; //Zeit bis die Plattform gelöscht wird

    private Rigidbody2D rb;

    void Start()
    {
        // Wir holen uns die Physik-Komponente der Plattform
        rb = GetComponent<Rigidbody2D>();
    }

    //wenn die Plattform berührt wird, dann ...
    void OnCollisionEnter2D(Collision2D collision)
    {
        //prüfen, ob der Spieler die Plattform berührt
        if (collision.gameObject.CompareTag("Player"))
        {
            //"Wecker" setzen, der die Funktion "DropPlatform" nach "fallDelay"-Sekunden aufruft
            Invoke("DropPlatform", fallDelay);

            //lösche die Plattform nach einer Weile, damit sie nicht endlos ins Nichts fällt
            Destroy(gameObject, fallDelay + destroyDelay); 
        }
    }

    //Funktion wird vom "Wecker" aufgerufen
    void DropPlatform()
    {
        //Schwerkraft aktivieren (Kinematic in Dynamic ändern)
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    
}
