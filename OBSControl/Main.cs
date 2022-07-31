using System;
using ABI.CCK.Components;
using ABI_RC.Core.Base;
using ABI_RC.Core.IO;
using ABI_RC.Core.Networking;
using ABI_RC.Core.Networking.IO.Instancing;
using ABI_RC.Core.UI;
using MelonLoader;
using OBSControl;
using Main = OBSControl.Main;

[assembly: MelonInfo(typeof(Main), Guh.Name, Guh.Version, Guh.Author, Guh.DownloadLink)]
[assembly: MelonGame("Alpha Blend Interactive", "ChilloutVR")]
[assembly: MelonColor(ConsoleColor.DarkCyan)]

namespace OBSControl;

public static class Guh
{
    public const string Name = "OBSControl";
    public const string Author = "Animal";
    public const string Version = "1.0.1";
    public const string DownloadLink = "https://github.com/Aniiiiiimal/OBSControl";
}

public class Main : MelonMod
{
    private static MelonPreferences_Category category;

    public override void OnApplicationStart()
    {
        Logger.Logs = LoggerInstance;
        InitPrefs();

        HarmonyInstance.Patch(
            typeof(CVRObjectLoader).GetMethod(nameof(CVRObjectLoader.InitiateLoadIntoWorld)), null,
            typeof(World).GetMethod(nameof(World.WorldJoin)).ToNewHarmonyMethod());
        HarmonyInstance.Patch(
            typeof(CVRWorld).GetMethod("OnDestroy"), null,
            typeof(World).GetMethod(nameof(World.WorldLeave)).ToNewHarmonyMethod());

        MelonCoroutines.Start(OBSMenu.OnUiReady());
    }

    public static MelonPreferences_Entry<string> HostIP, HostPassword, WorldLeaveScene, WorldJoinScene;
    public static MelonPreferences_Entry<bool> AutoConnect, SwitchWorldScene;

    private static void InitPrefs()
    {
        category = MelonPreferences.CreateCategory(Guh.Name, "OBS Control");
        HostIP = category.CreateEntry("HostIP", "ws://localhost:4444", "Host Address");
        HostPassword = category.CreateEntry("HostPassword", "", "Host Password");
        AutoConnect = category.CreateEntry("AutoConnect", false, "Connect when CVR starts");
        SwitchWorldScene = category.CreateEntry("SwitchWorldScene", false, "Switch scenes when switching worlds");
        WorldJoinScene = category.CreateEntry("WorldJoinScene", "", "Destination scene (leave blank for prior scene)");
        WorldLeaveScene = category.CreateEntry("WorldLeaveScene", "", "Switching worlds scene");
    }
}