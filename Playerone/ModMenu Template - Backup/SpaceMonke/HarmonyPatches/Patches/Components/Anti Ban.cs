using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Player1.Components
{
    public class Anti_Ban
    {
        public static bool onstart = true;
        public static void StartAntiBan()
        {
            Player p = PhotonNetwork.LocalPlayer;
            RequestableOwnershipGuard guard = new RequestableOwnershipGuard();
            GorillaGameManager manager = GorillaGameManager.instance;
            if (onstart)
            {
                guard.currentOwner = null;
            }
            if (PhotonNetwork.InRoom)
            {
                guard.photonView.RPC("AdminOnlySetOwner", RpcTarget.MasterClient, p);
                guard.TransferOwnership(manager.photonView.Owner);
                manager.photonView.ControllerActorNr = p.ActorNumber;
                manager.photonView.OwnerActorNr = p.ActorNumber;
                guard.TransferOwnership(GorillaNot.instance.photonView.Owner);
                PhotonNetwork.CurrentRoom.masterClientId = p.ActorNumber;
                GorillaNot.instance.photonView.RequestOwnership();
                Clear();
            }

        }
        public static void setOwnership(PhotonView view)
        {
            Player p = PhotonNetwork.LocalPlayer;
            RequestableOwnershipGuard guard = new RequestableOwnershipGuard();
            if (view == null) { return; }
            if (PhotonNetwork.InRoom)
            {
                StartAntiBan();
                guard.TransferOwnership(view.Owner);
                view.ControllerActorNr = p.ActorNumber;
                view.OwnerActorNr = p.ActorNumber;
            }
        }
        private static void Clear()
        {
            Player p = PhotonNetwork.LocalPlayer;
            PhotonNetwork.RemoveRPCs(p);
            PhotonNetwork.RemoveBufferedRPCs(p.ActorNumber);
            GorillaNot.instance.rpcCallLimit = int.MaxValue;
        }
    }
}