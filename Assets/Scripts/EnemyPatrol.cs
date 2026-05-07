
using System.Threading;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;  //hier ziehen wir Wegpunkt A hinein
    public Transform pointB;  //hier Wegpunkt B
    public float speed = 2f;

    private Transform ziel; //hier merkt sich der Gegner, 
                            //wo er gerade hinläuft

    void Start()
    {
        //Am Anfang soll er in Richtung Wegpunkt B starten
        ziel = pointB;
    }

    void Update()
    {
        //1. Bewege den Gegner Schritt für Schritt in 
        //   Richtung Ziel
        transform.position = Vector2.MoveTowards(transform.position,
                             ziel.position,speed * Time.deltaTime);

        //2. Prüfung: Sind wir am Ziel angekommen? (Abstand < als 0.1)
        if(Vector2.Distance(transform.position, ziel.position) < 0.1f)
        {
            //Wenn wir bei PunktA sind, wechsle Ziel auf PunktB
            if(ziel == pointA)
            {
                ziel = pointB;
            } //ansonsten wechsle auf PunktA
            else
            {
                ziel = pointA;
            }
        }

    }//end Update

}//end class
