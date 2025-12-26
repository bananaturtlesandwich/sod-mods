using DiscordRPC;
using BepInEx;
using HarmonyLib;

namespace Discord;

[BepInPlugin("spuds.discordrichpresence", "discord rich presence", "1.0.0")]
public sealed class DiscordPlugin : BaseUnityPlugin {

    static DiscordRpcClient client;
    static Timestamps now = Timestamps.Now;
    static string level = "Main Menu";

    void Start() {
        client = new("1453923240966619197");
        client.Initialize();
        SetPresence();
        new Harmony("spuds.discordrichpresence").PatchAll();
    }

    void Update() => client.Invoke();

    void OnApplicationQuit() => client.Dispose();

    [HarmonyPatch(typeof(RsResourceManager), "LoadLevel")]
    static class Patch_Transition {
        static void Prefix(string inLevel, bool skipMMOLogin) {
            level = inLevel;
            SetPresence();
        }
    }

    static void SetPresence() => client.SetPresence(new() {
        State = "Playing in " + level,
        Assets = new Assets() {
            LargeImageKey = "icon",
        },
        Timestamps = now
    });
}

