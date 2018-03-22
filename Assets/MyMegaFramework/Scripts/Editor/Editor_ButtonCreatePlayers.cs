using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MG_PlayerManager), true)]
public class Editor_ButtonCreatePlayers : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MG_PlayerManager myManager = (MG_PlayerManager)target;

        if (GUILayout.Button("Create Players"))
        {
            myManager.RemoveAllPlayers();
            myManager.CreatePlayers();
        }

        if (GUILayout.Button("Remove All Players"))
        {
            myManager.RemoveAllPlayers();
        }
    }
}
