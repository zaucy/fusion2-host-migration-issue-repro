using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkCamera : NetworkBehaviour
{
    public override void Spawned()
    {
        base.Spawned();
        Debug.Log("NetworkCamera Spawned", this);
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        base.Despawned(runner, hasState);
        Debug.Log($"NetworkCamera Despawned hasState={hasState}", this);
    }
}
