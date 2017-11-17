using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonNetworkManager : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("1.0.0");
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Multiplayer")
        {
            PhotonNetwork.Instantiate("PlayerCar", new Vector3(0, 10, 0), Quaternion.identity, 0);
        }
    }

    public void PlayOnline()
    {
        Connect();
        
    }

    void OnConnectedToMaster()
    {
        Debug.Log("Connected to my server");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined server!");
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 10,
            PublishUserId = true,
            IsOpen = true,
            IsVisible = true
        };

        PhotonNetwork.JoinOrCreateRoom("server", roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined game" + PhotonNetwork.room.Name);
        PhotonNetwork.LoadLevel("Multiplayer");
    }
}
