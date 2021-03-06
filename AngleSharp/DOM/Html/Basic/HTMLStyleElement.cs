﻿using AngleSharp.DOM.Css;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML style element.
    /// </summary>
    [DOM("HTMLStyleElement")]
    public sealed class HTMLStyleElement : HTMLElement, IStyleSheet
    {
        #region Members

        CSSStyleSheet _sheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates an HTML style element.
        /// </summary>
        internal HTMLStyleElement()
        {
            _name = Tags.STYLE;
            _sheet = new CSSStyleSheet();
            _sheet.OwnerNode = this;
            _children.ElementsChanged += OnChildrenChanged;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated style sheet.
        /// </summary>
        [DOM("sheet")]
        public StyleSheet Sheet
        {
            get { return _sheet; }
        }

        /// <summary>
        /// Gets or sets if the style is enabled or disabled.
        /// </summary>
        [DOM("disabled")]
        public Boolean Disabled
        {
            get { return Sheet.Disabled; }
            set { Sheet.Disabled = value; }
        }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        [DOM("media")]
        public String Media
        {
            get { return GetAttribute(AttributeNames.MEDIA); }
            set { SetAttribute(AttributeNames.MEDIA, value); }
        }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get { return GetAttribute(AttributeNames.TYPE); }
            set { SetAttribute(AttributeNames.TYPE, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a special textual representation of the node.
        /// </summary>
        /// <returns>A string containing only (rendered) text.</returns>
        public override String ToText()
        {
            return String.Empty;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Internal methods

        void OnChildrenChanged(Object sender, EventArgs e)
        {
            _sheet.ReevaluateFromSource(TextContent);
        }

        /// <summary>
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.MEDIA, StringComparison.Ordinal))
                _sheet.Media.MediaText = Media;
            else
                base.OnAttributeChanged(name);
        }

        #endregion
    }
}
