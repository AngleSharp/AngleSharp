namespace AngleSharp.DOM
{
    using System;

    [DOM("CharacterData")]
    interface ICharacterData : INode
    {
        [DOM("data")]
        String Value { get; set; }

        [DOM("length")]
        Int32 Length { get; }

        [DOM("substringData")]
        String Substring(Int32 offset, Int32 count);

        [DOM("appendData")]
        void Append(String value);

        [DOM("insertData")]
        void Insert(Int32 offset, String value);

        [DOM("deleteData")]
        void Delete(Int32 offset, Int32 count);

        [DOM("replaceData")]
        void Replace(Int32 offset, Int32 count, String value);
    }
}