using BepInEx;
using HarmonyLib;

namespace SuitUnequip;

[BepInPlugin("spuds.suitunequip", "suit unequip", "1.0.0")]
public sealed class SuitUnequipPlugin : BaseUnityPlugin {

    Harmony patcher = new Harmony("spuds.suitunequip");

    void Start() => patcher.PatchAll();

    void OnDestroy() => patcher.UnpatchSelf();

    static void RemoveSuit(ref AvAvatarController ctrl) {
        ctrl.mGlideEndTime = 0f;
        ctrl.mWasPlayerGliding = false;
        ctrl.EnableRemoveButton(false);
        if (!ctrl.mIsRemoveTutorialDone) {
            ProductData.AddTutorial(ctrl._RemoveTutorialName);
            ctrl.mIsRemoveTutorialDone = true;
        }
        ctrl.pAvatarCustomization.RestoreAvatar(true, false);
        ctrl.pAvatarCustomization.SaveCustomAvatar();
    }

    [HarmonyPatch(typeof(AvAvatarController), nameof(AvAvatarController.OnGlideLanding))]
    static class Patch_Ground {
        static void Postfix(ref AvAvatarController __instance) => RemoveSuit(ref __instance);
    }

    [HarmonyPatch(typeof(AvAvatarController), nameof(AvAvatarController.CheckForSubStateChange))]
    static class Patch_Swim {
        static void Prefix(AvAvatarController __instance, out AvAvatarSubState __state) => __state = __instance.pSubState;
        static void Postfix(ref AvAvatarController __instance, AvAvatarSubState __state) {
            if (__state != AvAvatarSubState.SWIMMING && __instance.pSubState == AvAvatarSubState.SWIMMING) RemoveSuit(ref __instance);
        }
    }

    [HarmonyPatch(typeof(UiAvatarControls), nameof(UiAvatarControls.OnDragonMount))]
    static class Patch_Reride {
        static void Postfix(ref UiAvatarControls __instance, bool mount) {
            if (mount) RemoveSuit(ref __instance.mAVController);
        }
    }
}
