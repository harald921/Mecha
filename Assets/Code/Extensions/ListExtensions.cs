using System.Collections.Generic;

public static class ListExtensions 
{
    public static void AddIfNotNull<T>(this List<T> inList, T inItemToAdd)
    {
        if (inItemToAdd != null)
            inList.Add(inItemToAdd); 
    }
}