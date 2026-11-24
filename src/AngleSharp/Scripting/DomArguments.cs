namespace AngleSharp.Scripting
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The arguments script passed to a registered operation, converted on request.
    /// </summary>
    /// <remarks>
    /// A struct handed by value, so a call costs no allocation for it. The values stay in the
    /// engine's own form (the state) and are only converted by the engine's reader when asked
    /// for, so this type knows nothing of any particular engine. The arguments are only valid
    /// during the call they were passed to - an engine may reuse their storage afterwards.
    /// </remarks>
    public readonly struct DomArguments
    {
        private readonly DomArgumentReader _reader;
        private readonly Object _state;
        private readonly Int32 _length;

        /// <summary>
        /// Creates the arguments of a call. Called by a scripting engine.
        /// </summary>
        /// <param name="reader">The reader converting the values of the engine.</param>
        /// <param name="state">The values in the engine's form, handed back to the reader.</param>
        /// <param name="length">The number of arguments passed.</param>
        public DomArguments(DomArgumentReader reader, Object state, Int32 length)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _state = state;
            _length = length;
        }

        /// <summary>
        /// Gets the number of arguments passed.
        /// </summary>
        public Int32 Length => _length;

        /// <summary>
        /// Gets the window of the engine, which some operations take implicitly.
        /// </summary>
        public IWindow? Window => _reader?.Window;

        /// <summary>
        /// Gets if the argument was passed, and is not undefined - the test WebIDL applies to an
        /// optional argument.
        /// </summary>
        /// <param name="index">The position of the argument.</param>
        /// <returns>True if there is a value for it.</returns>
        public Boolean Has(Int32 index) => InRange(index) && _reader.IsPresent(_state, index);

        /// <summary>
        /// Gets the argument of a required parameter converted to the given type. Whatever was
        /// passed is converted, undefined included (a DOMString then is "undefined", as in
        /// browsers); only an argument that was not passed at all gives the default of the type.
        /// </summary>
        /// <typeparam name="TValue">The type of the parameter.</typeparam>
        /// <param name="index">The position of the argument.</param>
        /// <returns>The converted argument.</returns>
        public TValue? Get<TValue>(Int32 index) =>
            InRange(index) && _reader.Convert(_state, index, typeof(TValue)) is TValue value ? value : default;

        /// <summary>
        /// Gets the argument of an optional parameter converted to the given type, or the
        /// fallback if it was not passed or is undefined.
        /// </summary>
        /// <typeparam name="TValue">The type of the parameter.</typeparam>
        /// <param name="index">The position of the argument.</param>
        /// <param name="fallback">The default value of the optional parameter.</param>
        /// <returns>The converted argument.</returns>
        public TValue? Get<TValue>(Int32 index, TValue? fallback) =>
            Has(index) && _reader.Convert(_state, index, typeof(TValue)) is TValue value ? value : fallback;

        /// <summary>
        /// Gets a member of a dictionary argument (e.g. the bubbles of an EventInit) converted to
        /// the given type, or the fallback if the argument is no object, or the member is missing
        /// or undefined. This is how a parameter of a [DomInitDict] method is filled.
        /// </summary>
        /// <typeparam name="TValue">The type of the parameter the member is for.</typeparam>
        /// <param name="index">The position of the dictionary argument.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="fallback">The value if the member is not given.</param>
        /// <returns>The converted member.</returns>
        public TValue? GetMember<TValue>(Int32 index, String name, TValue? fallback) =>
            InRange(index) && _reader.TryGetMember(_state, index, name, typeof(TValue), out var value) && value is TValue typed ? typed : fallback;

        /// <summary>
        /// Gets the argument as a callback, or null if it is not callable.
        /// </summary>
        /// <param name="index">The position of the argument.</param>
        /// <returns>The callback to invoke.</returns>
        public DomCallback? GetCallback(Int32 index) => InRange(index) ? _reader.GetCallback(_state, index) : null;

        //  the reader is null for default(DomArguments), which is then a call without arguments
        private Boolean InRange(Int32 index) => _reader != null && index >= 0 && index < _length;
    }

    /// <summary>
    /// Converts the arguments of a call from the form a scripting engine keeps them in. Implemented
    /// by the engine, once per engine rather than per call.
    /// </summary>
    public abstract class DomArgumentReader
    {
        /// <summary>
        /// Gets the window of the engine.
        /// </summary>
        protected internal abstract IWindow? Window { get; }

        /// <summary>
        /// Gets if the argument at the index is not undefined. The index is in range.
        /// </summary>
        /// <param name="state">The values of the call.</param>
        /// <param name="index">The position of the argument.</param>
        /// <returns>True if there is a value for it.</returns>
        protected internal abstract Boolean IsPresent(Object state, Int32 index);

        /// <summary>
        /// Converts the argument at the index to the given type, as a DOM parameter of the type
        /// takes it - undefined included. The index is in range.
        /// </summary>
        /// <param name="state">The values of the call.</param>
        /// <param name="index">The position of the argument.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <returns>The converted argument.</returns>
        protected internal abstract Object? Convert(Object state, Int32 index, Type type);

        /// <summary>
        /// Converts a member of the object at the index to the given type. The index is in range.
        /// </summary>
        /// <param name="state">The values of the call.</param>
        /// <param name="index">The position of the argument.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="type">The type of the parameter the member is for.</param>
        /// <param name="value">The converted member.</param>
        /// <returns>False if the argument is no object, or the member is missing or undefined.</returns>
        protected internal abstract Boolean TryGetMember(Object state, Int32 index, String name, Type type, out Object? value);

        /// <summary>
        /// Gets the argument at the index as a callback. The index is in range.
        /// </summary>
        /// <param name="state">The values of the call.</param>
        /// <param name="index">The position of the argument.</param>
        /// <returns>The callback, or null if the argument is not callable.</returns>
        protected internal abstract DomCallback? GetCallback(Object state, Int32 index);
    }
}
