using UnityEditor;
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

        public GameObjectPilotOverlay() : base(TOOLS_IDS)
        {
        }
    }
}