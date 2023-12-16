using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ObjectiveUpdate : NetworkBehaviour
{
    public NetworkVariable<int> objective1 = new(-1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> objective2 = new(-1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private ObjectiveManager obj;

    public override void OnNetworkSpawn()
    {
        objective1.OnValueChanged += OnStateChanged1;
        objective2.OnValueChanged += OnStateChanged2;
        ObjectiveManager.obj1 += UpdateObjective1ServerRpc;
        ObjectiveManager.obj2 += UpdateObjective2ServerRpc;
    }

    public override void OnNetworkDespawn()
    {
        objective1.OnValueChanged -= OnStateChanged1;
        objective2.OnValueChanged -= OnStateChanged2;
        ObjectiveManager.obj1 -= UpdateObjective1ServerRpc;
        ObjectiveManager.obj2 -= UpdateObjective2ServerRpc;
    }
    private void Start()
    {
        obj = FindFirstObjectByType<ObjectiveManager>();
    }
    public void OnStateChanged1(int previous, int current)
    {
        obj.UpdateObjective(false, current);
    }
    public void OnStateChanged2(int previous, int current)
    {
        obj.UpdateObjective(true, current);
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdateObjective1ServerRpc(int value)
    {
        if (value > objective1.Value) { objective1.Value = value; }
    }
    [ServerRpc(RequireOwnership = false)]
    public void UpdateObjective2ServerRpc(int value)
    {
        if (value > objective2.Value) { objective2.Value = value; }
    }
}