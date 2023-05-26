using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Player1.Main.Helpers
{
    public class PageManager
    {
        public static PageManager Instance;
        public PageManager() { }

        public static void AddPage(int PageNum, string button1, string button2, string button3, string button4, string button5, string button6)
        {
            MenuPatch.PageNum = PageNum;
            ButtonSwitch(0, button1);
            ButtonSwitch(1, button2);
            ButtonSwitch(2, button3);
            ButtonSwitch(3, button4);
            ButtonSwitch(4, button5);
            ButtonSwitch(5, button6);
            ButtonSwitch(6, ">>>");
            ButtonSwitch(7, "<<<");
        }
        public static void AddSubPage(int PageNum, string button1, string button2, string button3, string button4, string button5, string button6, string button7)
        {
            MenuPatch.PageNum = PageNum;
            ButtonSwitch(0, button1);
            ButtonSwitch(1, button2);
            ButtonSwitch(2, button3);
            ButtonSwitch(3, button4);
            ButtonSwitch(4, button5);
            ButtonSwitch(5, button6);
            ButtonSwitch(6, button7);
            ButtonSwitch(7, "Back");
        }
        public static void ChangePage(int Page)
        {
            MenuPatch.PageNum = Page;
            MenuPatch.buttonsActive[6] = false;
            MenuPatch.buttonsActive[6] = new bool?(false);
            MenuPatch.buttonsActive[7] = false;
            MenuPatch.buttonsActive[7] = new bool?(false);
            GameObject.Destroy(MenuPatch.menu);
            MenuPatch.menu = null;
            MenuPatch.CheckPages = true;

        }
        public static void ResetPage(int button)
        {
            MenuPatch.buttonsActive[button] = false;
            MenuPatch.buttonsActive[button] = new bool?(false);
            GameObject.Destroy(MenuPatch.menu);
            MenuPatch.menu = null;
            MenuPatch.CheckPages = true;
        }
        internal static void ButtonSwitch(int button, string name)
        {
            MenuPatch.buttons[button] = name;
        }


    }
}
