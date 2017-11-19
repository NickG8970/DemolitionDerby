using UnityEngine;

public class NetworkPlayerSetup : MonoBehaviour
{
    [SerializeField] private Transform playerCamTransform;
    [SerializeField] private Behaviour[] componentsToDisable;
    
    private PhotonView photonView;

    void Start()
    {
        if (PhotonNetwork.offlineMode) return;
        photonView = GetComponent<PhotonView>();
        Initialize();

    }

    void Initialize()
    {
        if (!photonView.isMine)
        {
            playerCamTransform.gameObject.SetActive(false);

            foreach (Behaviour b in componentsToDisable)
            {
                b.enabled = false;
            }
        }
    }
}