using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumerationExtensions
{
    public static void Add<T>(this Enum type, T value, ref T inDestination)
    {
        try
        {
            inDestination = (T)(object)(((int)(object)type | (int)(object)value));
        }

        catch (Exception ex)
        {
            throw new ArgumentException(
                string.Format(
                    "Could not append value from enumerated type '{0}'.",
                    typeof(T).Name
                    ), ex);
        }
    }

    public static void Remove<T>(this Enum type, T value, ref T inDestination)
    {
        try
        {
            inDestination = (T)(object)(((int)(object)type & ~(int)(object)value));
        }

        catch (Exception ex)
        {
            throw new ArgumentException(
                string.Format(
                    "Could not remove value from enumerated type '{0}'.",
                    typeof(T).Name
                    ), ex);
        }
    }
}