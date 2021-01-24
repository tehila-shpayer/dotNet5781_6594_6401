using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL
{
    public static class PreviewKeyDown
    {
        /// <summary>
        /// בדיקת תקינות קלט  כללית
        /// הפונקציה בודקת שהקלט מספר חיובי שלם
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void GeneralPerviewKeyDown(object sender, KeyEventArgs e)//allow to enter only digits to the textBox
        {
            TextBox t = sender as TextBox;
            if (t == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
            {
                if (!Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift))
                    return;
            }
            e.Handled = true;//if it is not a digit or a control key, the letter would not be writen
            return;
        }
    }
}
