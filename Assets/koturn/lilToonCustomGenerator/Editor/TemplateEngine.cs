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
            var fs = reader.BaseStream as FileStream;
            var msgPrefix = fs == null ? "" : fs.Name + ":";

            // Whether the parent block itself is to be output
            var emitStack = new Stack<bool>();
            // Has the parent block already printed the output of any branch within that `if` block.
            var branchEmittedStack = new Stack<bool>();
            // Whether the current block is to be output.
            var shouldEmitCurrent = true;
            // Has any branch already been executed within the current `if` block.
            var currentBranchEmitted = false;

            int lineCount = 0;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lineCount++;
                if (line.StartsWith("!!"))
                {
                    //
                    // ---- endif ----
                    //
                    if (RegexProvider.TagEndIfRegex.IsMatch(line))
                    {
                        if (emitStack.Count == 0)
                        {
                            throw new InvalidOperationException(msgPrefix + lineCount + ":\"endif\" is detected out of if context");
                        }

                        // Exit the current if block and return to the parent block.
                        shouldEmitCurrent = emitStack.Pop();
                        currentBranchEmitted = branchEmittedStack.Pop();

                        // Skip !!endif!! line.
                        continue;
                    }
                    //
                    // ---- else ----
                    //
                    if (RegexProvider.TagElseRegex.IsMatch(line))
                    {
                        if (emitStack.Count == 0)
                        {
                            throw new InvalidOperationException(msgPrefix + lineCount + "\"else\" is detected out of if context");
                        }

                        // If the parent block is not to be emitted, this else block will not be emitted either.
                        if (!emitStack.Peek())
                        {
                            shouldEmitCurrent = false;
                        }
                        else
                        {
                            // If any of the branches within this if block have already been emitted, this else statement will not be emitted.
                            if (currentBranchEmitted)
                            {
                                shouldEmitCurrent = false;
                            }
                            else
                            {
                                shouldEmitCurrent = true;
                                currentBranchEmitted = true;
                            }
                        }
                        // Skip !!else!! line.
                        continue;
                    }
                    //
                    // ---- if / elif ----
                    //
                    var m = RegexProvider.TagIfemptyRegex.Match(line);
                    if (m.Success)
                    {
                        var g = m.Groups;
                        var isElif = !string.IsNullOrEmpty(g[1].Value);
                        var isNot = !string.IsNullOrEmpty(g[2].Value);
                        var tag = g[3].Value;

                        if (!isElif)
                        {
                            //
                            // ---- ifempty / ifnotempty ----
                            //

                            // Since we're starting a new `if` block, we push the parent block's state onto the stack.
                            emitStack.Push(shouldEmitCurrent);
                            branchEmittedStack.Push(currentBranchEmitted);

                            // Reset the "branch output flag" within this `if` block.
                            currentBranchEmitted = false;

                            if (!emitStack.Peek())
                            {
                                // If the parent is not to be emitted, this `if` block will not be emitted either.
                                shouldEmitCurrent = false;
                            }
                            else
                            {
                                // Since parent is the target for output, evaluate the condition of this `if` block.
                                var hasTagValue = replaceDef.ContainsKey(tag) && !string.IsNullOrEmpty(replaceDef[tag]);
                                //
                                // ifnotempty / ifempty
                                //
                                if (isNot ? hasTagValue : !hasTagValue)
                                {
                                    shouldEmitCurrent = true;
                                    currentBranchEmitted = true;
                                }
                                else
                                {
                                    shouldEmitCurrent = false;
                                }
                            }
                        }
                        else
                        {
                            //
                            // ---- elifempty / elifnotempty ----
                            //
                            if (emitStack.Count == 0)
                            {
                                throw new InvalidOperationException(msgPrefix + lineCount + ":\"elif\" is detected out of if context.");
                            }

                            if (!emitStack.Peek())
                            {
                                // If the parent is not to be emitted, this `elif` block will not be emitted either.
                                shouldEmitCurrent = false;
                            }
                            else
                            {
                                if (currentBranchEmitted)
                                {
                                    // If a branch within this `if` block has already been executed, this `elif` will not be emitted.
                                    shouldEmitCurrent = false;
                                }
                                else
                                {
                                    // Since nothing has been emitted yet, evaluate the condition in this `elif` statement.
                                    var hasTagValue = replaceDef.ContainsKey(tag) && !string.IsNullOrEmpty(replaceDef[tag]);

                                    // elifnotempty / elifempty
                                    if (isNot ? hasTagValue : !hasTagValue)
                                    {
                                        shouldEmitCurrent = true;
                                        currentBranchEmitted = true;
                                    }
                                    else
                                    {
                                        shouldEmitCurrent = false;
                                    }
                                }
                            }
                        }

                        // Skip lines containing !!ifxxx!! / !!elifxxx!!.
                        continue;
                    }
                }

                // If the current block is not to be output, the line is skipped as usual.
                if (!shouldEmitCurrent)
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

            if (emitStack.Count > 0)
            {
                throw new InvalidOperationException(fs.Name + lineCount + ": Non closed if detected");
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
    }
}
