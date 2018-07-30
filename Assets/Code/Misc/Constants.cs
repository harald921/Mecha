using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Constants
{
    public const string APP_NAME     = "Mecha";
    public const string GAME_VERSION = "0.01";

    public const int    BOOL_SIZE_IN_BITS = 8;

    public static class Terrain
    {
        public const string CHUNK_SAVE_FOLDER    = @"C:\" + APP_NAME;
        public const int    CHUNK_SIZE           = 32;
        public const int    WORLD_SIZE_IN_CHUNKS = 3;
        public const int    WORLD_SIZE_IN_TILES  = CHUNK_SIZE * WORLD_SIZE_IN_CHUNKS;
        public const int    RENDER_DISTANCE      = 3;
    }

    public static class Networking
    {
        public const string PHOTON_CLOUD_APP_ID = "c161aabb-bcec-4782-aa0f-9913dd0e1b58";
        public const bool   AUTO_JOIN_LOBBY     = false;
    }
}
