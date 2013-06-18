using System;
using System.Diagnostics;

namespace AngleSharp
{
    /// <summary>
    /// A set of useful helpers concerning errors.
    /// </summary>
    static class Errors
    {
        /// <summary>
        /// Retrieves a string describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The description of the error.</returns>
        [DebuggerStepThrough]
        public static string GetError(ErrorCode code)
        {
            switch (code)
            {
                case ErrorCode.EOF:
                    return "Unexpected end of the given file.";

                case ErrorCode.IndexSizeError:
                    return "The index is not in the allowed range.";

                case ErrorCode.WrongDocumentError:
                    return "The object is in the wrong document.";

                case ErrorCode.NotFoundError:
                    return "The object can not be found here.";

                case ErrorCode.NotSupportedError:
                    return "The operation is not supported.";

                case ErrorCode.InvalidStateError:
                    return "The object is in an invalid state.";

                case ErrorCode.InvalidModificationError:
                    return "The object can not be modified in this way.";

                case ErrorCode.NamespaceError:
                    return "The operation is not allowed by Namespaces in XML.";

                case ErrorCode.InvalidAccessError:
                    return "The object does not support the operation or argument.";

                case ErrorCode.SecurityError:
                    return "The operation is insecure.";

                case ErrorCode.NetworkError:
                    return "A network error occurred.";

                case ErrorCode.AbortError:
                    return "The operation was aborted.";

                case ErrorCode.URLMismatchError:
                    return "The given URL does not match another URL.";

                case ErrorCode.QuotaExceededError:
                    return "The quota has been exceeded.";

                case ErrorCode.TimeoutError:
                    return "The operation timed out.";

                case ErrorCode.InvalidNodeTypeError:
                    return "The supplied node is incorrect or has an incorrect ancestor for this operation.";

                case ErrorCode.DataCloneError:
                    return "The object can not be cloned.";

                case ErrorCode.EncodingError:
                    return "The encoding operation (either encoded or decoding) failed.";

                case ErrorCode.ItemNotFound:
                    return "The specified item could not be found.";

                case ErrorCode.SyntaxError:
                    return "The given string has a syntax error and is unparsable";

                case ErrorCode.InUse:
                    return "The element is already in use.";

                case ErrorCode.HierarchyRequestError:
                    return "The requested hierarchy is not possible.";

                case ErrorCode.InvalidCharacter:
                    return "Invalid character detected.";

                case ErrorCode.NoModificationAllowed:
                    return "No modification allowed.";

                case ErrorCode.BogusComment:
                    return "Bogus comment detected.";

                case ErrorCode.AmbiguousOpenTag:
                    return "Ambiguous open tag symbol found.";

                case ErrorCode.TagClosedWrong:
                    return "The tag has been closed inappropriately.";

                case ErrorCode.ClosingSlashMisplaced:
                    return "The closing slash symbol is misplaced and has been ignored.";

                case ErrorCode.UndefinedMarkupDeclaration:
                    return "Undefined markup declaration ignored.";

                case ErrorCode.LineBreakUnexpected:
                    return "This position does not support a linebreak (LF, FF).";

                case ErrorCode.CommentEndedWithEM:
                    return "Comment ended unexpectedly with an exclamation mark.";

                case ErrorCode.CommentEndedWithDash:
                    return "Comment ended unexpectedly with a dash.";

                case ErrorCode.CommentEndedUnexpected:
                    return "Unexpected character detected at the end of the comment.";

                case ErrorCode.DoctypeUnexpected:
                    return "The doctype found an unexpected character.";

                case ErrorCode.TagCannotBeSelfClosed:
                    return "The given tag cannot be self-closed.";

                case ErrorCode.EndTagCannotBeSelfClosed:
                    return "End tags can never be self-closed.";

                case ErrorCode.EndTagCannotHaveAttributes:
                    return "End tags cannot carry attributes.";

                case ErrorCode.NULL:
                    return "No character has been found using replacement character instead.";

                case ErrorCode.CharacterReferenceInvalidCode:
                    return "The entered character code is invalid. A proper replacement character has been returned.";

                case ErrorCode.CharacterReferenceInvalidNumber:
                    return "The given character code is invalid. A replacement character has been returned.";

                case ErrorCode.CharacterReferenceInvalidRange:
                    return "The given character code is within an invalid range.";

                case ErrorCode.CharacterReferenceSemicolonMissing:
                    return "The given character code has not been closed properly.";

                case ErrorCode.CharacterReferenceWrongNumber:
                    return "The given character code must be a number, but no number has been detected.";

                case ErrorCode.CharacterReferenceNotTerminated:
                    return "The character reference has not been terminated by semi-colon.";

                case ErrorCode.CharacterReferenceAttributeEqualsFound:
                    return "The character reference in an attribute contains an invalid character.";

                case ErrorCode.AttributeNameExpected:
                    return "The provided character is not valid for the beginning of another attribute.";

                case ErrorCode.AttributeNameInvalid:
                    return "The scanned character is not allowed in attribute names.";

                case ErrorCode.AttributeValueInvalid:
                    return "The character cannot be used in attribute values.";

                case ErrorCode.DoubleQuotationMarkUnexpected:
                    return "The double quotation mark is illegal.";

                case ErrorCode.SingleQuotationMarkUnexpected:
                    return "The single quotation mark is misplaced.";

                case ErrorCode.DoctypeInvalidCharacter:
                    return "The scanned character is either not allowed in doctypes or misplaced.";

                case ErrorCode.DoctypePublicInvalid:
                    return "The doctype's public identifier contains an illegal character.";

                case ErrorCode.DoctypeSystemInvalid:
                    return "The doctype's system identifier contains an illegal character.";

                case ErrorCode.DoctypeUnexpectedAfterName:
                    return "The character is not allowed after the doctype's name.";

                case ErrorCode.AttributeDuplicateOmitted:
                    return "The specified attribute has already been added and has been omitted.";

                case ErrorCode.TokenNotPossible:
                    return "The given token is not allowed in the current state.";

                case ErrorCode.DoctypeTagInappropriate:
                    return "The doctype tag can only be placed on top of the document.";

                case ErrorCode.TagMustBeInHead:
                    return "This tag must be included in the head element.";

                case ErrorCode.HeadTagMisplaced:
                    return "The head tag can only be placed once inside the html element.";

                case ErrorCode.HtmlTagMisplaced:
                    return "The html tag can only be placed once as the root element.";

                case ErrorCode.BodyTagMisplaced:
                    return "The body tag can only be placed once inside the html element.";

                case ErrorCode.FramesetMisplaced:
                    return "The frameset element has been misplaced.";

                case ErrorCode.IllegalElementInSelectDetected:
                    return "The given tag cannot be a child element of a select node.";

                case ErrorCode.IllegalElementInTableDetected:
                    return "The given tag cannot be a child element of a table node.";

                case ErrorCode.ImageTagNamedWrong:
                    return "The tag name of the image tag is actually img and not image.";

                case ErrorCode.TagInappropriate:
                    return "The given tag cannot be applied at the current position.";

                case ErrorCode.InputUnexpected:
                    return "The input element is unexpected and has been ignored.";

                case ErrorCode.FormInappropriate:
                    return "The given form tag is inappropriate and has been ignored.";
                    
                case ErrorCode.TagCannotEndHere:
                    return "The ending of the given tag has been misplaced.";

                case ErrorCode.TagCannotStartHere:
                    return "The given tag cannot start here.";

                case ErrorCode.SelectNesting:
                    return "It is not possible to nest select tags.";

                case ErrorCode.TableNesting:
                    return "It is not possible to nest table tags.";

                case ErrorCode.DoctypeInvalid:
                    return "The given doctype tag is invalid.";

                case ErrorCode.DoctypeMissing:
                    return "The expected doctype tag is missing. Quirks mode has been activated.";

                case ErrorCode.TagClosingMismatch:
                    return "The given closing tag and the currently open tag do not match.";

                case ErrorCode.CaptionNotInScope:
                    return "No caption tag has been found within the local scope.";

                case ErrorCode.SelectNotInScope:
                    return "No select tag has been found within the local scope.";

                case ErrorCode.TableRowNotInScope:
                    return "No tr tag has been found within the local scope.";

                case ErrorCode.TableNotInScope:
                    return "No table tag has been found within the local scope.";

                case ErrorCode.ParagraphNotInScope:
                    return "No p tag has been found within the local scope.";

                case ErrorCode.BodyNotInScope:
                    return "No body tag has been found within the local scope.";

                case ErrorCode.BlockNotInScope:
                    return "No block element has been found within the local scope.";

                case ErrorCode.TableCellNotInScope:
                    return "No td or th tag has been found within the local scope.";

                case ErrorCode.TableSectionNotInScope:
                    return "No thead, tbody or tfoot tag has been found within the local scope.";

                case ErrorCode.ObjectNotInScope:
                    return "No object element has been found within the local scope.";

                case ErrorCode.HeadingNotInScope:
                    return "No h1, h2, h3, h4, h5 or h6 tag has been found within the local scope.";

                case ErrorCode.ListItemNotInScope:
                    return "No li, dt, or dd tag has been found within the local scope.";

                case ErrorCode.FormNotInScope:
                    return "No form tag has been found within the local scope.";

                case ErrorCode.ButtonInScope:
                    return "No button tag has been found within the local scope.";

                case ErrorCode.NobrInScope:
                    return "No nobr has been found within the local scope.";

                case ErrorCode.ElementNotInScope:
                    return "No element has been found within the local scope.";

                case ErrorCode.TagDoesNotMatchCurrentNode: 
                    return "The given end tag does not match the current node.";

                case ErrorCode.HeadingNested:
                    return "The previous heading has not been closed properly.";

                case ErrorCode.AnchorNested:
                    return "The previous anchor element has not been closed properly.";

                case ErrorCode.CurrentNodeIsNotRoot: 
                    return "The current node is not the root of the document.";

                case ErrorCode.CurrentNodeIsRoot: 
                    return "The current node is the root of the document.";

                case ErrorCode.TagInvalidInFragmentMode: 
                    return "This tag is invalid in fragment mode.";

                case ErrorCode.FormAlreadyOpen: 
                    return "Another form is already on the stack of open elements.";

                case ErrorCode.FormClosedWrong: 
                    return "The form's ending tag is misplaced.";

                case ErrorCode.BodyClosedWrong: 
                    return "The body has been closed wrong.";

                case ErrorCode.FormattingElementNotFound: 
                    return "An expected formatting element has not been found.";

                case ErrorCode.NotSupported:
                    return "The action is not supported in the current context.";

                default:
                    return "An unexpected error occurred.";
            }
        }
    }
}
