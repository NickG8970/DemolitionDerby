using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float turnDamping = 2f;
    public float speedBoostMultiplier = 1.5f;

    private Camera playerCam;
    private Player player;
    private bool canPlayAudio = true;

    void Start ()
    {
        playerCam = GetComponentInChildren<Camera>();
        player = GetComponent<Player>();
	}

	void Update ()
    {
        if (player.isDead) return;

        MovePlayer();
        LookAtMouse();
    }

    void LookAtMouse()
    {
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnDamping * Time.deltaTime);
        }
    }

    void MovePlayer()
    {
        var horizontal = Input.GetAxis("Horizontal") * 0.5f; // A + D keys
        var vertical = Input.GetAxis("Vertical"); // W + S keys
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= speedBoostMultiplier;
        }

        transform.Translate(movement);

        if (horizontal != 0 || vertical != 0)
        {
            if (AudioManager.instance.CheckIsPlaying("JeepDriving")) return;

            AudioManager.instance.StopSound("JeepIdle");
            AudioManager.instance.PlaySound("JeepDriving");
        }
        else
        {
            if (AudioManager.instance.CheckIsPlaying("JeepIdle")) return;

            AudioManager.instance.StopSound("JeepDriving");
            AudioManager.instance.PlaySound("JeepIdle");
        }   
    }
}
