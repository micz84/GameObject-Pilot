using System;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace pl.micz84.gameobjectpilot.Editor
{
    [EditorToolbarElement(ID, typeof(SceneView))]
    [Icon("Packages/pl.micz84.gameobjectpilot/Runtime/Icons/camera.png")]
    public class PilotGameObjectTool : EditorToolbarToggle
    {
        private GameObject[] _selectedGameObjects;
        public const string ID = "pilot-game-object-tool";

        public PilotGameObjectTool()
        {
            icon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                "Packages/pl.micz84.gameobjectpilot/Runtime/Icons/camera.png");
            tooltip = "Pilot GameObject";
            Selection.selectionChanged += OnSelectionChangedHandler;
            GameObjectPilotOverlay.OnDisplayedChanged += OnDisplayedChangedHandler;
            RegisterCallback<ChangeEvent<bool>>(ChangeEventCallback);
        }

        private void OnDisplayedChangedHandler(bool isShown)
        {
            if(isShown)
                return;
            Selection.selectionChanged -= OnSelectionChangedHandler;
            UnregisterCallback<ChangeEvent<bool>>(ChangeEventCallback);
            SceneView.duringSceneGui -= SceneViewOnDuringSceneGui;
            GameObjectPilotOverlay.OnDisplayedChanged -= OnDisplayedChangedHandler;
        }

        private void OnSelectionChangedHandler()
        { // if tool is selected and no game object is selected take first selection
            if (value && (_selectedGameObjects == null || _selectedGameObjects.Length == 0))
                _selectedGameObjects = Selection.gameObjects;

        }

        private void ChangeEventCallback(ChangeEvent<bool> evt)
        {
            var state = evt.newValue;
            if (state)
                _selectedGameObjects = Selection.gameObjects;
            else
                _selectedGameObjects = null;

            
            if (state)
                SceneView.duringSceneGui += SceneViewOnDuringSceneGui;
            else
                SceneView.duringSceneGui -= SceneViewOnDuringSceneGui;
        }

        private void SceneViewOnDuringSceneGui(SceneView sceneView)
        {
            var sceneViewCameraTransform = sceneView.camera.transform;
            var position = sceneViewCameraTransform.position;
            var rotation = sceneViewCameraTransform.rotation;

            for (var i = 0; i < _selectedGameObjects.Length; i++)
            {
                var t = _selectedGameObjects[i].transform;
                t.position = position;
                t.rotation = rotation;
            }
        }
    }
}