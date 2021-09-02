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

    void Update()
    {

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
