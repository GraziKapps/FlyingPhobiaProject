using UnityEngine;
using System.Collections;


public class NetworkManager : MonoBehaviour {

    private const string typeName = "UniqueGameName";
    private const string gameName = "RoomName";
    [SerializeField]
    private MessageManager messageManager;
    InformationSaver instance;
    
   // NetworkView networkView;


    void Awake()
    {
       // messageManager = new MessageManager();
        instance = InformationSaver.getInstance();
        #if UNITY_EDITOR
            Debug.Log("Unity Editor");
           
            instance.IsNetworked = true;
        #endif

        if (instance.IsNetworked == true)
        {

            MasterServer.ipAddress = Network.player.ipAddress;
            MasterServer.port = 23466;
            MasterServer.dedicatedServer = true;
            // }
            Debug.Log("Caralho");
            Application.OpenURL((Application.dataPath) + "/MS/MS.exe");
            Debug.Log(Application.dataPath);
            StartCoroutine(waitSecondsAndStartServer());
        }

    }

    IEnumerator waitSecondsAndStartServer()
    {
       
       
        yield return new WaitForSeconds(20);
        StartServer();
       
    }


    private void StartServer()
    {
       // if (InformationSaver.getInstance().IsNetworked == true)
       // {
           
                // Network.InitializeServer();
                //Network.
                //MasterServer.RegisterHost(typeName, gameName);
                Network.InitializeServer(4, 25000, false);

            MasterServer.RegisterHost(typeName, gameName);
        //}
    }


    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
    }


    void OnGUI()
    {
        if (instance.IsNetworked == true)
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(0, 100, 250, 100), Network.player.ipAddress);
        }
          /*  if (!Network.isClient && !Network.isServer)
            {
                if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                    StartServer();

                if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
                    RefreshHostList();






                if (hostList != null)
                {
                    for (int i = 0; i < hostList.Length; i++)
                    {
                        if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                            JoinServer(hostList[i]);
                    }
                }


            }

            if (GUI.Button(new Rect(400, 250, 250, 100), "Teste"))
                GetComponent<NetworkView>().RPC("ReceiveSimpleMessage", RPCMode.Server, "Hello world");
        */
    }

    private HostData[] hostList;

    private void RefreshHostList()
    {
        MasterServer.RequestHostList(typeName);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }


    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    [RPC]
    void ReceiveSimpleMessage(string teste,NetworkMessageInfo info)
    {
        Debug.Log(teste + " from " + info.sender);

       // if (networkView.isMine)
            
    }

    [RPC]
    void receiveTurbulenceChange(string toggle)
    {
        Debug.Log("Active turbulence toggle is  " + toggle);
        messageManager.handleTurbulence(toggle);
        //send
    }

    [RPC]
    void receiveStressChange(string toggle)
    {
        Debug.Log("Active stress toggle is  " + toggle);
        messageManager.handleStressLevels(toggle);
        //send
    }
    [RPC]
    void receiveTimeChange(string time)
    {
        Debug.Log("New time is  " + time);
        messageManager.handleTime(time);
        //send
    }


    [RPC]
    void receiveClimateChange(int clima)
    {
        Debug.Log("received >" + clima);

        messageManager.handleClimate(clima);
        // if (networkView.isMine)

    }

    [RPC]
    void receiveLapseChange(string lapse)
    {
        Debug.Log("New lapse is  " + lapse);
        messageManager.handleNewLape(lapse);

    }

    [RPC]
    void receiveDownChange(bool isDown)
    {
        Debug.Log("ITS GOING DOWN BOYS");
        messageManager.handleDown();
    }

    void OnConnectedToServer()
    {
        Debug.Log("Server Joined");
    }
}
