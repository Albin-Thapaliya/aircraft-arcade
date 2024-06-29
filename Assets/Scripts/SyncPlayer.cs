using Photon.Pun;
using UnityEngine;

public class SyncPlayer : MonoBehaviourPun, IPunObservable
{
    private Vector3 smoothMove;
    private Quaternion smoothRotate;

    private void Start()
    {
        smoothMove = transform.position;
        smoothRotate = transform.rotation;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            smoothMove = (Vector3)stream.ReceiveNext();
            smoothRotate = (Quaternion)stream.ReceiveNext();
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, smoothRotate, Time.deltaTime * 10);
        }
    }
}