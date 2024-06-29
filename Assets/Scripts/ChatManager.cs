using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPun
{
    public InputField chatInput;
    public Text chatHistory;
    public int maxMessages = 25;
    private Queue<string> messageQueue = new Queue<string>();

    public void SendChatMessage()
    {
        string message = chatInput.text;
        if (!string.IsNullOrEmpty(message))
        {
            photonView.RPC("ReceiveMessage", RpcTarget.All, PhotonNetwork.Nickname + ": " + message);
            chatInput.text = "";
            chatInput.ActivateInputField();
        }
    }

    [PunRPC]
    void ReceiveMessage(string message)
    {
        if (messageQueue.Count >= maxMessages)
        {
            messageQueue.Dequeue();
        }
        messageQueue.Enqueue(message);

        chatHistory.text = string.Join("\n", messageQueue.ToArray());
    }
}