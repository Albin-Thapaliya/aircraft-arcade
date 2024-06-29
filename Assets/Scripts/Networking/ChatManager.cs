using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPun
{
    public InputField chatInput;
    public Text chatHistory;
    private const int maxMessages = 100;

    public void SendChatMessage()
    {
        string message = chatInput.text;
        if (!string.IsNullOrEmpty(message))
        {
            photonView.RPC("ReceiveMessage", RpcTarget.All, message);
            chatInput.text = "";
        }
    }

    [PunRPC]
    void ReceiveMessage(string message)
    {
        if (chatHistory.text.Length > maxMessages * 100)
        {
            chatHistory.text = "";
        }

        chatHistory.text += message + "\n";
    }
}