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
        [PluginReference] private Plugin ImageLibrary;

        #endregion

        #region OxideHooks

        private object CanLootEntity(BasePlayer player, StorageContainer container)
        {
            if (player == null || !(container.GetEntity() is BuildingPrivlidge)) return null;
            ShowUI(player);
            player.ChatMessage("ТЫ НЕ ПИДОРА ЗХАХАХАХХАХАВХАХВАХВХАВХА");
            return null;
        }

        private void OnLootEntityEnd(BasePlayer player, BaseCombatEntity entity)
        {
            CuiHelper.DestroyUi(player, Layer);
        }

        #endregion

        #region UI

        private void ShowUI(BasePlayer player)
        {
            var container = new CuiElementContainer();

            container.Add(new CuiPanel
            {
                RectTransform = {AnchorMin = "0.655 0.0235", AnchorMax = "0.83 0.1355"},
                Image = {Color = "0.38 0.35 0.33 0.55"}
            }, "Overlay", Layer);

            container.Add(new CuiLabel
            {
                RectTransform = {AnchorMin = "0 0.8", AnchorMax = "1 1"},
                Text =
                {
                    Text = "ТЕКУЩИЙ УРОВЕНЬ ШКАФА", Font = "robotocondensed-bold.ttf", FontSize = 10, Align = TextAnchor.MiddleCenter,
                    Color = "0.87 0.83 0.79 1"
                }
            }, Layer);

            container.Add(new CuiPanel
            {
                RectTransform = {AnchorMin = "0 0", AnchorMax = "0 0", OffsetMin = "5 5", OffsetMax = "60 60"},
                Image = {Color = "0.38 0.35 0.33 0.8"}
            }, Layer, Layer + ".item");
            
            container.Add(new CuiElement
            {
                Parent = Layer + ".item",
                Components =
                {
                    new CuiRawImageComponent {Png = ImageLibrary.Call<string>("GetImage", "pickaxe")},
                    new CuiRectTransformComponent {AnchorMin = "0 0", AnchorMax = "0 0", OffsetMin = "5 5", OffsetMax = "50 50"}
                }
            });

            container.Add(new CuiLabel
            {
                RectTransform = {AnchorMin = "0 0", AnchorMax = "1 0.25"},
                Text =
                {
                    Text = "x1000", Font = "robotocondensed-regular.ttf", FontSize = 10, Align = TextAnchor.LowerRight,
                    Color = "0.87 0.83 0.79 1"
                }
            }, Layer + ".item");
 
            container.Add(new CuiButton
            {
                RectTransform = {AnchorMin = "0.2 0.0", AnchorMax = "1 0.8"},
                Button = {Color = "0 0 0 0"},
                Text =
                {
                    Text = "УЛУЧШИТЬ", Font = "robotocondensed-bold.ttf", FontSize = 16, Align = TextAnchor.MiddleCenter,
                    Color = "0.87 0.83 0.79 1"
                }
            }, Layer);
            
            CuiHelper.DestroyUi(player, Layer);
            CuiHelper.AddUi(player, container);
        }

        #endregion
    }
}