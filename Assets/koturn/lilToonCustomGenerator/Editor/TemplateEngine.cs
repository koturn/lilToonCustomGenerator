using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Koturn.LilToonCustomGenerator.Editor.Internals;


namespace Koturn.LilToonCustomGenerator.Editor
{
    /// <summary>
    /// Entry point class
    /// </summary>
    [System.Runtime.InteropServices.Guid("09b3ed90-e783-6a54-e828-052feebaf672")]
    public sealed class TemplateEngine
    {
        /// <summary>
        /// New line code.
        /// </summary>
        public string NewLine { get; set; }
        /// <summary>
        /// Replace definition.
        /// </summary>
        public Dictionary<string, string> TagDictionary { get; } = new Dictionary<string, string>();


        /// <summary>
        /// Create instance with empty tag dictionary and environment new line string.
        /// </summary>
        public TemplateEngine()
            : this(null, Environment.NewLine)
        {
        }

        /// <summary>
        /// Create instance with empty tag dictionary and specified new line string.
        /// </summary>
        /// <param name="newLine">New line string.</param>
        public TemplateEngine(string newLine)
            : this(null, newLine)
        {
        }

        /// <summary>
        /// Create instance with specified tag dictionary and environment new line string.
        /// </summary>
        /// <param name="tagDictionary">Tag dictionary.</param>
        public TemplateEngine(Dictionary<string, string> tagDictionary)
            : this(tagDictionary, Environment.NewLine)
        {
        }

        /// <summary>
        /// Create instance with specified tag dictionary and new line string.
        /// </summary>
        /// <param name="tagDictionary"></param>
        /// <param name="newLine">New line string.</param>
        public TemplateEngine(Dictionary<string, string> tagDictionary, string newLine)
        {
            TagDictionary = tagDictionary ?? new Dictionary<string, string>();
            NewLine = newLine;
        }


