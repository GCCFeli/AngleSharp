﻿using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the base class for frame elements.
    /// </summary>
    public abstract class HTMLFrameElementBase : HTMLFrameOwnerElement
    {
        #region ctor

        internal HTMLFrameElementBase()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the frame.
        /// </summary>
        [DOM("name")]
        public String Name
        {
            get { return GetAttribute(AttributeNames.NAME); }
            set { SetAttribute(AttributeNames.NAME, value); }
        }

        /// <summary>
        /// Gets or sets the frame source.
        /// </summary>
        [DOM("src")]
        public String Src
        {
            get { return GetAttribute(AttributeNames.SRC); }
            set { SetAttribute(AttributeNames.SRC, value); }
        }

        /// <summary>
        /// Gets or sets whether or not the frame should have scrollbars.
        /// </summary>
        [DOM("scrolling")]
        public String Scrolling
        {
            get { return GetAttribute(AttributeNames.SCROLLING); }
            set { SetAttribute(AttributeNames.SCROLLING, value); }
        }

        /// <summary>
        /// Gets the document this frame contains, if there is any and it is available, or null otherwise.
        /// </summary>
        [DOM("contentDocument")]
        public Document ContentDocument
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the URL designating a long description of this image or frame.
        /// </summary>
        [DOM("longDesc")]
        public String LongDesc
        {
            get { return GetAttribute(AttributeNames.LONGDESC); }
            set { SetAttribute(AttributeNames.LONGDESC, value); }
        }

        /// <summary>
        /// Gets or sets the request frame borders.
        /// </summary>
        [DOM("frameBorder")]
        public String FrameBorder
        {
            get { return GetAttribute(AttributeNames.FRAMEBORDER); }
            set { SetAttribute(AttributeNames.FRAMEBORDER, value); }
        }

        #endregion
    }
}
