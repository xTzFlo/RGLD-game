using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Launcher : MonoBehaviourPunCallbacks
{

	public static Launcher Instance;

	[SerializeField] TMP_InputField roomNameInputField;
	[SerializeField] TMP_InputField NickName;
	[SerializeField] TMP_Text errorText;
	[SerializeField] TMP_Text RoomNameText;
	[SerializeField] Transform roomListContent;
	[SerializeField] GameObject roomListItemPrefab;
	[SerializeField] Transform PlayerListContent;
	[SerializeField] GameObject PlayerListItemPrefab;
	[SerializeField] GameObject startGameButton;
	[SerializeField] GameObject settingsWindows;
	[SerializeField] TMP_Text Perso_Name;
	[SerializeField] private TMP_Dropdown Personnages;
	public bool SinglePlayer = false;

	public static string perso = "chevalier";

	private void Awake()
	{
		Instance = this;
	}

	public void connection()
	{
		MenuManager.Instance.OpenMenu("loading");
		Debug.Log("Connecting to Master");
		PhotonNetwork.ConnectUsingSettings();
		perso = "chevalier";
	}

	List<string> personnage = new List<string>();
	public void Start()
	{
		Personnages.ClearOptions();
		personnage.Add("chevalier");
		personnage.Add("mage");
		Personnages.AddOptions(personnage);
	}

	public void chooseperso(int personageIndex)
	{
		perso = personnage[personageIndex];
	}

	public void singleplayer() // lance la partie solo du jeux
	{
		SinglePlayer = true;
		PhotonNetwork.OfflineMode = true;

	}

	public void SettingsButton()
	{
		settingsWindows.SetActive(true);
	}

	public void CloseSettingsWindows()
	{
		settingsWindows.SetActive(false);
	}

	public override void OnConnectedToMaster() // se connecte a photon
	{
		Debug.Log("Connected to Master");
		if (SinglePlayer)
		{
			Perso_Name.text = perso;
			MenuManager.Instance.OpenMenu("title_menu_solo");
		}
		else
		{
			PhotonNetwork.JoinLobby();
			PhotonNetwork.AutomaticallySyncScene = true;
		}
		
	}

	public void Choose_chevalier()
	{
		perso = "chevalier";
		Perso_Name.text = perso;
	}

	public void Choose_Mage()
	{
		perso = "mage";
		Perso_Name.text = perso;
	}
	public override void OnJoinedLobby()
	{
		MenuManager.Instance.OpenMenu("title");
		Debug.Log("Joined Lobby");
		if (NickName.text == "")
			PhotonNetwork.NickName = "Player " + new Random().Next(0, 1000).ToString("0000");
		else
			PhotonNetwork.NickName = NickName.text;
		
	}
	
	

	public void CreateRoom()
	{
		if (SinglePlayer)
		{
			string name = new Random().Next(0, 1000).ToString("0000");
			PhotonNetwork.CreateRoom(name);
			MenuManager.Instance.OpenMenu("loading");
		}
		else
		{
			if (string.IsNullOrEmpty(roomNameInputField.text))
			{
				return;
			}

			PhotonNetwork.CreateRoom(roomNameInputField.text);
			MenuManager.Instance.OpenMenu("loading");
		}
	}

	public override void OnJoinedRoom()
	{
		if (SinglePlayer)
		{
			PhotonNetwork.CurrentRoom.IsOpen = false;
			PhotonNetwork.CurrentRoom.IsVisible = false;
			StartGame();
		}
		else
		{
			MenuManager.Instance.OpenMenu("room");
			RoomNameText.text = PhotonNetwork.CurrentRoom.Name;

			Player[] players = PhotonNetwork.PlayerList;

			foreach (Transform child in PlayerListContent)
			{
				Destroy(child.gameObject);
			}

			for (int i = 0; i < players.Count(); i++)
			{
				Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
			}

			startGameButton.SetActive(PhotonNetwork.IsMasterClient);
		}
	}


	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room Creation Failed: " + message;
		MenuManager.Instance.OpenMenu("error");
	}

	public void StartGame()
	{
		PhotonNetwork.LoadLevel(1);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		MenuManager.Instance.OpenMenu("loading");

	}

	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
		MenuManager.Instance.OpenMenu("loading");
	}

	public override void OnLeftRoom()
	{
		MenuManager.Instance.OpenMenu("title");
	}

	public void LeaveLobby()
	{
		PhotonNetwork.Disconnect();
		MenuManager.Instance.OpenMenu("menu_principal");
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach (Transform trans in roomListContent)
		{
			Destroy(trans.gameObject);
		}
		for (int i = 0; i < roomList.Count; i++)
		{
			if(roomList[i].RemovedFromList)
				continue;
			Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
		}
	}


	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
	}
}