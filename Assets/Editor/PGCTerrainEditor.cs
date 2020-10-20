
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PGCTerrain))]
[CanEditMultipleObjects]

public class PGCTerrainEditor : Editor
{

    private SerializedProperty heightMapScale;
    private SerializedProperty perlinXOffset;
    private SerializedProperty perlinYOffset;
    private SerializedProperty perlinScaleX;
    private SerializedProperty perlinScaleY;
    private SerializedProperty Persistance;
    private SerializedProperty perlinHeightScale;
    private SerializedProperty Octaves;
    private SerializedProperty randomHeight;


    private bool PerlinBool = false;
    private bool MultiPerlinBool;
    private bool showRandom = false;

    SerializedProperty vorMinPeak;
    SerializedProperty vorMaxPeak;
    SerializedProperty vorFall;
    SerializedProperty vorDrop;
    SerializedProperty voronoiType;
    SerializedProperty vorPeaks;
    bool showVoronoi = false;


    SerializedProperty perlinParameters;
    private void OnEnable()
    {

        heightMapScale = serializedObject.FindProperty("heightMapScale");
        perlinXOffset = serializedObject.FindProperty("perlinXOffset");
        perlinYOffset = serializedObject.FindProperty("perlinYOffset");
        perlinScaleX = serializedObject.FindProperty("perlinScaleX");
        perlinScaleY = serializedObject.FindProperty("perlinScaleY");
        Persistance = serializedObject.FindProperty("perlinPersistance");
        perlinHeightScale = serializedObject.FindProperty("perlinHeightScale");
        Octaves = serializedObject.FindProperty("Octaves");
        randomHeight = serializedObject.FindProperty("randomHeight");
        perlinParameters = serializedObject.FindProperty("perlinParameters");
        Persistance = serializedObject.FindProperty("Persistance");

        vorMinPeak = serializedObject.FindProperty("vorMinPeak");
        vorMaxPeak = serializedObject.FindProperty("vorMaxPeak");
        vorFall = serializedObject.FindProperty("vorFall");
        vorDrop = serializedObject.FindProperty("vorDrop");
        voronoiType = serializedObject.FindProperty("voronoiType");
        vorPeaks = serializedObject.FindProperty("vorPeaks");




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
            EditorGUILayout.Slider(perlinScaleX, 0, 1, new GUIContent("X Scale"));
            EditorGUILayout.Slider(perlinScaleY, 0, 1, new GUIContent("Y Scale"));
            EditorGUILayout.IntSlider(perlinXOffset, 0, 100, new GUIContent("X Offset"));
            EditorGUILayout.IntSlider(perlinYOffset, 0, 100, new GUIContent("Y OFfset"));
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
            EditorGUILayout.Slider(perlinScaleX, 0, 1, new GUIContent("X Scale"));
            EditorGUILayout.Slider(perlinScaleY, 0, 1, new GUIContent("Y Scale"));
            EditorGUILayout.IntSlider(perlinXOffset, 0, 100, new GUIContent("X Offset"));
            EditorGUILayout.IntSlider(perlinYOffset, 0, 100, new GUIContent("Y OFfset"));
            EditorGUILayout.IntSlider(Octaves, 1, 10, new GUIContent("Octaves"));
            EditorGUILayout.Slider(Persistance, 0.1f, 10, new GUIContent("Persistance"));
            EditorGUILayout.Slider(perlinHeightScale, 0, 1, new GUIContent("Height Scale"));
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

        showVoronoi = EditorGUILayout.Foldout(showVoronoi, "Voronoi");

        if (showVoronoi)
        {
            EditorGUILayout.Slider(vorMinPeak, 0, 1, new GUIContent("Min Peak"));
            EditorGUILayout.Slider(vorMaxPeak, 0, 1, new GUIContent("Max Peak"));
            EditorGUILayout.Slider(vorFall, 0, 10, new GUIContent("Fall off"));
            EditorGUILayout.Slider(vorDrop, 0, 10, new GUIContent("Drop off"));
            EditorGUILayout.IntSlider(vorPeaks, 1, 10, new GUIContent("voronoi Peaks"));
            EditorGUILayout.PropertyField(voronoiType);

            if (GUILayout.Button("Voronoi"))

            {
                terrain.Voronoi();

            }

        }




        if (GUILayout.Button("Reset Terrain"))
        {
            terrain.ResetTerrain();
        }

        serializedObject.ApplyModifiedProperties();
    }

}


