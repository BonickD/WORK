using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Game.Rust.Cui;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("TCGrades", "AhigaO#4485", "1.0.0")]
    internal class TCGrades : RustPlugin
    {
        #region Static

        private const string Layer = "UI_TCGrades";
        private int ImageLibraryCheck = 0;
        private Configuration _config;
        private Dictionary<ulong, Data> data;
        [PluginReference] private Plugin ImageLibrary;

        #endregion

        #region Config

        private class Configuration
        {
            
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();
            try
            {
                _config = Config.ReadObject<Configuration>();
                if (_config == null) throw new Exception();
                SaveConfig();
            }
            catch
            {
                PrintError("Your configuration file contains an error. Using default configuration values.");
                LoadDefaultConfig();
            }
        }

        protected override void SaveConfig() => Config.WriteObject(_config);

        protected override void LoadDefaultConfig() => _config = new Configuration();

        #endregion

        #region Data

        private class Data
        {

        }

        private void LoadData()
        {
            if (Interface.Oxide.DataFileSystem.ExistsDatafile($"{Name}/data"))
                data = Interface.Oxide.DataFileSystem.ReadObject<Dictionary<ulong, Data>>(
                    $"{Name}/data");
            else data = new Dictionary<ulong, Data>();
            Interface.Oxide.DataFileSystem.WriteObject($"{Name}/data", data);
        }

        private void OnServerSave()
        {
            SaveData();
        }

        private void SaveData()
        {
            if (data != null) Interface.Oxide.DataFileSystem.WriteObject($"{Name}/data", data);
        }

        #endregion

        #region OxideHooks

        private void OnServerInitialized()
        {
            if (!ImageLibrary)
            {
                if (ImageLibraryCheck == 3)
                {
                    PrintError("ImageLibrary not found!Unloading");
                    Interface.Oxide.UnloadPlugin(Name);
                    return;
                }

                timer.In(1, () =>
                {
                    ImageLibraryCheck++;
                    OnServerInitialized();
                });
                return;
            }

            LoadData();
        }

        private void Unload()
        {
            SaveData();
        }

        private object CanLootEntity(BasePlayer player, StorageContainer container)
        {
            if (player == null || !(container.GetEntity() is BuildingPrivlidge)) return null;
            ShowUI(player);
            player.ChatMessage("ТЫ НЕ ПИДОРА ЗХАХАХАХХАХАВХАХВАХВХАВХА");
            return null;
        }
        
        private void OnPlayerConnected(BasePlayer player)
        {
            if (player == null || data.ContainsKey(player.userID)) return;
            data.Add(player.userID, new Data());
        }
        
        private void OnLootEntityEnd(BasePlayer player, BaseCombatEntity entity)
        {
            CuiHelper.DestroyUi(player, Layer);
        }

        #endregion

        #region Commands


        #endregion

        #region Functions



        #endregion

        #region UI

        private void ShowUI(BasePlayer player)
        {
            var container = new CuiElementContainer();

            container.Add(new CuiPanel
            {
                RectTransform = {AnchorMin = "0.655 0.0235", AnchorMax = "0.83 0.1355"},
                Image = {Color = "0.38 0.35 0.33 0.4"}
            }, "Overlay", Layer);
            container.Add(new CuiLabel
            {
                RectTransform = {AnchorMin = "0 0.7", AnchorMax = "1 1"},
                Text =
                {
                    Text = "Текущий", Font = "robotocondensed-bold.ttf", FontSize = 12, Align = TextAnchor.MiddleCenter,
                    Color = "1 1 1 1"
                }
            }, Layer);
            container.Add(new CuiElement
            {
                Parent = Layer,
                Components =
                {
                    new CuiRawImageComponent {Png = ImageLibrary.Call<string>("GetImage", "")},
                    new CuiRectTransformComponent {AnchorMin = "0 0", AnchorMax = "0.4 0.65"}
                }
            });
            CuiHelper.DestroyUi(player, Layer);
            CuiHelper.AddUi(player, container);
        }

        #endregion
    }
}