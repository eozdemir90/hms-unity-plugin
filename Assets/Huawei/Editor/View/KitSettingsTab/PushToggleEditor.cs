﻿using UnityEngine;
using UnityEditor;
using HmsPlugin;
using System;
using UnityEditor.SceneManagement;

namespace HmsPlugin
{
    public class PushToggleEditor : ToggleEditor, IDrawer
    {
        public const string PushKitEnabled = "PushKit";

        public PushToggleEditor()
        {
            bool enabled = HMSMainEditorSettings.Instance.Settings.GetBool(PushKitEnabled);
            _toggle = new Toggle.Toggle("Push", enabled, OnStateChanged, true);
            Enabled = enabled;
        }

        private void OnStateChanged(bool value)
        {
            if (value)
            {
                CreateManagers();
            }
            else
            {
                DestroyManagers();
            }
            HMSMainEditorSettings.Instance.Settings.SetBool(PushKitEnabled, value);
        }

        public void Draw()
        {
            _toggle.Draw();
        }

        public override void CreateManagers()
        {
            if (!HMSPluginSettings.Instance.Settings.GetBool(PluginToggleEditor.PluginEnabled, true))
                return;
            if (GameObject.FindObjectOfType<HMSPushKitManager>() == null)
            {
                GameObject obj = new GameObject("HMSPushKitManager");
                obj.AddComponent<HMSPushKitManager>();
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            Enabled = true;
        }

        public override void DestroyManagers()
        {
            var pushManagers = GameObject.FindObjectsOfType<HMSPushKitManager>();
            if (pushManagers.Length > 0)
            {
                for (int i = 0; i < pushManagers.Length; i++)
                {
                    GameObject.DestroyImmediate(pushManagers[i].gameObject);
                }
            }
            Enabled = false;
        }

        public override void DisableManagers(bool removeTabs)
        {
            var pushManagers = GameObject.FindObjectsOfType<HMSPushKitManager>();
            if (pushManagers.Length > 0)
            {
                for (int i = 0; i < pushManagers.Length; i++)
                {
                    GameObject.DestroyImmediate(pushManagers[i].gameObject);
                }
            }
        }

        public override void RefreshToggles()
        {
            if (_toggle != null)
            {
                _toggle.SetChecked(HMSMainEditorSettings.Instance.Settings.GetBool(PushKitEnabled));
            }
        }
    }
}