using UnityEngine;
using UnityEditor;

public class Texture2DArrayWizard : ScriptableWizard
{
    [SerializeField] Texture2D[] _inputTextures;

    [MenuItem("Assets/Create/Texture Array")]
    static void CreateWizard() =>
        DisplayWizard<Texture2DArrayWizard>("Create Texture Array", "Create");


    void OnWizardCreate()
    {
        // If the user didn't input any textures, return
        if (_inputTextures.Length == 0)
            return;

        // Get a path to where the user wants to save the textureArray
        string path = EditorUtility.SaveFilePanelInProject("Save Texture Array", "Texture Array", "asset", "Save Texture Array");

        // Create the textureArray and copy the settings from the first texture
        Texture2D texture = _inputTextures[0];
        Texture2DArray textureArray = new Texture2DArray(texture.width, texture.height, _inputTextures.Length, texture.format, texture.mipmapCount > 1)
        {
            anisoLevel = texture.anisoLevel,
            filterMode = texture.filterMode,
            wrapMode = texture.wrapMode
        };

        // Loop over every texture and insert its data into the textureArray
        for (int i = 0; i < _inputTextures.Length; i++)
            for (int mipLevel = 0; mipLevel < texture.mipmapCount; mipLevel++)
                Graphics.CopyTexture(_inputTextures[i], 0, mipLevel, textureArray, i, mipLevel);

        // Create the textureArray as an asset
        AssetDatabase.CreateAsset(textureArray, path);
    }
}