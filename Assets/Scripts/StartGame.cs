using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour, INetworkRunnerCallbacks
{
    public Button startGameButton;

    void Start()
    {
        startGameButton = startGameButton ?? GetComponent<Button>();
        startGameButton.onClick.AddListener(OnStartGameButtonClick);
    }

    void OnDestroy()
    {
        startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
    }

    private void OnStartGameButtonClick()
    {
        var runner = FindObjectOfType<NetworkRunner>();

        if (runner == null)
        {
            var runnerGameObject = new GameObject("Network Runner");
            runner = runnerGameObject.AddComponent<NetworkRunner>();
            runner.AddCallbacks(this);

            runner.StartGame(new StartGameArgs()
            {
                GameMode = GameMode.AutoHostOrClient,
                OnGameStarted = OnGameStarted,
                SessionName = "TEST",
                Scene = SceneRef.FromIndex(0),
            });
        }
    }

    private void OnGameStarted(NetworkRunner runner)
    {
        Debug.Log($"Game Started - session is {runner.SessionInfo.Name}");
    }

    // Update is called once per frame
    void Update()
    {
        var runner = FindObjectOfType<NetworkRunner>();

        if (runner == null)
        {
            startGameButton.interactable = true;
        }
        else
        {
            startGameButton.interactable = false;
        }
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public async void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        await runner.Shutdown(shutdownReason: ShutdownReason.HostMigration);

        var runnerGameObject = new GameObject("Network Runner (migrated)");
        runner = runnerGameObject.AddComponent<NetworkRunner>();
        runner.AddCallbacks(this);

        await runner.StartGame(new StartGameArgs()
        {
            HostMigrationToken = hostMigrationToken,
            HostMigrationResume = OnHostMigrationResume,
            OnGameStarted = OnGameStarted,
            Scene = SceneRef.FromIndex(0),
        });
    }

    void OnHostMigrationResume(NetworkRunner runner)
    {
        foreach (var (sceneResumeNO, header) in runner.GetResumeSnapshotNetworkSceneObjects())
        {
            Debug.Log($"scene resume object: {sceneResumeNO.gameObject.name}", this);
        }

        foreach (var resumeNO in runner.GetResumeSnapshotNetworkObjects())
        {
            Debug.Log($"Spawning from snapshot: {resumeNO.gameObject.name}", this);
            runner.Spawn(resumeNO, position: resumeNO.transform.position, rotation: resumeNO.transform.rotation, onBeforeSpawned: (runner, newNO) =>
            {
                newNO.CopyStateFrom(resumeNO);
            });
        }
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}
