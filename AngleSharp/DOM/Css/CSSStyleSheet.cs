﻿using AngleSharp.Css;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS Stylesheet.
    /// </summary>
    [DOM("CSSStyleSheet")]
    public sealed class CSSStyleSheet : StyleSheet, ICssObject
    {
        #region Members

        CSSRuleList _cssRules;
        CSSRule _ownerRule;
        Task _current;
        CancellationTokenSource _cts;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        internal CSSStyleSheet()
        {
            _cssRules = new CSSRuleList();
            _cts = new CancellationTokenSource();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the style sheet.
        /// </summary>
        [DOM("cssRules")]
        public CSSRuleList CssRules
        {
            get { return _cssRules; }
        }

        /// <summary>
        /// Gets the @import rule if the stylesheet was importated otherwise it returns null.
        /// </summary>
        [DOM("ownerRule")]
        public CSSRule OwnerRule
        {
            get { return _ownerRule; }
            internal set { _ownerRule = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Removes a style rule from the current style sheet object.
        /// </summary>
        /// <param name="index">The index representing the position to be removed.</param>
        /// <returns>The current stylesheet.</returns>
        [DOM("deleteRule")]
        public CSSStyleSheet DeleteRule(Int32 index)
        {
            if (index >= 0 && index < _cssRules.Length)
                _cssRules.RemoveAt(index);

            return this;
        }

        /// <summary>
        /// Inserts a new style rule into the current style sheet.
        /// </summary>
        /// <param name="rule">A string containing the rule to be inserted (selector and declaration).</param>
        /// <param name="index">The index representing the position to be inserted.</param>
        /// <returns>The current stylesheet.</returns>
        [DOM("insertRule")]
        public CSSStyleSheet InsertRule(String rule, Int32 index)
        {
            if (index >= 0 && index <= _cssRules.Length)
            {
                var value = CssParser.ParseRule(rule);
                _cssRules.InsertAt(index, value);
            }

            return this;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public String ToCss()
        {
            var sb = Pool.NewStringBuilder();

            foreach (var rule in _cssRules)
                sb.AppendLine(rule.ToCss());

            return sb.ToPool();
        }

        #endregion

        #region Internal Methods

        internal void ReevaluateFromUrl(String url)
        {
            TryCancelCurrent();
            _cssRules.List.Clear();
            _current = Builder.StyleFromUrl(this, url, _cts.Token);
        }

        internal void ReevaluateFromSource(String source)
        {
            TryCancelCurrent();
            _cssRules.List.Clear();
            Builder.StyleFromSource(this, source);
        }

        void TryCancelCurrent()
        {
            if (_current != null && !_current.IsCompleted)
            {
                _cts.Cancel();
                _cts = new CancellationTokenSource();
            }
        }

        #endregion
    }
}
