using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Fore more information about CSS properties
    /// see http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    public class CSSProperty
    {
        #region Members

        string _name;
        CSSValue _value;
        bool _important;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS property.
        /// </summary>
        /// <param name="name"></param>
        public CSSProperty(string name)
        {
            _name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        public CSSValue Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Gets or sets if the !important flag has been set.
        /// </summary>
        public bool Important
        {
            get { return _important; }
            set { _important = value; }
        }

        #endregion

        #region Factory

        /// <summary>
        /// Creates a new CSSProperty from the given property name.
        /// </summary>
        /// <param name="propertyName">The name of the new CSS property.</param>
        /// <returns>The new CSSProperty.</returns>
        public static CSSProperty Factory(string propertyName)
        {
            //azimuth
            //animation
            //animation-delay
            //animation-direction
            //animation-duration
            //animation-fill-mode
            //animation-iteration-count
            //animation-name
            //animation-play-state
            //animation-timing-function
            //background-attachment
            //background-color
            //background-clip
            //background-origin
            //background-size
            //background-image
            //background-position
            //background-repeat
            //background
            //border-color
            //border-spacing
            //border-collapse
            //border-style
            //border-radius
            //box-shadow
            //box-decoration-break
            //break-after
            //break-before
            //break-inside
            //backface-visibility
            //border-top-left-radius
            //border-top-right-radius
            //border-bottom-left-radius
            //border-bottom-right-radius
            //border-image
            //border-image-outset
            //border-image-repeat
            //border-image-source
            //border-image-slice
            //border-image-width
            //border-top
	        //border-right
            //border-bottom
            //border-left
            //border-top-color
            //border-left-color
            //border-right-color
            //border-bottom-color
            //border-top-style
            //border-left-style
            //border-right-style
            //border-bottom-style
            //border-top-width
            //border-left-width
            //border-right-width
            //border-bottom-width
            //border-width
            //border
            //bottom
            //columns
            //column-count
            //column-fill
            //column-gap
            //column-rule-color
            //column-rule-style
            //column-rule-width
            //column-span
            //column-width		
            //caption-side		
            //clear				
            //clip				
            //color				
            //content				
            //counter-increment	
            //counter-reset		
            //cue-after			
            //cue-before			
            //cue					
            //cursor				
            //direction			
            //display				
            //elevation			
            //empty-cells			
            //float				
            //font-family			
            //font-size			
            //font-style			
            //font-variant		
            //font-weight			
            //font				
            //height				
            //left				
            //letter-spacing		
            //line-height			
            //list-style-image	
            //list-style-position	
            //list-style-type		
            //list-style
            //marquee-direction
            //marquee-play-count
            //marquee-speed
            //marquee-style
            //margin-right
            //margin-left
            //margin-top
		    //margin-bottom
            //margin
            //max-height
            //max-width
            //min-height
            //min-width
            //opacity
            //orphans
            //outline-color
            //outline-style
            //outline-width
            //outline
            //overflow
            //padding-top
            //padding-right
            //padding-left
            //padding-bottom
            //padding
            //page-break-after
            //page-break-before
            //page-break-inside
            //pause-after
            //pause-before
            //pause
            //perspective
            //perspective-origin
            //pitch-range
            //pitch
            //play-during
            //position
            //quotes
            //richness
            //right
            //speak-header
            //speak-numeral
            //speak-punctuation
            //speak
            //speech-rate
            //stress
            //table-layout
            //text-align
            //text-decoration
            //text-indent
            //text-transform
            //transform
            //transform-origin
            //transform-style
            //transition
            //transition-delay
            //transition-duration
            //transition-timing-function
            //transition-property
            //top
            //unicode-bidi
            //vertical-align
            //visibility
            //voice-family
            //volume
            //white-space
            //widows
            //width
            //word-spacing
            //z-index
            return new CSSProperty(propertyName);
        }

        #endregion
    }
}
