namespace AngleSharp.Scripting
{
    using System;

    /// <summary>
    /// A function script passed to a registered operation.
    /// </summary>
    /// <remarks>
    /// A registration adapts this object to the delegate type the DOM member takes, in code
    /// written for it, e.g. <c>args.GetCallback(0) is DomCallback cb ? (a, b) => cb.Invoke&lt;Boolean&gt;(a, b) : null</c>.
    /// That needs neither reflection nor compiled expressions, so it works under native AoT.
    /// </remarks>
    public abstract class DomCallback
    {
        /// <summary>
        /// Calls the function with undefined as this.
        /// </summary>
        /// <param name="arguments">The arguments to pass.</param>
        /// <returns>The result of the function, or null if it threw.</returns>
        public Object? Invoke(params Object?[] arguments) => CallCore(null, arguments ?? Array.Empty<Object?>(), null);

        /// <summary>
        /// Calls the function with undefined as this, converting its result as a DOM member
        /// converts a value script gives it - what a delegate returning a value needs.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="arguments">The arguments to pass.</param>
        /// <returns>The converted result, or the default of the type if it threw.</returns>
        public TResult? Invoke<TResult>(params Object?[] arguments) =>
            CallCore(null, arguments ?? Array.Empty<Object?>(), typeof(TResult)) is TResult result ? result : default;

        /// <summary>
        /// Calls the function.
        /// </summary>
        /// <param name="thisArgument">The receiver of the call.</param>
        /// <param name="arguments">The arguments to pass.</param>
        /// <returns>The result of the function, or null if it threw.</returns>
        public Object? Call(Object? thisArgument, params Object?[] arguments) => CallCore(thisArgument, arguments ?? Array.Empty<Object?>(), null);

        /// <summary>
        /// Calls the function. What it throws is reported to the window like an error of a
        /// listener, as nothing on the CLR side could catch it meaningfully.
        /// </summary>
        /// <param name="thisArgument">The receiver of the call.</param>
        /// <param name="arguments">The arguments to pass.</param>
        /// <param name="resultType">The type to convert the result to, or null to take it as it is.</param>
        /// <returns>The result of the function, or null if it threw.</returns>
        protected internal abstract Object? CallCore(Object? thisArgument, Object?[] arguments, Type? resultType);
    }
}
