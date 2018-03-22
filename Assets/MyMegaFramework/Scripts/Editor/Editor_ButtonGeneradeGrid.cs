using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MG_Grid), true)]
public class Editor_ButtonGeneradeGrid : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MG_Grid gridGenerator = (MG_Grid)target;

        if (GUILayout.Button("Generate Grid"))
        {
            gridGenerator.RemoveAllCells();
            gridGenerator.GenerateGrid();
        }

        if (GUILayout.Button("Clear Grid"))
        {
            gridGenerator.RemoveAllCells();
        }

        if (GUILayout.Button("Snap Units To Grid"))
        {
            gridGenerator.SnapUnitsToGrid();
        }



        
    }

  
}
