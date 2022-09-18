using BepInEx;
using HarmonyLib;
using PotionCraft.ManagersSystem.RecipeMap;
using UnityEngine;

namespace NormalisedLadleRotation
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, "1.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        private static bool _intercept = false;

        [HarmonyPrefix, HarmonyPatch(typeof(RecipeMapManager), "MoveIndicatorTowardsObject")]
        public static void MoveIndicatorTowardsObject_Prefix(Vector2 objectLocalPosition, float objectEulerAngle, float positionSpeed, float rotationMaxSpeed, float rotationAnimationTime, ref float movedDistance, ref float movedAngle, ref bool wasMovingStartedAlready, ref float globalMovingProgress, ref float rotationDistanceOnStart, ref float indicatorRotationValueOnStart)
        {
            _intercept = true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RecipeMapManager.IndicatorRotationSubManager), "RunRotationMovementTween")]
        public static void RunRotationMovementTween_Prefix(float currentVisualValue, ref float targetVisualValue, float animationTime)
        {
            if (_intercept)
            {
                float diff = targetVisualValue - currentVisualValue;
                diff *= (0.01f / Time.deltaTime);
                targetVisualValue = currentVisualValue + diff;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RecipeMapManager), "MoveIndicatorTowardsObject")]
        public static void MoveIndicatorTowardsObject_Postfix(Vector2 objectLocalPosition, float objectEulerAngle, float positionSpeed, float rotationMaxSpeed, float rotationAnimationTime, ref float movedDistance, ref float movedAngle, ref bool wasMovingStartedAlready, ref float globalMovingProgress, ref float rotationDistanceOnStart, ref float indicatorRotationValueOnStart)
        {
            _intercept = false;
        }
    }
}
