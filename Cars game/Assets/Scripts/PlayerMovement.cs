using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 5f;
    public float turnDamping = 2f;

    [Header("Stamina")]
    public float baseStamina = 100f;
    public float staminaLossPerSecond = 27.5f;
    public float staminaGainPerSecond = 17.5f;
    public float staminaDelayBeforeRecharge = 1.5f;
    public float speedBoostMultiplier = 1.5f;
    public Image staminaBar;

    [Space]

    public Camera playerCam;

    private Player player;
    private bool canPlayAudio = true;
    private float stamina;
    private float staminaDelay;

    void Start ()
    {
        player = GetComponent<Player>();
        stamina = baseStamina;
        staminaDelay = 0;
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
        if (Input.GetKey(KeyCode.LeftShift) &&
            stamina > 0
            )
        {
            movement *= speedBoostMultiplier;
            staminaDelay = staminaDelayBeforeRecharge;
            stamina -= Time.deltaTime * staminaLossPerSecond;
        }
        else
        {
            if (staminaDelay > 0)
            {
                staminaDelay -= Time.deltaTime * staminaDelayBeforeRecharge;
            }
            else
            {
                if (stamina < baseStamina)
                {
                    stamina += Time.deltaTime * staminaGainPerSecond;
                }
            }
        }

        staminaBar.fillAmount = (stamina / baseStamina);

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
