// From the Fusion 2 Tutorial: https://doc.photonengine.com/fusion/current/tutorials/host-mode-basics/2-setting-up-a-scene#launching-fusion
using UnityEngine;
using Fusion;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// This class launches Fusion NetworkRunner, and also spanws a new avatar whenever a player joins.
public class SpawningLauncher : EmptyLauncher
{
    [SerializeField] NetworkPrefabRef _playerPrefab;
    [SerializeField] Transform[] spawnPoints;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public override void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player {player} joined");
        bool isAllowedToSpawn = (runner.GameMode == GameMode.Shared) ?
            (player == runner.LocalPlayer) :   // in Shared mode, the local player is allowed to spawn.
            runner.IsServer;                  // in Host or Server mode, only the server is allowed to spawn.
        if (isAllowedToSpawn)
        {
            //SceneManager.LoadScene(nextScene); 
            // Create a unique position for the player
            //Vector3 spawnPosition = spawnPoints[player.AsIndex % spawnPoints.Length].position;
            //Vector3 spawnPosition2 = new Vector3((float)6.86, (float)-1, player.AsIndex);
            Vector3 spawnPosition2 = new Vector3((float)6.18 - ((player.AsIndex - 1) * 14), (float)-1, player.AsIndex);
            //new Vector3((player.RawEncoded % runner.Config.Simulation.PlayerCount) * 3, 0, 0);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition2, Quaternion.identity, /*input authority:*/ player);
            // Keep track of the player avatars for easy access
            _spawnedCharacters.Add(player, networkPlayerObject);
        }
    }

    public override void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player {player} left");
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }

    [SerializeField] InputAction moveAction = new InputAction(type: InputActionType.Button);
    private void OnEnable() { moveAction.Enable(); }
    private void OnDisable() { moveAction.Disable(); }
    void OnValidate()
    {
        // Provide default bindings for the input actions. Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (moveAction.bindings.Count == 0)
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Right", "<Keyboard>/rightArrow");
    }

    NetworkInputData inputData = new NetworkInputData();

    private void Update()
    {
    }

    public override void OnInput(NetworkRunner runner, NetworkInput input)
    {
        inputData.moveActionValue = moveAction.ReadValue<Vector2>();
        input.Set(inputData);    // pass inputData by value 
    }
}