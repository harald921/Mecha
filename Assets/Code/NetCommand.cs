using Lidgren.Network;

public abstract partial class Command : IPackable
{
    public abstract Type      type { get; }


    public void Send()
    {
        NetBuffer writeBuffer = new NetBuffer();

        writeBuffer.WriteVariableInt32((int)type);

        PackInto(writeBuffer);

        NetworkManager.Send(writeBuffer.Data);
    }


    public enum Type
    {
        MoveMech,
        SpawnMech
    }

    public abstract int GetPacketSize();
    public abstract void PackInto(NetBuffer inBuffer);
    public abstract void UnpackFrom(NetBuffer inBuffer);
}
