using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [CreateAssetMenu()]
    public class Sounds : ScriptableObject
    {
        [SerializeField] private AudioClip[] m_sounds = new AudioClip[Enum.GetNames(typeof(Sound)).Length];
        public AudioClip this[Sound s] => m_sounds[(int)s];
        /*
        #if UNITY_EDITOR
        [CustomEditor(typeof(Sounds))]
        public class SoundsInspector : Editor
        {
            private static readonly int soundCount = Enum.GetValues(typeof(Sound)).Length;

            private new Sounds target => base.target as Sounds;

            public override void OnInspectorGUI()
            {
                if (target.m_sounds.Length < soundCount)
                {
                    Array.Resize(ref target.m_sounds, soundCount);
                }

                for (int i = 0; i < target.m_sounds.Length; i++)
                {
                    target.m_sounds[i] = EditorGUILayout.ObjectField($"{(Sound)i} ({i}):",
                        target.m_sounds[i],typeof(AudioClip), false) as AudioClip;
                }
            }
        }
        #endif*/
    }
}
