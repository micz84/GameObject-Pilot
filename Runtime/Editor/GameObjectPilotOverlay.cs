using System;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;

namespace pl.micz84.gameobjectpilot.Editor
{
    [Overlay(editorWindowType = typeof(SceneView), id = ID_OVERLAY, displayName = "GameObject Pilot")]
    [Icon("Packages/pl.micz84.gameobjectpilot/Runtime/Icons/camera.png")]
    public class GameObjectPilotOverlay : ToolbarOverlay
    {
        private const string ID_OVERLAY = "gameobject-pilot-overlay";
        private static readonly string[] TOOLS_IDS = new[] { PilotGameObjectTool.ID };

        private bool _state = false;
        private Button _button = null;
        internal static event Action<bool> OnDisplayedChanged;
        public GameObjectPilotOverlay() : base(TOOLS_IDS)
        {
            displayedChanged += b =>
            {
                OnDisplayedChanged?.Invoke(b);
            };
        }
    }
}