        /// <summary>
        /// Expand template file.
        /// </summary>
        /// <param name="templatePath">File path of template file.</param>
        /// <param name="targetPath">File path of destination file.</param>
        /// <exception cref="InvalidOperationException">Thrown when invalid template syntax detected.</exception>
        public void ExpandTemplate(string templatePath, string targetPath)
        {
            using (var templateStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, FileOptions.SequentialScan))
            using (var targetStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.Read, 8192, FileOptions.SequentialScan))
            {
                ExpandTemplate(templateStream, targetStream);
            }
        }

        /// <summary>
        /// Expand template file.
        /// </summary>
        /// <param name="templateStream"><see cref="FileStream"/> of template file.</param>
        /// <param name="targetStream"><see cref="FileStream"/> of destination file.</param>
        /// <exception cref="InvalidOperationException">Thrown when invalid template syntax detected.</exception>
        public void ExpandTemplate(FileStream templateStream, FileStream targetStream)
        {
            using (var reader = new StreamReader(templateStream))
            using (var writer = new StreamWriter(targetStream)
            {
                NewLine = NewLine
            })
            {
                ExpandTemplate(reader, writer);
            }
        }

        /// <summary>
        /// Expand template file.
        /// </summary>
        /// <param name="reader"><see cref="StreamReader"/> of template file.</param>
        /// <param name="writer"><see cref="StreamWriter"/> of destination file.</param>
        /// <exception cref="InvalidOperationException">Thrown when invalid template syntax detected.</exception>
        public void ExpandTemplate(StreamReader reader, StreamWriter writer)
        {
            var replaceDef = TagDictionary;
            var state = IfState.ShouldEmit;
            var stateStack = new Stack<IfState>();
            var skipLevel = int.MaxValue;
            var lineCount = 0;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lineCount++;

                if (line.StartsWith("!!"))
                {
                    if (RegexProvider.TagEndIfRegex.IsMatch(line))
                    {
                        // endif-tag
                        if (stateStack.Count <= skipLevel)
                        {
                            state = stateStack.Pop();
                        }
                        else
                        {
                            stateStack.Pop();
                        }
                        continue;  // Not emit !!endif!! line
                    }
                    else if (RegexProvider.TagElseRegex.IsMatch(line))
                    {
                        // else-tag
                        if (stateStack.Count == 0)
                        {
                            throw new InvalidOperationException("\"else\" is detected out of if context at line " + lineCount + ".");
                        }
                        if (stateStack.Count <= skipLevel)
                        {
                            if (state == IfState.ShouldNotEmit)
                            {
                                state = IfState.ShouldEmit;
                                skipLevel = int.MaxValue;
                            }
                            else if (state == IfState.ShouldEmit)
                            {
                                state = IfState.AlreadyEmit;
                            }
                        }
                        continue;  // Not emit !!else!! line
                    }
                    else
                    {
                        var m = RegexProvider.TagIfemptyRegex.Match(line);
                        if (m.Success)
                        {
                            var g = m.Groups;
                            if (string.IsNullOrEmpty(g[1].Value))
                            {
                                // if
                                stateStack.Push(state);
                            }
                            else if (state == IfState.ShouldEmit || state == IfState.AlreadyEmit)
                            {
                                // elif
                                if (stateStack.Count <= skipLevel)
                                {
                                    state = IfState.AlreadyEmit;
                                }
                                continue;
                            }

                            if (stateStack.Count > skipLevel)
                            {
                                continue;
                            }

                            // if
                            // var hasTagValue = string.IsNullOrEmpty(replaceDef.GetValueOrDefault(g[3].Value));
                            var tag = g[3].Value;
                            var hasTagValue = replaceDef.ContainsKey(tag) && !string.IsNullOrEmpty(replaceDef[tag]);

                            if (string.IsNullOrEmpty(g[2].Value))
                            {
                                // ifempty
                                state = hasTagValue ? IfState.ShouldNotEmit : IfState.ShouldEmit;
                            }
                            else
                            {
                                // ifnotempty
                                state = hasTagValue ? IfState.ShouldEmit : IfState.ShouldNotEmit;
                            }
                            if (state == IfState.ShouldEmit)
                            {
                                skipLevel = int.MaxValue;
                            }
                            else
                            {
                                skipLevel = stateStack.Count;
                            }

                            continue;  // Not emit !!ifempty!!, !!ifnotempty!!, !!elifempty!!, !!elifnotempty!! line
                        }
                    }
                }
                if (state != IfState.ShouldEmit)
                {
                    continue;
                }

                var replacedLine = Replace(line);
                if (replacedLine != null)
                {
                    writer.Write(replacedLine);
                    writer.Write(NewLine);
                }
            }

            if (stateStack.Count > 0)
            {
                throw new InvalidOperationException("Non closed if");
            }
        }

        /// <summary>
        /// Replace tags in the specified text.
        /// </summary>
        /// <param name="text">Target text.</param>
        /// <returns>Replaced text.</returns>
        public string Replace(string text)
        {
            var startIndex = text.IndexOf("%%");
            if (startIndex == -1)
            {
                return text;
            }

            var replaceDef = TagDictionary;
            var sb = new StringBuilder();
            var m = RegexProvider.TagRegex.Match(text);
            var parsedIndex = 0;
            while (m.Success)
            {
                var g = m.Groups;

                if (g[0].Index > parsedIndex)
                {
                    sb.Append(text, parsedIndex, g[0].Index - parsedIndex);
                    parsedIndex = g[0].Index;
                }

                var tag = g[1].Value;
                if (replaceDef.ContainsKey(tag))
                {
                    var content = replaceDef[tag];
                    var indentString = string.Empty;
                    var isKeepIndent = false;
                    var optionPart = m.Groups[2].Value;
                    foreach (var option in optionPart.Split(':'))
                    {
                        if (option.StartsWith("spaceindent="))
                        {
                            indentString = new string(' ', int.Parse(option.Substring(12)));
                            isKeepIndent = false;
                        }
                        else if (option.StartsWith("tabindent="))
                        {
                            indentString = new string('\t', int.Parse(option.Substring(10)));
                            isKeepIndent = false;
                        }
                        else if (option == "keepindent")
                        {
                            var m2 = Regex.Match(text, "^\\s+");
                            if (m2.Success)
                            {
                                indentString = m2.Groups[0].Value;
                            }
                            else
                            {
                                indentString = string.Empty;
                            }
                            isKeepIndent = true;
                        }
                        else if (option == "skipempty")
                        {
                            if (content.Length == 0)
                            {
                                return null;
                            }
                        }
                    }

                    using (var ssr = new StringReader(content))
                    {
                        int writeLineCount = 0;
                        string contentLine;
                        while ((contentLine = ssr.ReadLine()) != null)
                        {
                            if (writeLineCount > 0)
                            {
                                sb.Append(NewLine);
                                sb.Append(indentString);
                            }
                            else if (!isKeepIndent)
                            {
                                sb.Append(indentString);
                            }
                            sb.Append(contentLine);
                            writeLineCount++;
                        }
                    }
                }
                else
                {
                    Debug.LogWarningFormat("tag \"{0}\" is not defined", tag);
                }

                parsedIndex += g[0].Length;
                m = RegexProvider.TagRegex.Match(text, parsedIndex);
            }

            sb.Append(text.Substring(parsedIndex));

            return sb.ToString();
        }

        /// <summary>
        /// If condition state.
        /// </summary>
        private enum IfState
        {
            /// <summary>
            /// Indicates that should not be emitted.
            /// </summary>
            ShouldNotEmit,
            /// <summary>
            /// Indicates that should be emitted.
            /// </summary>
            ShouldEmit,
            /// <summary>
            /// Indicates that it has already been emitted.
            /// </summary>
            AlreadyEmit,
        }
    }
}
