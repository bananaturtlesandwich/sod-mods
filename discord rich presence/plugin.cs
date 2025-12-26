using DiscordRPC;
using BepInEx;
using HarmonyLib;

namespace Discord;

[BepInPlugin("spuds.discordrichpresence", "discord rich presence", "1.0.0")]
public sealed class DiscordPlugin : BaseUnityPlugin {

    static DiscordRpcClient client;
    static Timestamps now = Timestamps.Now;
    static string message = "Main Menu";

    Harmony patcher = new Harmony("spuds.discordrichpresence");

    void Start() {
        client = new("1453923240966619197");
        client.Initialize();
        SetPresence();
        patcher.PatchAll();
    }

    void OnDestroy() {
        client.Dispose();
        patcher.UnpatchSelf();
    }

    void Update() => client.Invoke();

    [HarmonyPatch(typeof(RsResourceManager), nameof(RsResourceManager.LoadLevel))]
    static class Patch_Transition {
        static void Prefix(string inLevel, bool skipMMOLogin) {
            message = inLevel.ToLower() switch {
                "arenafrenzydo" => "Playing Fireball Frenzy",
                "armorwingislanddo" => "On Armorwing island",
                "auctionshipintdo" => "Shopping at the auction ship",
                "berkcavesdo" => "Exploring the Berk caves",
                "berkcloudsdo" => "Visiting Gothi",
                "berkdocksdo" or "berkdocksnightdo" => "Going around the Berk docks",
                "berkfarmdo" => "Visiting Sven's farm",
                "clansdm" => "Catching up with the clan",
                "creditsdo" => "Watching the credits",
                "customizehuddm" => "Customising the HUD",
                "darkdeepcavesdo" => "Exploring the Dark Deep caves",
                "darkdeepdo" => "On Darp Deep",
                "declubhouseintdo" => "Chilling at the Dragon's Edge clubhouse",
                "dragonhunterbasedo" => "Infiltrating a dragon hunter base",
                "dragonracingdo"
                    or "trackdo01"
                    or "trackdo02"
                    or "trackdo03"
                    or "trackdo04"
                    or "trackdo05"
                    or "trackdo06"
                    or "trackdo07"
                    or "trackdo08"
                    or "trackdo09"
                    or "trackdo10"
                    or "trackdo11"
                    or "trackdo12"
                    or "trackdo13"
                    or "trackdo14"
                    or "trackdo15"
                    or "trackdo16"
                    or "trackdo17"
                    or "trackdo18"
                    or "trackdo19"
                    or "trackdo20"
                    or "trackdo21" => "Racing their dragon",
                "dragonstableamberintdo"
                    or "dragonstablebewilderbeastintdo"
                    or "dragonstableboulderintdo"
                    or "dragonstabledreadfallintdo"
                    or "dragonstablefuhuintdo"
                    or "dragonstablehiddenworldintdo"
                    or "dragonstableintdo"
                    or "dragonstablelavaintdo"
                    or "dragonstablenightlightsintdo"
                    or "dragonstablewaterintdo" => "Relaxing in the stables",
                "elementmatchdo" => "Playing Element Match",
                "farmingdo"
                    or "farmingdreadfalldo"
                    or "farmingoceando"
                    or "farmingthawfestdo" => "Tending to the farm",
                "flightschooldo"
                    or "flightschooldotheme1"
                    or "fsdeadlynadderdo"
                    or "fsflightsuitdo"
                    or "fsgronckledo"
                    or "fsnightfurydo"
                    or "fsnightmaredo"
                    or "fsthunderdrumdo"
                    or "fstimberjackdo"
                    or "fswhisperingdeathdo"
                    or "fszipplebackdo" => "Attending Flight School",
                "glacierislanddo" => "On Glacier island",
                "greathallschoolintdamageddo" or "greathallschoolintdo" => "Convening in the Great Hall",
                "greathallschoolintdreamdo" => "Tripping out in the Great Hall",
                "hatcheryint02do" or "hatcheryintdm" or "hatcheryintdo" => "Hatching baby dragons",
                "hauntedhousedo" => "Braving the Haunted House",
                "helheimsgatedo" => "Passing through Helheim's Gate",
                "hobblegruntislanddo" => "On Hobblegrunt island",
                "hubarctic01do" or "hubarcticintdo" => "On Icestorm island",
                "hubauctionislanddo" or "hubauctionislandvanaheimdo" => "Shopping at the auction",
                "hubberkdo" => "Chilling in Berk",
                "hubberknewdo" => "Chilling in New Berk",
                "hubcenotedo" or "hubcenoteint01do" or "hubcenoteint02do" => "Exploring the Cenote",
                "hubdeathsongislanddo" => "On Deathsong island",
                "hubdragonislanddo" or "hubdragonislandintdo" => "On Dragon island",
                "hubdragonsedgedo" => "Chilling in Dragon's Edge",
                "hubdreadfallracetrackdo" => "Checking out Ruff and Tuff's Dreadfall race track",
                "huberuptodonislanddo" => "On Eruptodon island",
                "hubftuedo" => "ftue?",
                "hubhiddenworlddo"
                    or "hubhiddenworldnbdo"
                    or "hubhiddenworldnbintdo"
                    or "stcurseofhgmap01do"
                    or "stcurseofhgmap02do"
                    or "sthiddenworld01mapdo"
                    or "sthiddenworld02mapdo"
                    or "sthiddenworld03mapdo"
                    or "sthiddenworld04mapdo" => "Exploring the Hidden World",
                "hublookoutdo" => "Scouting at the Lookout",
                "hubschooldo" => "Learning at the School",
                "hubschoolhiddenworlddo" => "hubschoolhiddenworld?",
                "hubtradewindislanddo" => "On Tradewind island",
                "hubtrainingdo" => "Defending the Training Grounds",
                "hubvanaheimdo"
                    or "hubvanaheimnightdo"
                    or "stvanaheim02mapdo"
                    or "stvanaheim04mapdo"
                    or "stvanaheim06mapdo"
                    or "stvanaheim09_1mapdo"
                    or "stvanaheim09_2mapdo"
                    or "stvanaheim09_3mapdo"
                    or "stvanaheim09_4mapdo" => "Respecting Vanaheim",
                "hubwarlordislanddo"
                    or "stwarlord01mapdo"
                    or "stwarlord02mapdo"
                    or "stwarlord03mapdo"
                    or "stwarlord04mapdo"
                    or "stwarlord05mapdo"
                    or "stwarlord07mapdo" => "Infiltrating the Warlord Outpost",
                "hubwilderness01do" => "Surviving the Wilderness",
                "journaldm" => "Flicking through their journal",
                "labintdo" => "Running experiments in the lab",
                "layout_stthawfest04mapdo" => "thawfest map?",
                "logindm" => "Logging in",
                "mazedreadfalldo"
                    or "mazesnoggletogdo"
                    or "mazespringdo"
                    or "mazetestscene"
                    or "mazethawfestdo"
                    or "mazetimetrialdo"
                    or "nightlightmazedo"
                    or "snoggletogmazedo" => "Solving a maze",
                "application" => "application?",
                "messageboarddm" => "Chatting on the message boards",
                "mudrakerislanddo" => "On Mudraker island",
                "myroomintdo" => "Decorating their room",
                "openoceando"
                    or "openoceannightdo"
                    or "openoceannorthernlightsdo"
                    or "openoceanremotedo"
                    or "openoceanvanaheimdo" => "Flying over the open ocean",
                "petplaydm" => "Playing with their dragon",
                "petplayeelblastdm" => "Roasting eels with their dragon",
                "piratequeenshipdo" => "Checking out Stormheart's ship",
                "profiledm" => "Customising their profile",
                "profileselectiondo" => "Choosing their profile",
                "reaperintdo" => "reaper?",
                "sandbusterlairintdo" or "stsandbuster01mapdo" or "stsandbuster02mapdo" => "Sneaking around the Sandbuster's lair",
                "scuttleclawislanddo" => "On Scuttleclaw island",
                "shipgraveyarddo" or "stshipgraveyard01mapdo" => "Scouring the Ship Graveyard",
                "sinkingboatintdo" => "Escaping a sinking ship",
                "starctict01mapdo"
                    or "starctict02mapdo"
                    or "starctict03mapdo"
                    or "starctict04mapdo"
                    or "starctict05mapdo"
                    or "starctict06mapdo"
                    or "starctict07mapdo"
                    or "starctict08mapdo"
                    or "starena01mapdo"
                    or "starena02mapdo"
                    or "starena03mapdo"
                    or "starena04mapdo"
                    or "starena05mapdo"
                    or "starena06mapdo"
                    or "starena07mapdo"
                    or "starena08mapdo"
                    or "stdreadfall01mapdo"
                    or "stdreadfall02mapdo"
                    or "stdreadfall03mapdo"
                    or "stdreadfall04mapdo"
                    or "stdreadfall05mapdo"
                    or "stdreadfall06mapdo"
                    or "stdreadfall07mapdo"
                    or "stdreadfall08mapdo"
                    or "stnightlight01mapdo"
                    or "stnightlight02mapdo"
                    or "stnightlight03mapdo"
                    or "stnightlight04mapdo"
                    or "stlevelselectiondo"
                    or "stpiratearenamapdo"
                    or "stpiratebossmapdo"
                    or "stpiratedemapdo"
                    or "stpiratetradermapdo"
                    or "stprank01mapdo"
                    or "stprank02mapdo"
                    or "stprank03mapdo"
                    or "stremoteisland01mapdo"
                    or "strunestone01mapdo"
                    or "strunestone02mapdo"
                    or "strunestone03mapdo"
                    or "strunestone04mapdo"
                    or "strunestone05mapdo"
                    or "strunestone06mapdo"
                    or "strunestone07mapdo"
                    or "strunestone08mapdo"
                    or "strunestonemap01do"
                    or "strunestonemap02do"
                    or "strunestonemap03do"
                    or "strunestonemap04do"
                    or "strunestonemap05do"
                    or "strunestonemap06do"
                    or "strunestonemap07do"
                    or "strunestonemap08do"
                    or "stsnoggletog1mapdo"
                    or "stsnoggletog2mapdo"
                    or "stsnoggletog3mapdo"
                    or "stsnoggletog4mapdo"
                    or "stsnoggletog5mapdo"
                    or "stsnoggletog6mapdo"
                    or "stsnoggletog7mapdo"
                    or "stsnoggletog8mapdo"
                    or "stsummer01mapdo"
                    or "stsummer02mapdo"
                    or "stsummer03mapdo"
                    or "stsummer04mapdo"
                    or "stsummer05mapdo"
                    or "stsummer06mapdo"
                    or "stsummer07mapdo"
                    or "stsummer08mapdo"
                    or "stthawfest01mapdo"
                    or "stthawfest02mapdo"
                    or "stthawfest03mapdo"
                    or "stthawfest04mapdo"
                    or "stthawfest05mapdo"
                    or "stthawfest06mapdo"
                    or "stthawfest07mapdo"
                    or "stthawfest08mapdo"
                    or "sttrainer01mapdo"
                    or "sttrainer02mapdo"
                    or "sttrainer03mapdo"
                    or "sttrainer04mapdo"
                    or "sttrainer05mapdo"
                    or "sttrainer06mapdo"
                    or "sttrainer07mapdo"
                    or "sttrainer08mapdo"
                    or "sttutorialmapdo"
                    or "stzipplewraith01mapdo" => "Playing Dragon Tactics",
                "storesdm" => "Buying supplies at the store",
                "targetpracticedo" => "target practice?",
                "titanislanddo" => "On Titan island",
                "underwatervanaheimdo" => "Diving around Vanaheim",
                "valkahideoutintdo" => "Sheltering in Valka's sanctuary",
                "zipplebackislanddo" => "On Zippleback island",
                _ => inLevel,
            };
            SetPresence();
        }
    }

    static void SetPresence() => client.SetPresence(new() {
        State = message,
        Assets = new Assets() {
            LargeImageKey = "icon",
        },
        Timestamps = now
    });
}

