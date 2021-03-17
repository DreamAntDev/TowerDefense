using UnityEditor;
using UnityEngine;

namespace Gpm.Manager.Ui.Helper
{
    internal static class ManagerUiIcon
    {
        public const string WAIT_SPIN = "WaitSpin";

        public static readonly Texture2D CHECK_ICON = EditorGUIUtility.FindTexture("CollabNew");
        public static readonly Texture2D REFRESH_ICON = EditorGUIUtility.FindTexture("d_Refresh");
        public static readonly Texture2D INFOMATION_ICON = EditorGUIUtility.FindTexture("ClothInspector.SelectTool");

        private static Texture2D[] statusWheel;
        private static int statusWheelFrame;

        public static Texture2D StatusWheel
        {
            get
            {
                if (statusWheel == null)
                {
                    statusWheel = new Texture2D[12];
                    for (int i = 0; i < 12; i++)
                    {
                        statusWheel[i] = EditorGUIUtility.Load(WAIT_SPIN + i.ToString("00")) as Texture2D;
                    }
                }

                statusWheelFrame = (int)Mathf.Repeat(Time.realtimeSinceStartup * 10, 11.99f);

                if (statusWheelFrame == 12)
                {
                    statusWheelFrame = 0;
                }

                return statusWheel[statusWheelFrame++];
            }
        }
    }
}
