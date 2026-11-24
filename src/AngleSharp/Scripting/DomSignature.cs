namespace AngleSharp.Scripting
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// What an overload of an operation or constructor takes. The parameter types are the CLR
    /// types the arguments would be converted to, which is what WebIDL's overload resolution
    /// matches the arguments of a call against.
    /// </summary>
    public sealed class DomSignature
    {
        private readonly Type[] _parameters;

        /// <summary>
        /// Creates a signature whose parameters are all required.
        /// </summary>
        /// <param name="parameters">The types of the parameters.</param>
        public DomSignature(params Type[] parameters)
            : this(parameters?.Length ?? 0, false, parameters)
        {
        }

        /// <summary>
        /// Creates a signature whose leading parameters are required.
        /// </summary>
        /// <param name="required">How many of the parameters are required.</param>
        /// <param name="parameters">The types of the parameters.</param>
        public DomSignature(Int32 required, params Type[] parameters)
            : this(required, false, parameters)
        {
        }

        /// <summary>
        /// Creates a signature whose leading parameters are required, and whose last one may
        /// take any number of arguments.
        /// </summary>
        /// <param name="required">How many of the parameters are required.</param>
        /// <param name="variadic">If the last parameter takes the remaining arguments.</param>
        /// <param name="parameters">The types of the parameters, the variadic one by its array type.</param>
        public DomSignature(Int32 required, Boolean variadic, params Type[]? parameters)
        {
            _parameters = parameters ?? Array.Empty<Type>();

            if (required < 0 || required > _parameters.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(required));
            }

            if (variadic && (_parameters.Length == 0 || !_parameters[_parameters.Length - 1].IsArray))
            {
                throw new ArgumentException("A variadic signature ends with an array type.", nameof(variadic));
            }

            Required = required;
            IsVariadic = variadic;
        }

        /// <summary>
        /// Gets how many of the parameters are required.
        /// </summary>
        public Int32 Required { get; }

        /// <summary>
        /// Gets if the last parameter takes the remaining arguments.
        /// </summary>
        public Boolean IsVariadic { get; }

        /// <summary>
        /// Gets the types of the parameters.
        /// </summary>
        public IReadOnlyList<Type> Parameters => _parameters;
    }
}
