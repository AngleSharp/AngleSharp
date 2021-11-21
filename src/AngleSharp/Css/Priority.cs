namespace AngleSharp.Css
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// A priority object for comparing priorities.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1, CharSet = CharSet.Unicode)]
    public readonly struct Priority : IEquatable<Priority>, IComparable<Priority>
    {
        #region Fields

        [FieldOffset(0)]
        private readonly Byte _tags;
        [FieldOffset(1)]
        private readonly Byte _classes;
        [FieldOffset(2)]
        private readonly Byte _ids;
        [FieldOffset(3)]
        private readonly Byte _inlines;
        [FieldOffset(0)]
        private readonly UInt32 _priority;

        #endregion

        #region Default

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

        /// <summary>
        /// Creates a new priority with the given hashcode.
        /// </summary>
        /// <param name="priority">The hashcode to use.</param>
        public Priority(UInt32 priority)
        {
            _inlines = _ids = _classes = _tags = 0;
            _priority = priority;
        }

        /// <summary>
        /// Creates a new priority with the given values.
        /// </summary>
        /// <param name="inlines">The number of inlines.</param>
        /// <param name="ids">The number of ids.</param>
        /// <param name="classes">The number of classes.</param>
        /// <param name="tags">The number of tags.</param>
        public Priority(Byte inlines, Byte ids, Byte classes, Byte tags)
        {
            _priority = 0;
            _inlines = inlines;
            _ids = ids;
            _classes = classes;
            _tags = tags;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of tags for this priority.
        /// </summary>
        public Byte Tags => _tags;

        /// <summary>
        /// Gets the number of classes for this priority.
        /// </summary>
        public Byte Classes => _classes;

        /// <summary>
        /// Gets the number of ids for this priority.
        /// </summary>
        public Byte Ids => _ids;

        /// <summary>
        /// Gets the number of inlines for this priority.
        /// </summary>
        public Byte Inlines => _inlines;

        #endregion

        #region Operators

        /// <summary>
        /// Adds the two given priorities.
        /// </summary>
        /// <param name="a">The first priority.</param>
        /// <param name="b">The second priority.</param>
        /// <returns>The result of adding the two priorities.</returns>
        public static Priority operator +(Priority a, Priority b) => new Priority(a._priority + b._priority);

        #endregion

        #region Equality

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the two do match.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second priority to use.</param>
        /// <returns>True if both priorities are equal, otherwise false.</returns>
        public static Boolean operator ==(Priority a, Priority b) => a._priority == b._priority;

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the first one is greater.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second priority to use.</param>
        /// <returns>True if the first priority is higher, otherwise false.</returns>
        public static Boolean operator >(Priority a, Priority b) => a._priority > b._priority;

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the first one is greater or equal.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second priority to use.</param>
        /// <returns>True if the first priority is higher or equal, otherwise false.</returns>
        public static Boolean operator >=(Priority a, Priority b) => a._priority >= b._priority;

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the second one is greater.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second priority to use.</param>
        /// <returns>True if the second priority is higher, otherwise false.</returns>
        public static Boolean operator <(Priority a, Priority b) => a._priority < b._priority;

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the second one is greater or equal.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second priority to use.</param>
        /// <returns>True if the second priority is higher or equal, otherwise false.</returns>
        public static Boolean operator <=(Priority a, Priority b) => a._priority <= b._priority;

        /// <summary>
        /// Compares two priorities and returns a boolean indicating if the two do not match.
        /// </summary>
        /// <param name="a">The first priority to use.</param>
        /// <param name="b">The second color to use.</param>
        /// <returns>True if both priorities are not equal, otherwise false.</returns>
        public static Boolean operator !=(Priority a, Priority b) => a._priority != b._priority;

        /// <summary>
        /// Checks two priorities for equality.
        /// </summary>
        /// <param name="other">The other priority.</param>
        /// <returns>True if both priorities or equal, otherwise false.</returns>
        public Boolean Equals(Priority other) => _priority == other._priority;

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
#if NET5_0_OR_GREATER
        public override Boolean Equals(Object? obj) => obj is Priority other && Equals(other);
#else
        public override Boolean Equals(Object obj) => obj is Priority other && Equals(other);
#endif

        /// <summary>
        /// Returns a hash code that defines the current priority.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => (Int32)_priority;

        /// <summary>
        /// Compares the current priority with another priority.
        /// </summary>
        /// <param name="other">The priority to compare to.</param>
        /// <returns>A value greater than 1 if the current priority is larger.</returns>
        public Int32 CompareTo(Priority other) => this == other ? 0 : (this > other ? 1 : -1);

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the priority.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override String ToString() => $"({_inlines}, {_ids}, {_classes}, {_tags})";

        #endregion
    }
}
