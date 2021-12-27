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
                    new CuiRawImageComponent {Png = ImageLibrary.Call<string>("GetImage", "123")},
                    new CuiRectTransformComponent {AnchorMin = "0 0", AnchorMax = "0.4 0.65"}
                }
            });

            CuiHelper.DestroyUi(player, Layer);
            CuiHelper.AddUi(player, container);
        }

        #endregion
    }
}