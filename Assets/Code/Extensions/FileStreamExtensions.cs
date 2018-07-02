using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileStreamExtensions 
{
    public static FileStream LoadAndWaitUntilLoaded(string inPath, FileMode inMode, FileAccess inAccess = FileAccess.Read, FileShare inShare = FileShare.Read)
    {
        for (int i = 0; i < 10; i++)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(inPath, inMode, inAccess, inShare);
                return stream;
            }

            catch (IOException)
            {
                if (stream != null)
                    stream.Dispose();

                System.Threading.Thread.Sleep(50);
            }
        }

        Debug.LogError("Couldn't read file " + inPath);

        return null;
    }
}