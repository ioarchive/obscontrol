using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using UnityEngine;
using UnityEngine.UI;

namespace OBSControl;

public class OBSMenu
{
    internal static OBSWebsocket obs;
    
    /*private static string connectState, streamState, recState, currentScene, stats;*/

    public static IEnumerator OnUiReady()
    {
        while (GameObject.Find("Cohtml/QuickMenu") == null) yield return null;
        
        Logger.Msg("hi");
        
        //TODO: ui things
        
        obs = new OBSWebsocket();

        obs.Connected += OnConnect;
        obs.Disconnected += OnDisconnect;
        obs.SceneChanged += OnSceneChanged;
        obs.StreamStatus += StreamStatus;
        

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
            Logger.Error(msg);
        }
        catch (ErrorResponseException ex)
        {
            msg = $"Failed to connect to server {serverAddress}: {ex.Message}.";
            Logger.Error(msg);
        }
        catch (Exception ex)
        {
            msg = $"Failed to connect to server {serverAddress}: {ex.Message}.";
            Logger.Error(msg);
        }

        return Task.CompletedTask;
    }

    private static void OnConnect(object sender, EventArgs e)
    {
        Logger.Msg($"Connected to OBS v{obs.GetVersion().OBSStudioVersion} at {Main.HostIP.Value}");
        /*connectButton.transform.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>().text = "Disconnect";
        connectState.Title = "Connection: <color=green>Connected</color>";
        currentScene.Title = $"Scene: {obs.GetCurrentScene().Name}";
        stats.Title = $"Free disk space: {obs.GetStats().FreeDiskSpace} MB";*/
    }

    private static void OnSceneChanged(OBSWebsocket sender, string sceneName)
    {
        /*currentScene.Title = $"Scene: {sceneName}";*/
    }

    private static void StreamStatus(OBSWebsocket sender, StreamStatus status)
    {
        /*stats.Title = $"FPS: {status.FPS} / DF: {status.DroppedFrames} TF: {status.TotalFrames}";
        streamState.Title = $"Streaming: {(status.Streaming ? "Started" : "Stopped")}";
        recState.Title = $"Recording: {(status.Recording ? "Started" : "Stopped")}";*/
    }


    private static void OnDisconnect(object sender, EventArgs e)
    {
        Logger.Msg("Disconnected from OBS.");
        /*connectButton.transform.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>().text = "Connect";
        connectState.Title = "Connection: <color=red>Disconnected</color>";*/
    }
}