using System.Runtime.Serialization;
using System.Xml.Serialization;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    //private InputAction mouvement_input;
    [SerializeField] GameObject player_tictac;
    [SerializeField] Transform player_spawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player_tictac = Instantiate(player_tictac, player_spawn.position, player_spawn.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
