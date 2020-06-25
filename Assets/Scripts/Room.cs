using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Room : MonoBehaviour
{

    [SerializeField] Vector3 playerSpawnPos;
    [SerializeField] GameObject X;
    [SerializeField] GameObject O;

    bool x = true, o = true;
    public InputField odp;
    public string ipAdress;
    GameObject choosenCharacter; 
    public NetworkManager manager;
    // Use this for initialization
     public Text hintText;
    private void Start()
    {
        GetLocalIPAddress();
    }
    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                hintText.text = ip.ToString();
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    public void XCharChooser()
    {
        if (x)
        {
            choosenCharacter = X;
            playerSpawnPos = new Vector3(-112.7f, 4f, -121.4f);
            x = false;
            JoinGame();
        }
    }

    public void OCharChoose()
    {
        if (o)
        {
            choosenCharacter = O;
            playerSpawnPos = new Vector3(0f, 0f, 0f);
            o = false;
            JoinGame();
        }
    }

    public void StartupHost()
    {
        
        SetPort();
        NetworkManager.singleton.StartHost();
    }

  

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
        manager.StartClient();
    }

    void SetIPAddress()
    {
      
        ipAdress = odp.text.ToString();
        NetworkManager.singleton.networkAddress = ipAdress;

    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

  

  

public  void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {

        var player = (GameObject)GameObject.Instantiate(choosenCharacter, playerSpawnPos, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}



