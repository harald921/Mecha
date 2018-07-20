using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Constants
{
    public const string APP_NAME = "Inscent";

    public static class Terrain
    {
        public const string CHUNK_SAVE_FOLDER    = @"C:\" + APP_NAME;
        public const int    CHUNK_SIZE           = 32;
        public const int    WORLD_SIZE_IN_CHUNKS = 3;
        public const int    WORLD_SIZE_IN_TILES  = CHUNK_SIZE * WORLD_SIZE_IN_CHUNKS;
        public const int    RENDER_DISTANCE      = 3;
    }
}
