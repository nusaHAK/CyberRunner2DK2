using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb; // Neue Variable für Physik

    public TextMeshProUGUI scoreText;
    private int score = 0;  

    void Start() {
        // Wir holen uns die Verbindung zum Rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        
        // Alte Zeile mit Translate LÖSCHEN!
        // Neue Physik-Bewegung: Wir setzen die Geschwindigkeit direkt
        // Wir behalten die aktuelle y-Geschwindigkeit (fallen/springen) bei
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        // Springen mit Leertaste
        if (Input.GetButtonDown("Jump")) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
            // Lade die aktuelle Szene neu
            SceneManager.LoadScene(SceneManager.GetActiveScene()
                                                 .buildIndex);
        }

    }

}
