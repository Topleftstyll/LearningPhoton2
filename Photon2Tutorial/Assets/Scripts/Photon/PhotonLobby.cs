using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks {
    public static PhotonLobby lobby;
    public GameObject battleButton;
    public GameObject cancelButton;

    void Awake() {
        lobby = this;
    }

    void Start() {
        PhotonNetwork.ConnectUsingSettings(); // connects to master photon server
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Player has connected to the photon master server");
        battleButton.SetActive(true);
    }

    public void OnBattleButtonClicked() {
        Debug.Log("OnBattleButton Clicked");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log("Tried to join random game but failed. there must be no open games available");
        CreateRoom();
    }

    void CreateRoom() {
        Debug.Log("Trying to create new Room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        //roomOps.CustomRoomProperties.Add("Password", "1234"); // possibly how to add a password to a lobby
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("Tried to create new room but failed, must be a room with the same name");
        CreateRoom();
    }

    public override void OnJoinedRoom(){
        Debug.Log("We are now in a room");
    }

    public void OnCancelButtonClicked() {
        Debug.Log("Cancel Button clicked");
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
