using System.Reflection;
using Overlayer.Core;
using Overlayer.Core.Patches;
using Overlayer.Core.Translation;
using Overlayer.Modules;
using UnityEngine;

namespace MoreTimeTags {
    public class Main : OverlayerModule {
        public static Main Instance;
        private static Assembly _assembly;

        public Main() {
            Instance = this;
        }

        public override bool IsEnabled { get; set; }

        public override void OnLoad() {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public override void OnEnable() {
            LazyPatchManager.Load(_assembly);
            TagManager.Load(typeof(CustomTags));
            TextManager.Refresh();
            IsEnabled = true;
        }

        public override void OnDisable() {
            TagManager.Unload(typeof(CustomTags));
            LazyPatchManager.Unload(_assembly);
            IsEnabled = false;
            MemoryHelper.Clean(CleanOption.All);
        }

        public override void OnUnload() {
            _assembly = null;
        }

        public override void OnGUI() {
            Values values = GetValues();
            GUILayout.Label("MoreTimeTags", new GUIStyle(GUI.skin.label) {
                font = FontManager.GetFont("Default").font,
                fontSize = 50,
                richText = true
            });
            GUILayout.BeginHorizontal();
            GUILayout.Label(values.Credit_Devloper + " : Jongyeol");
            if(GUILayout.Button(values.Credit_Source)) Application.OpenURL("https://github.com/Jongye0l/MoreTimeTags");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Label(values.Credit_BugReport);
        }
        
        public static Values GetValues() {
            return Overlayer.Main.Language == Language.Korean ? Values.Korean : Values.English;
        }
    }
}