using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class DtdEntityToken : DtdToken
    {
        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public DtdEntityToken()
        {
            _type = DtdTokenType.Entity;
        }

        #endregion

        #region Properties

        public Boolean IsParameter 
        { 
            get; 
            set;
        }

        public Boolean IsExtern
        {
            get;
            set;
        }

        public String Value
        {
            get;
            set;
        }

        public String ExternNotation
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public Entity ToElement()
        {
            var entity = new Entity();
            //TODO
            entity.NotationName = Name;
            entity.NodeValue = Value;
            return entity;
        }

        #endregion
    }
}
