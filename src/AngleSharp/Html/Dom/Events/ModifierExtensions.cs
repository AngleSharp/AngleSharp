namespace AngleSharp.Html.Dom.Events
{
    using System;

    /// <summary>
    /// A couple of useful extensions for the modifier list.
    /// </summary>
    static class ModifierExtensions
    {
        private const String Control = "Control";
        private const String Shift = "Shift";
        private const String Alt = "Alt";
        private const String Meta = "Meta";

        public static Boolean IsCtrlPressed(this String modifierList) => modifierList.ContainsKey(Control);

        public static Boolean IsMetaPressed(this String modifierList) => modifierList.ContainsKey(Meta);

        public static Boolean IsShiftPressed(this String modifierList) => modifierList.ContainsKey(Shift);

        public static Boolean IsAltPressed(this String modifierList) => modifierList.ContainsKey(Alt);

        public static Boolean ContainsKey(this String modifierList, String key)
        {
            foreach (var modifier in modifierList.Split(' '))
            {
                if (String.Equals(modifier, key, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static String ToModifierList(Boolean ctrlKey, Boolean shiftKey, Boolean altKey, Boolean metaKey)
        {
            var list = new System.Collections.Generic.List<String>();

            if (ctrlKey)
            {
                list.Add(Control);
            }

            if (shiftKey)
            {
                list.Add(Shift);
            }

            if (altKey)
            {
                list.Add(Alt);
            }

            if (metaKey)
            {
                list.Add(Meta);
            }

            return String.Join(" ", list);
        }
    }
}
