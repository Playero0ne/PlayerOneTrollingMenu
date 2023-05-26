using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Player1.Components
{
    public class TagAllBecauseihaveto: MonoBehaviour
    {
        public static void TeleportMe(Vector3 PlaceToGo, float rotation)
        {
            var rig = GorillaTagger.Instance.myVRRig;
            rig.enabled = false;
            rig.transform.position = PlaceToGo;
        }
        public void startstuff()
        {
            base.StartCoroutine(this.Startrandom());
        }
        public static VRRig FindRigByPlayer(Player player)
        {
            foreach (VRRig vrrig2 in GorillaParent.instance.vrrigs)
            {
                if (!vrrig2.isOfflineVRRig && vrrig2.GetComponent<PhotonView>().Owner == player)
                {
                    return vrrig2;
                }
            }
            VRRig vrrig;
            if (GorillaParent.instance.vrrigDict.TryGetValue(player, out vrrig))
            {
                return vrrig;
            }
            if (player == null)
            {
                return null;
            }
            return null;
        }
        private IEnumerator Startrandom()
        {
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                var rig = FindRigByPlayer(p);
                GorillaTagManager gorillaTagManager = UnityEngine.Object.FindObjectOfType<GorillaTagManager>();
                if (!gorillaTagManager.currentInfected.Contains(p) && p != PhotonNetwork.LocalPlayer)
                {
                    int num;
                    for (int i = 0; i < 5; i = num + 1)
                    {
                        TeleportMe(rig.headMesh.transform.position, 0f);
                        yield return new WaitForSeconds(0.1f);
                        num = i;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                rig = null;
                gorillaTagManager = null;
            }
            Player[] array = null;
            GorillaTagger.Instance.myVRRig.enabled = true;
            yield break;
        }

    }
}
