namespace AngleSharp.Dom.Events
{
    using System;

    /// <summary>
    /// A couple of useful extensions for the modifier list.
    /// </summary>
    static class ModifierExtensions
    {
        public static Boolean IsCtrlPressed(this String modifierList)
        {
            return false;
        }

        public static Boolean IsMetaPressed(this String modifierList)
        {
            return false;
        }

        public static Boolean IsShiftPressed(this String modifierList)
        {
            return false;
        }

        public static Boolean IsAltPressed(this String modifierList)
        {
            return false;
        }

        public static Boolean ContainsKey(this String modifierList, String key)
        {
            return modifierList.Contains(key);
        }
    }
}
