using BepInEx;
using HarmonyLib;

namespace SuitUnequip;

[BepInPlugin("spuds.suitunequip", "suit unequip", "1.0.1")]
public sealed class SuitUnequipPlugin : BaseUnityPlugin {

    Harmony patcher = new Harmony("spuds.suitunequip");

    void Start() => patcher.PatchAll();

    void OnDestroy() => patcher.UnpatchSelf();

    [HarmonyPatch(typeof(AvAvatarController), nameof(AvAvatarController.OnGlideLanding))]
    static class Patch_Land {
        static void Postfix(ref AvAvatarController __instance) {
            __instance.mGlideEndTime = 0f;
            __instance.mWasPlayerGliding = false;
            __instance.EnableRemoveButton(false);
            if (!__instance.mIsRemoveTutorialDone) {
                ProductData.AddTutorial(__instance._RemoveTutorialName);
                __instance.mIsRemoveTutorialDone = true;
            }
            __instance.pAvatarCustomization.RestoreAvatar(true, false);
            __instance.pAvatarCustomization.SaveCustomAvatar();
        }
    }
}
