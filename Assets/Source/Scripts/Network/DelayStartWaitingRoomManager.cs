using Photon.Pun;
using Photon.Realtime;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PhotonView = Photon.Pun.PhotonView;

public class DelayStartWaitingRoomManager : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;

    public string multiplayerSceneName;
    public string menuSceneName;
    public int minPlayersToStart;

    public Text roomCountText;
    public Text timerToStartText; // Don't think imma keep it thought.

    private int playerCount;
    private byte roomSize;

    private bool readyToCountDown = false;
    private bool readyToStart = false;
    private bool startingGame = false;

    private float timerToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;

    public float maxWaitTime;
    public float maxFullGameWaitTime;

    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameWaitTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;

        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;

        PlayerCountUpdate();
    }

    private void Update()
    {
        WaitingForMorePlayers();
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        PlayerCountUpdate();

        // Send master clients countdown timer to all other players in order to sync time.
        if (PhotonNetwork.IsMasterClient)
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
    }

    public override void OnPlayerLeftRoom(Player player) => PlayerCountUpdate();

    private void WaitingForMorePlayers()
    {
        if (playerCount <= 1)
        {
            ResetTimer();
        }

        if (readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }
        else if (readyToCountDown)
        {
            notFullGameTimer -= Time.deltaTime;
            timerToStartGame = notFullGameTimer;
        }

        string tempTimer = $"{timerToStartGame:00}";
        timerToStartText.text = tempTimer;

        // When timer is done : start the game.
        if (timerToStartGame <= 0f)
        {
            if (startingGame) return;
            StartGame();
        }
    }

    /// <summary>
    ///     Loads the gameplay Scene.
    /// </summary>
    private void StartGame()
    {
        startingGame = true;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            // Load la scene de multipalyer gameplay.
            PhotonNetwork.LoadLevel(multiplayerSceneName);
        }
    }

    /// <summary>
    ///     Resets timer.
    /// </summary>
    private void ResetTimer()
    {
        timerToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameWaitTime;
    }

    /// <summary>
    ///     Leaves the current lobby and goes back to menuScene.
    /// </summary>
    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneName);
    }

    /// <summary>
    ///     Updates player count when players join the room.
    ///     Displays player count.
    ///     Triggers countdown timer.
    /// </summary>
    private void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomCountText.text = playerCount + " / " + roomSize;

        if (playerCount == roomSize)
            readyToStart = true;
        else if (playerCount >= minPlayersToStart)
            readyToCountDown = true;
    }

    /// <summary>
    ///     RPC for syncing the countdown timer to that join after it has started the countdown.
    /// </summary>
    /// <param name="timeIn"></param>
    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if (timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }
}
