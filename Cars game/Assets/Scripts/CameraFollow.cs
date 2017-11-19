using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject targetToFollow;
    public float heightAbovePlayer;
    public Vector3 positionOffset; // Position offset from the player's position to place the camera (for the gameover animation).
    public Vector3 cinematicRotation; // Rotation of the camera during the gameover animation.
    [Range(0, 1)] public float cinematicDampening; // Time it takes to pan to the next rotation + position, clamped between 0-1

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player.isDead)
        {
            Vector3 targetPos = player.transform.position; // Grab the player's transform
            Vector3 cinematicPos = player.transform.position + positionOffset; // Set specific values.
            transform.position = Vector3.Lerp(transform.position, cinematicPos, cinematicDampening);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cinematicRotation), cinematicDampening);
        }
        else
        {
            Vector3 playerPos = player.transform.position;
            transform.position = new Vector3(playerPos.x, heightAbovePlayer, playerPos.z);
            transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        }
    }
}