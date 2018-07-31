using Lidgren.Network;

public interface IPackable
{
    int GetPacketSize();
    void PackInto(NetBuffer inBuffer);
    void UnpackFrom(NetBuffer inBuffer);
}