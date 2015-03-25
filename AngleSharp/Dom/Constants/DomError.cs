namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// A collection of official DOM error codes.
    /// </summary>
    public enum DomError : ushort
    {
        /// <summary>
        /// The index is not in the allowed range.
        /// </summary>
        [DomDescription("The index is not in the allowed range.")]
        [DomName("INDEX_SIZE_ERR")]
        IndexSizeError = 0x1,
        /// <summary>
        /// The size of the string is invalid.
        /// </summary>
        [DomDescription("The size of the string is invalid.")]
        [DomName("DOMSTRING_SIZE_ERR")]
        [DomHistorical]
        DomStringSize = 0x2,
        /// <summary>
        /// The operation would yield an incorrect node tree.
        /// </summary>
        [DomDescription("The operation would yield an incorrect node tree.")]
        [DomName("HIERARCHY_REQUEST_ERR")]
        HierarchyRequest = 0x3,
        /// <summary>
        /// The object is in the wrong document.
        /// </summary>
        [DomDescription("The object is in the wrong document.")]
        [DomName("WRONG_DOCUMENT_ERR")]
        WrongDocument = 0x4,
        /// <summary>
        /// Invalid character detected.
        /// </summary>
        [DomDescription("Invalid character detected.")]
        [DomName("INVALID_CHARACTER_ERR")]
        InvalidCharacter = 0x5,
        /// <summary>
        /// The data is allowed for this object.
        /// </summary>
        [DomDescription("The data is allowed for this object.")]
        [DomName("NO_DATA_ALLOWED_ERR")]
        [DomHistorical]
        NoDataAllowed = 0x6,
        /// <summary>
        /// The object can not be modified.
        /// </summary>
        [DomDescription("The object can not be modified.")]
        [DomName("NO_MODIFICATION_ALLOWED_ERR")]
        NoModificationAllowed = 0x7,
        /// <summary>
        /// The object can not be found here.
        /// </summary>
        [DomDescription("The object can not be found here.")]
        [DomName("NOT_FOUND_ERR")]
        NotFound = 0x8,
        /// <summary>
        /// The operation is not supported.
        /// </summary>
        [DomDescription("The operation is not supported.")]
        [DomName("NOT_SUPPORTED_ERR")]
        NotSupported = 0x9,
        /// <summary>
        /// The element is already in-use.
        /// </summary>
        [DomDescription("The element is already in-use.")]
        [DomName("INUSE_ATTRIBUTE_ERR")]
        [DomHistorical]
        InUse = 0xA,
        /// <summary>
        /// The object is in an invalid state.
        /// </summary>
        [DomDescription("The object is in an invalid state.")]
        [DomName("INVALID_STATE_ERR")]
        InvalidState = 0xB,
        /// <summary>
        /// The string did not match the expected pattern.
        /// </summary>
        [DomDescription("The string did not match the expected pattern.")]
        [DomName("SYNTAX_ERR")]
        Syntax = 0xC,
        /// <summary>
        /// The object can not be modified in this way.
        /// </summary>
        [DomDescription("The object can not be modified in this way.")]
        [DomName("INVALID_MODIFICATION_ERR")]
        InvalidModification = 0xD,
        /// <summary>
        /// The operation is not allowed by namespaces in XML.
        /// </summary>
        [DomDescription("The operation is not allowed by namespaces in XML.")]
        [DomName("NAMESPACE_ERR")]
        Namespace = 0xE,
        /// <summary>
        /// The object does not support the operation or argument.
        /// </summary>
        [DomDescription("The object does not support the operation or argument.")]
        [DomName("INVALID_ACCESS_ERR")]
        InvalidAccess = 0xF,
        /// <summary>
        /// The validation failed.
        /// </summary>
        [DomDescription("The validation failed.")]
        [DomName("VALIDATION_ERR")]
        Validation = 0xF,
        /// <summary>
        /// The provided argument type is invalid.
        /// </summary>
        [DomDescription("The provided argument type is invalid.")]
        [DomName("TYPE_MISMATCH_ERR")]
        [DomHistorical]
        TypeMismatch = 0x11,
        /// <summary>
        /// The operation is insecure.
        /// </summary>
        [DomDescription("The operation is insecure.")]
        [DomName("SECURITY_ERR")]
        Security = 0x12,
        /// <summary>
        /// A network error occurred.
        /// </summary>
        [DomDescription("A network error occurred.")]
        [DomName("NETWORK_ERR")]
        Network = 0x13,
        /// <summary>
        /// The operation was aborted.
        /// </summary>
        [DomDescription("The operation was aborted.")]
        [DomName("ABORT_ERR")]
        Abort = 0x14,
        /// <summary>
        /// The given URL does not match another URL.
        /// </summary>
        [DomDescription("The given URL does not match another URL.")]
        [DomName("URL_MISMATCH_ERR")]
        UrlMismatch = 0x15,
        /// <summary>
        /// The quota has been exceeded.
        /// </summary>
        [DomDescription("The quota has been exceeded.")]
        [DomName("QUOTA_EXCEEDED_ERR")]
        QuotaExceeded = 0x16,
        /// <summary>
        /// The operation timed out.
        /// </summary>
        [DomDescription("The operation timed out.")]
        [DomName("TIMEOUT_ERR")]
        Timeout = 0x17,
        /// <summary>
        /// The supplied node is incorrect or has an incorrect ancestor for this operation.
        /// </summary>
        [DomDescription("The supplied node is incorrect or has an incorrect ancestor for this operation.")]
        [DomName("INVALID_NODE_TYPE_ERR")]
        InvalidNodeType = 0x18,
        /// <summary>
        /// The object can not be cloned.
        /// </summary>
        [DomDescription("The object can not be cloned.")]
        [DomName("DATA_CLONE_ERR")]
        DataClone = 0x19,
    }
}
