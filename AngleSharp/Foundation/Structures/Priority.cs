namespace AngleSharp
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// A priority object for comparing priorities.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1, CharSet = CharSet.Unicode)]
    public struct Priority : IEquatable<Priority>
    {
        #region Fields

        [FieldOffset(0)]
        Byte inlines;
        [FieldOffset(1)]
        Byte ids;
        [FieldOffset(2)]
        Byte classes;
        [FieldOffset(3)]
        Byte tags;
        [FieldOffset(0)]
        UInt32 priority;

        #endregion

        #region Default

        /// <summary>
        /// Gets the priority for a custom set property.
        /// </summary>
        public static readonly Priority Custom = new Priority(UInt32.MaxValue);

        /// <summary>
        /// Gets the priority for an important property.
        /// </summary>
        public static readonly Priority Important = new Priority(Byte.MaxValue - 1, Byte.MaxValue, Byte.MaxValue, Byte.MaxValue);

        /// <summary>
        /// Gets the lowest (zero) priority.
        /// </summary>
        public static readonly Priority Zero = new Priority(0u);

        /// <summary>
        /// Gets the priority for having a single tag.
        /// </summary>
        public static readonly Priority OneTag = new Priority(0, 0, 0, 1);

        /// <summary>
        /// Gets the priority for having a single class.
        /// </summary>
        public static readonly Priority OneClass = new Priority(0, 0, 1, 0);

        /// <summary>
        /// Gets the priority for having a single Id.
        /// </summary>
        public static readonly Priority OneId = new Priority(0, 1, 0, 0);

        /// <summary>
        /// Gets the priority for having an inline element.
        /// </summary>
        public static readonly Priority Inline = new Priority(1, 0, 0, 0);

        #endregion

        #region ctor

        public Priority(UInt32 priority)
        {
            this.inlines = this.ids = this.classes = this.tags = 0;
            this.priority = priority;
        }

        public Priority(Byte inlines, Byte ids, Byte classes, Byte tags)
        {
            this.priority = 0;
            this.inlines = inlines;
            this.ids = ids;
            this.classes = classes;
            this.tags = tags;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Adds the two given priorities.
        /// </summary>
        /// <param name="a">The first priority.</param>
        /// <param name="b">The second priority.</param>
        /// <returns>The result of adding the two priorities.</returns>
        public static Priority operator +(Priority a, Priority b)
        {
            return new Priority(a.priority + b.priority);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the two do match.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second priority to use.</param>
        /// <returns>True if both priorities are equal, otherwise false.</returns>
        public static Boolean operator ==(Priority a, Priority b)
        {
            return a.priority == b.priority;
        }

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the first one is greater.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second priority to use.</param>
        /// <returns>True if the first priority is higher, otherwise false.</returns>
        public static Boolean operator >(Priority a, Priority b)
        {
            return a.priority > b.priority;
        }

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the first one is greater or equal.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second priority to use.</param>
        /// <returns>True if the first priority is higher or equal, otherwise false.</returns>
        public static Boolean operator >=(Priority a, Priority b)
        {
            return a.priority >= b.priority;
        }

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the second one is greater.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second priority to use.</param>
        /// <returns>True if the second priority is higher, otherwise false.</returns>
        public static Boolean operator <(Priority a, Priority b)
        {
            return a.priority < b.priority;
        }

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the second one is greater or equal.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second priority to use.</param>
        /// <returns>True if the second priority is higher or equal, otherwise false.</returns>
        public static Boolean operator <=(Priority a, Priority b)
        {
            return a.priority <= b.priority;
        }

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the two do not match.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second color to use.</param>
        /// <returns>True if both priorities are not equal, otherwise false.</returns>
        public static Boolean operator !=(Priority a, Priority b)
        {
            return a.priority != b.priority;
        }

        /// <summary>
        /// Checks two priorities for equality.
        /// </summary>
        /// <param name="other">The other priority.</param>
        /// <returns>True if both priorities or equal, otherwise false.</returns>
        public Boolean Equals(Priority other)
        {
            return this.priority == other.priority;
        }

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj is Priority)
                return this.Equals((Priority)obj);

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current priority.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return (Int32)priority;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the priority.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override String ToString()
        {
            return String.Format("({0}, {1}, {2}, {3})", inlines, ids, classes, tags);
        }

        #endregion
    }
}
