using System;
using System.Collections;
using System.Collections.Generic;
using Lidgren.Network;

public static class SystemGuidExtensions
{
	const int GUID_NUM_BYTES = 16;

    public static void PackInto(this Guid inGuid, NetBuffer inBuffer)
    {
        byte[] guidBytes = inGuid.ToByteArray();

        inBuffer.Write(guidBytes, 0, GUID_NUM_BYTES);
    }

	public static void UnpackFrom(this Guid inGuid, NetBuffer inBuffer, ref Guid inDestination)
    {
        byte[] guidBytes = inBuffer.ReadBytes(GUID_NUM_BYTES);

        inDestination = new Guid(guidBytes);
    }
}
