using BepInEx;
using HarmonyLib;
using PotionCraft.ManagersSystem.RecipeMap;
using UnityEngine;

namespace NormalisedLadleRotation
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RecipeMapManager.IndicatorRotationSubManager), "RunRotationMovementTween")]
        public static void RunRotationMovementTween_Prefix(float currentVisualValue, ref float targetVisualValue, float animationTime)
        {
            float diff = targetVisualValue - currentVisualValue;
            diff *= (0.01f / Time.deltaTime);
            targetVisualValue = currentVisualValue + diff;
        }
    }
}
