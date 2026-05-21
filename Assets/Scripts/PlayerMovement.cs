using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb; // Neue Variable für Physik

    public TextMeshProUGUI scoreText;
    private int score = 0;

    //neue Variablen für den Sensor 
    public Transform groundCheck;  //Hier ziehen wir später das Fuß-Objekt hinein
    public float groundCheckRadius = 0.2f;   //Größe des Sensorkreises (Fuß)
    public LayerMask groundLayer;  //Hier stellen wir den Layer "Ground" ein
    public bool isGrounded;

    //neue Variablen für die Verwaltung der Leben
    public TextMeshProUGUI livesText;  //hier ziehen wir den neuen Text hinein
    public int lives = 3;  //beim Start 3 Leben vergeben



    void Start() {
        // Wir holen uns die Verbindung zum Rigidbody
        rb = GetComponent<Rigidbody2D>();

        //Anzeige beim Start direkt auf 3 Leben setzen
        livesText.text = "Lives: " + lives;

    }

    void Update() {
        float moveX = Input.GetAxis("Horizontal");

        //1. Der Sensor prüft jeden Frame, ob er den Boden berührt.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        

        // Alte Zeile mit Translate LÖSCHEN!
        // Neue Physik-Bewegung: Wir setzen die Geschwindigkeit direkt
        // Wir behalten die aktuelle y-Geschwindigkeit (fallen/springen) bei
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        //2. Springen: Nur erlauben, wenn die Leertaste gedrückt wird UND
        //             der Sensor am Boden ist.
        if (Input.GetButtonDown("Jump") && isGrounded == true) {
             //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Wenn das andere Objekt den Tag "Coin" hat...
        if (other.CompareTag("Coin")) {
            score++; // Zähler hoch
            scoreText.text = "Coins: " + score; // Text ändern
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Goal")) {
            // Lade das nächste Level
            SceneManager.LoadScene(SceneManager.GetActiveScene()
                                                 .buildIndex + 1);
        }
        if (other.CompareTag("Respawn"))
        {
            // Lade die aktuelle Szene neu
            SceneManager.LoadScene(SceneManager.GetActiveScene()
                                                 .buildIndex);
        }

    }

    //Neue Methode: wird aufgerufen, wenn der Spieler gegen etwas Massives
    //prallt.
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Wenn das berührte Objekt das Tag Enemy hat ...
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives--;  //ein Leben abziehen
            livesText.text = "Lives: " + lives;  //Text aktualisieren

            //Wenn die Leben auf 0 fallen
            if (lives <= 0)
            {
                //Game Over bzw. lade die Szene neu
                SceneManager.LoadScene(SceneManager.GetActiveScene()
                                                 .buildIndex);
            }
        }
    }


}
