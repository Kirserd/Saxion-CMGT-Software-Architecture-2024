using UnityEngine;

public abstract class Visualisator<DataPacket> : MonoBehaviour
{
    protected DataPacket Data;
    public void UpdateData(DataPacket data) => Data = data;
    public abstract void DisplayData();

    public abstract void Open();
    public abstract void Hide();

    public void Return() => GameStateMachine.Return();
    public void ReturnToRoot() => GameStateMachine.Next<MapOverviewState>();
}
