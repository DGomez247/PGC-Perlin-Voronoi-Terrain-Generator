
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PGCTerrain))]
[CanEditMultipleObjects]

public class PGCTerrainEditor : Editor
{
   
    private SerializedProperty heightMapScale;
    private SerializedProperty perlinOffsetX;
    private SerializedProperty perlinOffsetY;
    private SerializedProperty perlinXScale;
    private SerializedProperty perlinYScale;
    private SerializedProperty perlinPersistance;
    private SerializedProperty perlinHeightScale;
    private SerializedProperty perlinOctaves;
    private SerializedProperty randomHeight;


    private bool PerlinBool = false;
    private bool MultiPerlinBool;
    private bool showRandom = false;

    SerializedProperty perlinParameters;
    private void OnEnable()
    {

        heightMapScale = serializedObject.FindProperty("heightMapScale");
        perlinOffsetX = serializedObject.FindProperty("perlinOffsetX");
        perlinOffsetY = serializedObject.FindProperty("perlinOffsetY");
        perlinXScale = serializedObject.FindProperty("perlinXScale");
        perlinYScale = serializedObject.FindProperty("perlinYScale");
        perlinPersistance = serializedObject.FindProperty("perlinPersistance");
        perlinHeightScale = serializedObject.FindProperty("perlinHeightScale");
        perlinOctaves = serializedObject.FindProperty("perlinOctaves");
        randomHeight = serializedObject.FindProperty("randomHeight");
        perlinParameters = serializedObject.FindProperty("perlinParameters");
        



    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PGCTerrain terrain = (PGCTerrain)target;

        


        PerlinBool = EditorGUILayout.Foldout(PerlinBool, "Single Perlin");
        if (PerlinBool)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Perlin Noise", EditorStyles.boldLabel);
            EditorGUILayout.IntSlider(perlinOffsetX, 0, 100, new GUIContent("X Offset"));
            EditorGUILayout.IntSlider(perlinOffsetY, 0, 100, new GUIContent("Y Offset"));
            EditorGUILayout.Slider(perlinXScale, 0, 1, new GUIContent("X Scale"));
            EditorGUILayout.Slider(perlinYScale, 0, 1, new GUIContent("Y Scale"));
        
            if (GUILayout.Button("Perlin"))
            {
                terrain.Perlin();
            }
        }

        MultiPerlinBool = EditorGUILayout.Foldout(MultiPerlinBool, "Mult Perlin");
        if (MultiPerlinBool)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Mulitple Perlin Noise", EditorStyles.boldLabel);
            EditorGUILayout.IntSlider(perlinOffsetX, 0, 100, new GUIContent("X Offset"));
            EditorGUILayout.IntSlider(perlinOffsetY, 0, 100, new GUIContent("Y Offset"));
            EditorGUILayout.Slider(perlinXScale, 0, 1, new GUIContent("X Scale"));
            EditorGUILayout.Slider(perlinYScale, 0, 1, new GUIContent("Y Scale"));
            EditorGUILayout.IntSlider(perlinOctaves, 1, 10, new GUIContent("Octaves"));
            EditorGUILayout.Slider(perlinPersistance, 0.1f, 10, new GUIContent("Persistance"));
            EditorGUILayout.Slider(perlinHeightScale, 0, 1, new GUIContent("Height"));
            GUILayout.Space(20);

            if (GUILayout.Button("Apply Multiple Perlin"))
            {
                terrain.MultiplePerlinTerrain();
            }
        }

        showRandom = EditorGUILayout.Foldout(showRandom, "Random");

        if (showRandom)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Set Heights Between Random Values", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(randomHeight);
            if (GUILayout.Button("Random Heights"))
            {
                terrain.RandomTerrain();
            }
        }



        if (GUILayout.Button("Reset Terrain"))
        {
            terrain.ResetTerrain();
        }

        serializedObject.ApplyModifiedProperties();
    }

}


