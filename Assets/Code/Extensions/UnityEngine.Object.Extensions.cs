public static class UnityEngineObjectExtensions
{
    public static void Destroy(this UnityEngine.Object inObjectToDestroy) => 
        UnityEngine.Object.Destroy(inObjectToDestroy);
}
