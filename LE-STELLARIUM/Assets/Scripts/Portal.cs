using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool isOrange;

    // Le mot-clé "static" signifie que cette variable est partagée par TOUS les portails.
    private static float prochainTeleportPossible = 0f;

    // Le temps d'attente avant de pouvoir reprendre un portail (en secondes)
    public float tempsCooldown = 0.2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. On vérifie si le cooldown est terminé.
        // Si ce n'est pas le cas, on arrête tout de suite la fonction.
        if (Time.time < prochainTeleportPossible)
        {
            return;
        }

        // 2. Trouver le portail de destination
        GameObject destinationPortal;

        if (isOrange == false) // Si c'est le bleu
        {
            destinationPortal = GameObject.FindGameObjectWithTag("orange portal");
        }
        else // Si c'est le orange
        {
            destinationPortal = GameObject.FindGameObjectWithTag("blue portal");
        }

        // 3. Téléporter et activer le cooldown
        if (destinationPortal != null)
        {
            // On téléporte le joueur
            other.transform.position = destinationPortal.transform.position;

            // On met à jour l'horloge : la prochaine téléportation ne sera possible
            // que dans 0.2 secondes, pour TOUS les portails.
            prochainTeleportPossible = Time.time + tempsCooldown;
        }
    }
}
