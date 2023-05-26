using System;
using UnityEngine;

namespace Player1.Main.Helpers
{
    // Token: 0x02000004 RID: 4
    internal class BtnCollider : MonoBehaviour
    {
        // Token: 0x0600000B RID: 11 RVA: 0x00003044 File Offset: 0x00001244
        private void OnTriggerEnter(Collider collider)
        {
            bool flag = Time.frameCount >= MenuPatch.framePressCooldown + 30;
            if (flag)
            {
                    MenuPatch.Toggle(this.relatedText);
                    MenuPatch.framePressCooldown = Time.frameCount;
            }
        }

        // Token: 0x04000017 RID: 23
        public string relatedText;
    }
}
