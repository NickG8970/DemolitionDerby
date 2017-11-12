using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonNetworkManager : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
        Connect();
	}
	
	public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("1.0.0");
    }

    public void PlayOnline()
    {
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 10,
            PublishUserId = true,
            IsOpen = true,
            IsVisible = true
        };
        PhotonNetwork.JoinOrCreateRoom("nan", roomOptions, TypedLobby.Default);
    }

    void OnConnectedToMaster()
    {
        Debug.Log("Connected to my nan");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined nan!");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined nun" + PhotonNetwork.room.Name);
        PhotonNetwork.LoadLevel("Mission02");
    }
}
