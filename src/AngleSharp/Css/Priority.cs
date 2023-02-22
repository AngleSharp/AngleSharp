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
            Inlines = Ids = Classes = Tags = 0;
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
            Inlines = inlines;
            Ids = ids;
            Classes = classes;
            Tags = tags;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of tags for this priority.
        /// </summary>
        [field: FieldOffset(0)]
        public Byte Tags { get; }

        /// <summary>
        /// Gets the number of classes for this priority.
        /// </summary>
        [field: FieldOffset(1)]
        public Byte Classes { get; }

        /// <summary>
        /// Gets the number of ids for this priority.
        /// </summary>
        [field: FieldOffset(2)]
        public Byte Ids { get; }

        /// <summary>
        /// Gets the number of inlines for this priority.
        /// </summary>
        [field: FieldOffset(3)]
        public Byte Inlines { get; }

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
        public override Boolean Equals(Object? obj) => obj is Priority other && Equals(other);

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
        public override String ToString() => $"({Inlines}, {Ids}, {Classes}, {Tags})";

        #endregion
    }
}
