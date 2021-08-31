using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    public NetworkManager manager;

    public InputField ipField;

    public GameObject startPanel;
    public GameObject connectingPanel;
    public GameObject connnectedPanel;

    /*
    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }*/


    void Update()
    {
        /*
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            TogglePanel(startPanel);
        }
        else
        {
            //StatusLabels();
            TogglePanel(connnectedPanel);
        }*/
        // client ready
        /*
        if (NetworkClient.isConnected && !NetworkClient.ready)
        {
            NetworkClient.Ready();
            if (NetworkClient.localPlayer == null)
            {
                NetworkClient.AddPlayer();
            }
        }*/

        if (!NetworkClient.isConnected)
        {
            TogglePanel(startPanel);
        }
        else
        {
            TogglePanel(connnectedPanel);
        }

        if (NetworkClient.isConnecting)
        {
            TogglePanel(connectingPanel);
        }


    }



    /*
    void StartButtons()
    {
        if (!NetworkClient.active)
        {
            // Server + Client
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                if (GUILayout.Button("Host (Server + Client)"))
                {
                    manager.StartHost();
                }
            }

            // Client + IP
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Client"))
            {
                manager.StartClient();
            }
            manager.networkAddress = GUILayout.TextField(manager.networkAddress);
            GUILayout.EndHorizontal();

            // Server Only
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                // cant be a server in webgl build
                GUILayout.Box("(  WebGL cannot be server  )");
            }
            else
            {
                if (GUILayout.Button("Server Only")) manager.StartServer();
            }
        }
        else
        {
            // Connecting
            GUILayout.Label("Connecting to " + manager.networkAddress + "..");
            if (GUILayout.Button("Cancel Connection Attempt"))
            {
                manager.StopClient();
            }
        }
    }

    void StatusLabels()
    {
        // host mode
        // display separately because this always confused people:
        //   Server: ...
        //   Client: ...
        if (NetworkServer.active && NetworkClient.active)
        {
            GUILayout.Label($"<b>Host</b>: running via {Transport.activeTransport}");
        }
        // server only
        else if (NetworkServer.active)
        {
            GUILayout.Label($"<b>Server</b>: running via {Transport.activeTransport}");
        }
        // client only
        else if (NetworkClient.isConnected)
        {
            GUILayout.Label($"<b>Client</b>: connected to {manager.networkAddress} via {Transport.activeTransport}");
        }
    }

    
    void StopButtons()
    {
        // stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            if (GUILayout.Button("Stop Host"))
            {
                manager.StopHost();
            }
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            if (GUILayout.Button("Stop Client"))
            {
                manager.StopClient();
            }
        }
        // stop server if server-only
        else if (NetworkServer.active)
        {
            if (GUILayout.Button("Stop Server"))
            {
                manager.StopServer();
            }
        }
    }*/

    public void TogglePanel(GameObject panel)
    {
        List<GameObject> panels = new List<GameObject> { connectingPanel, connnectedPanel, startPanel };

        for (int i = 0; i < panels.Count; i++)
        {
            if (panel == panels[i])
            {
                if (!panel.activeInHierarchy)
                {
                    panel.SetActive(true);
                }
            }
            else
            {
                if (panels[0].activeInHierarchy)
                {
                    panels[0].SetActive(false);
                }
            }
        }

        connectingPanel.SetActive(connectingPanel == panel);
        connnectedPanel.SetActive(connnectedPanel == panel);
        startPanel.SetActive(startPanel == panel);
    }

    public void Host()
    {
        manager.StartHost();
    }

    public void Join()
    {
        manager.networkAddress = ipField.text;
        manager.StartClient();
    }

    public void CancelJoin()
    {
        manager.StopClient();
    }

    public void Leave()
    {
        // stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            manager.StopHost();
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            manager.StopClient();

        }
        // stop server if server-only
        else if (NetworkServer.active)
        {
            manager.StopServer();
        }
    }
}
