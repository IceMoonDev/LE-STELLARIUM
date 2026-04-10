using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool isOrange;
    private static float prochainTeleportPossible = 0f;
    public float tempsCooldown = 0.2f;

    void Start()
    {
        // On remet le compteur à zéro à chaque fois qu'on lance le jeu !
        prochainTeleportPossible = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // On garde ton if qui fonctionne !
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            Debug.Log("ÉTAPE 1 : Le joueur a le bon Tag et est entré.");

            // Vérification du cooldown
            if (Time.time < prochainTeleportPossible)
            {
                Debug.Log("ÉTAPE 2 :  BLOQUÉ PAR LE COOLDOWN !");
                return;
            }

            // Recherche du portail
            GameObject destinationPortal;
            if (isOrange == false)
            {
                destinationPortal = GameObject.FindGameObjectWithTag("orange portal");
            }
            else
            {
                destinationPortal = GameObject.FindGameObjectWithTag("blue portal");
            }

            // Résultat de la recherche et téléportation
            if (destinationPortal != null)
            {
                Debug.Log("ÉTAPE 3 :  Portail de destination TROUVÉ ! Téléportation en cours...");

                // On cherche si l'objet a un composant physique (Rigidbody2D)
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    // Si oui, on téléporte le composant physique !
                    rb.position = destinationPortal.transform.position;
                    // Facultatif : on peut aussi stopper sa vitesse pour éviter qu'il ne s'envole en sortant
                    // rb.linearVelocity = Vector2.zero;
                }
                else
                {
                    // Si c'est un objet sans physique, on téléporte le Transform normalement
                    other.transform.position = destinationPortal.transform.position;
                }

                prochainTeleportPossible = Time.time + tempsCooldown;
            }
            {
                Debug.Log("ÉTAPE 3 : ❌ ERREUR - Portail de destination INTROUVABLE. Vérifie que l'autre portail existe et a exactement le tag recherché.");
            }
        }
    }
}
