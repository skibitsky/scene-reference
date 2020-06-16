using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Skibitsky.Unity
{
    [Serializable]
    public class SceneReference : ISerializationCallbackReceiver, IComparable<SceneReference>
    {
        /// <summary>
        /// Name of the scene
        /// </summary>
        public string SceneName
        {
            get => sceneName;
            private set => sceneName = value;
        }

        /// <summary>
        /// Build index.Returns -1 if the scene is missing in the Unity build settings
        /// </summary>
        public int BuildIndex
        {
            get => buildIndex;
            private set => buildIndex = value;
        }

        /// <summary>
        /// Full path to the .unity asset
        /// </summary>
        public string AssetPath
        {
            get => assetPath;
            private set => assetPath = value;
        }

#if UNITY_EDITOR
        [SerializeField]
        private SceneAsset sceneAsset;
#endif
        [SerializeField] private string sceneName;
        [SerializeField] private int buildIndex;
        [SerializeField] private string assetPath;

        // Makes it work with the existing Unity methods (LoadLevel/LoadScene)
        public static implicit operator string(SceneReference sceneReference) { return sceneReference.SceneName; }
        
        public void OnBeforeSerialize()
        {
            Validate();
        }

        public void OnAfterDeserialize() { }

        private void Validate()
        {
#if UNITY_EDITOR
            if (sceneAsset == null)
            {
                AssetPath = "";
                BuildIndex = -1;
                SceneName = "";
                return;
            }
            
            assetPath = AssetDatabase.GetAssetPath(sceneAsset);
            SceneName = Path.GetFileNameWithoutExtension(assetPath);
            BuildIndex = SceneUtility.GetBuildIndexByScenePath(assetPath);

            if (BuildIndex == -1)
                Debug.LogWarning($"The scene <b>{SceneName}</b> is missing in the Unity build settings");
#endif
        }

        protected bool Equals(SceneReference other)
        {
            return AssetPath == other.AssetPath;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((SceneReference) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable twice NonReadonlyMemberInGetHashCode
            return AssetPath != null ? AssetPath.GetHashCode() : 0;
        }

        public int CompareTo(SceneReference other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : string.Compare(assetPath, other.assetPath, StringComparison.Ordinal);
        }
    }
}