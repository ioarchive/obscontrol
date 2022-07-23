using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using ReMod.Core.VRChat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.UI.Core;
using VRC.UI.Elements.Controls;
using Object = UnityEngine.Object;

namespace OBSControl;

public class OBSMenu
{
    private static ReCategoryPage obsPage;
    internal static OBSWebsocket obs;
    private static ReMenuHeader title;
    private static GameObject connectButton;
    private static ReMenuCategory connectState, streamState, recState, currentScene, stats;

    public static IEnumerator OnUiReady()
    {
        while (UIManager.prop_UIManager_0 == null) yield return null;
        while (Object.FindObjectOfType<VRC.UI.Elements.QuickMenu>() == null) yield return null;

        
        NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1
            .field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>(player =>
            {
                if (player == null) return;
                World.WorldLeave();
            }));

        ReTabButton.Create("obscontrol", "Remotely control OBS from here", "OBSControl",
            ResourceManager.GetSprite("OBSControl.obs"));
        obsPage = ReCategoryPage.Create("OBSControl", true);
        Logger.Msg("hi");

        connectState = obsPage.AddCategory("Connection: <color=red>Disconnected</color>");

        connectButton = connectState.Header.RectTransform.Find("InfoButton").gameObject;
        connectButton.SetActive(true);
        connectButton.transform.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>().text = "Connect";
        Object.DestroyImmediate(connectButton.GetComponent<PushPageButton>());
        connectButton.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = "Connect / Disconnect";

        connectButton.GetComponent<Button>().onClick.AddListener(new Action(() =>
        {
            switch (obs.IsConnected)
            {
                case true:
                    QuickMenuEx.Instance.ShowConfirmDialog("Disconnect from OBS?", "Are you sure?",
                        () => { obs.Disconnect(); });
                    break;
                case false:
                    Task.Run(ConnectToObs);
                    break;
            }
        }));

        streamState = obsPage.AddCategory("Streaming: <color=red>Stopped</color>", false);
        recState = obsPage.AddCategory("Recording: <color=red>Stopped</color>", false);
        currentScene = obsPage.AddCategory("Scene: ", false);
        stats = obsPage.AddCategory("Stats: ", false);
        var general = obsPage.AddCategory("General");
        title = general.Header;
        obs = new OBSWebsocket();

        obs.Connected += OnConnect;
        obs.Disconnected += OnDisconnect;
        obs.SceneChanged += OnSceneChanged;
        obs.StreamStatus += StreamStatus;

        general.AddButton("Start/Stop Recording", "Start or stop a recording", obs.StartStopRecording);
        general.AddButton("Start/Stop Stream", "Start or stop streaming", obs.StartStopStreaming);

        if (Main.AutoConnect.Value)
            Task.Run(ConnectToObs);
    }

    private static Task ConnectToObs()
    {
        if (obs is {IsConnected: true}) return Task.CompletedTask;
        var serverAddress = Main.HostIP.Value;
        string msg;
        if (string.IsNullOrEmpty(serverAddress))
        {
            msg = "Host IP cannot be null or empty. Change in settings";
            Logger.Error(msg);
            return Task.CompletedTask;
        }

        try
        {
            obs.Connect(serverAddress, Main.HostPassword.Value);
        }
        catch (AuthFailureException)
        {
            msg = $"Authentication failed connecting to server {serverAddress}.";
            QuickMenuEx.Instance.ShowAlertDialog("OBSControl", msg);
            Logger.Error(msg);
        }
        catch (ErrorResponseException ex)
        {
            msg = $"Failed to connect to server {serverAddress}: {ex.Message}.";
            QuickMenuEx.Instance.ShowAlertDialog("OBSControl", "Failed to connect. Check console");
            Logger.Error(msg);
        }
        catch (Exception ex)
        {
            msg = $"Failed to connect to server {serverAddress}: {ex.Message}.";
            QuickMenuEx.Instance.ShowAlertDialog("OBSControl", "Failed to connect. Check console");
            Logger.Error(msg);
        }

        return Task.CompletedTask;
    }

    public static void LoadIcons()
    {
        // https://github.com/RequiDev/ReModCE/blob/master/ReModCE/ReMod.cs
        var ourAssembly = Assembly.GetExecutingAssembly();
        var resources = ourAssembly.GetManifestResourceNames();
        foreach (var resource in resources)
        {
            if (!resource.EndsWith(".png"))
                continue;

            var stream = ourAssembly.GetManifestResourceStream(resource);

            var ms = new MemoryStream();
            stream?.CopyTo(ms);
            var resourceName = Regex.Match(resource, @"([a-zA-Z\d\-_]+)\.png").Groups[1].ToString();
            ResourceManager.LoadSprite("OBSControl", resourceName, ms.ToArray());
        }
    }

    private static void OnConnect(object sender, EventArgs e)
    {
        Logger.Msg($"Connected to OBS v{obs.GetVersion().OBSStudioVersion} at {Main.HostIP.Value}");
        connectButton.transform.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>().text = "Disconnect";
        connectState.Title = "Connection: <color=green>Connected</color>";
        currentScene.Title = $"Scene: {obs.GetCurrentScene().Name}";
        stats.Title = $"Free disk space: {obs.GetStats().FreeDiskSpace} MB";
    }

    private static void OnSceneChanged(OBSWebsocket sender, string sceneName)
    {
        currentScene.Title = $"Scene: {sceneName}";
    }

    private static void StreamStatus(OBSWebsocket sender, StreamStatus status)
    {
        stats.Title = $"FPS: {status.FPS} / DF: {status.DroppedFrames} TF: {status.TotalFrames}";
        streamState.Title = $"Streaming: {(status.Streaming ? "Started" : "Stopped")}";
        recState.Title = $"Recording: {(status.Recording ? "Started" : "Stopped")}";
    }


    private static void OnDisconnect(object sender, EventArgs e)
    {
        Logger.Msg("Disconnected from OBS.");
        connectButton.transform.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>().text = "Connect";
        connectState.Title = "Connection: <color=red>Disconnected</color>";
    }
}