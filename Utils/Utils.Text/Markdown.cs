using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Utils.Text
{
	public class Markdown
	{
		private enum TokenType
		{
			Text,
			Tag
		}

		private struct Token
		{
			public Markdown.TokenType Type;

			public string Value;

			public Token(Markdown.TokenType type, string value)
			{
				this.Type = type;
				this.Value = value;
			}
		}

		private const string _version = "1.13";

		private const int _nestDepth = 6;

		private const int _tabWidth = 4;

		private const string _markerUL = "[*+-]";

		private const string _markerOL = "\\d+[.]";

		private string _emptyElementSuffix = " />";

		private bool _linkEmails = true;

		private bool _strictBoldItalic;

		private bool _autoNewlines;

		private bool _autoHyperlink;

		private bool _encodeProblemUrlCharacters;

		private static readonly Dictionary<string, string> _escapeTable;

		private static readonly Dictionary<string, string> _invertedEscapeTable;

		private static readonly Dictionary<string, string> _backslashEscapeTable;

		private readonly Dictionary<string, string> _urls = new Dictionary<string, string>();

		private readonly Dictionary<string, string> _titles = new Dictionary<string, string>();

		private readonly Dictionary<string, string> _htmlBlocks = new Dictionary<string, string>();

		private int _listLevel;

		private static Regex _newlinesLeadingTrailing;

		private static Regex _newlinesMultiple;

		private static Regex _leadingWhitespace;

		private static string _nestedBracketsPattern;

		private static string _nestedParensPattern;

		private static Regex _linkDef;

		private static Regex _blocksHtml;

		private static Regex _htmlTokens;

		private static Regex _anchorRef;

		private static Regex _anchorInline;

		private static Regex _anchorRefShortcut;

		private static Regex _imagesRef;

		private static Regex _imagesInline;

		private static Regex _headerSetext;

		private static Regex _headerAtx;

		private static Regex _horizontalRules;

		private static string _wholeList;

		private static Regex _listNested;

		private static Regex _listTopLevel;

		private static Regex _codeBlock;

		private static Regex _codeSpan;

		private static Regex _bold;

		private static Regex _strictBold;

		private static Regex _italic;

		private static Regex _strictItalic;

		private static Regex _blockquote;

		private static Regex _autolinkBare;

		private static Regex _outDent;

		private static Regex _codeEncoder;

		private static Regex _amps;

		private static Regex _angles;

		private static Regex _backslashEscapes;

		private static Regex _unescapes;

		private static char[] _problemUrlChars;

		public string EmptyElementSuffix
		{
			get
			{
				return this._emptyElementSuffix;
			}
			set
			{
				this._emptyElementSuffix = value;
			}
		}

		public bool LinkEmails
		{
			get
			{
				return this._linkEmails;
			}
			set
			{
				this._linkEmails = value;
			}
		}

		public bool StrictBoldItalic
		{
			get
			{
				return this._strictBoldItalic;
			}
			set
			{
				this._strictBoldItalic = value;
			}
		}

		public bool AutoNewLines
		{
			get
			{
				return this._autoNewlines;
			}
			set
			{
				this._autoNewlines = value;
			}
		}

		public bool AutoHyperlink
		{
			get
			{
				return this._autoHyperlink;
			}
			set
			{
				this._autoHyperlink = value;
			}
		}

		public bool EncodeProblemUrlCharacters
		{
			get
			{
				return this._encodeProblemUrlCharacters;
			}
			set
			{
				this._encodeProblemUrlCharacters = value;
			}
		}

		public string Version
		{
			get
			{
				return "1.13";
			}
		}

		public Markdown()
		{
		}

		public Markdown(MarkdownOptions options)
		{
			this._autoHyperlink = options.AutoHyperlink;
			this._autoNewlines = options.AutoNewlines;
			this._emptyElementSuffix = options.EmptyElementSuffix;
			this._encodeProblemUrlCharacters = options.EncodeProblemUrlCharacters;
			this._linkEmails = options.LinkEmails;
			this._strictBoldItalic = options.StrictBoldItalic;
		}

		static Markdown()
		{
			Markdown._newlinesLeadingTrailing = new Regex("^\\n+|\\n+\\z", RegexOptions.Compiled);
			Markdown._newlinesMultiple = new Regex("\\n{2,}", RegexOptions.Compiled);
			Markdown._leadingWhitespace = new Regex("^[ ]*", RegexOptions.Compiled);
			Markdown._linkDef = new Regex(string.Format("\r\n                        ^[ ]{{0,{0}}}\\[(.+)\\]:  # id = $1\r\n                          [ ]*\r\n                          \\n?                   # maybe *one* newline\r\n                          [ ]*\r\n                        <?(\\S+?)>?              # url = $2\r\n                          [ ]*\r\n                          \\n?                   # maybe one newline\r\n                          [ ]*\r\n                        (?:\r\n                            (?<=\\s)             # lookbehind for whitespace\r\n                            [\"(]\r\n                            (.+?)               # title = $3\r\n                            [\")]\r\n                            [ ]*\r\n                        )?                      # title is optional\r\n                        (?:\\n+|\\Z)", 3), RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._blocksHtml = new Regex(Markdown.GetBlockPattern(), RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
			Markdown._htmlTokens = new Regex("\r\n            (<!(?:--.*?--\\s*)+>)|        # match <!-- foo -->\r\n            (<\\?.*?\\?>)|                 # match <?foo?> " + Markdown.RepeatString(" \r\n            (<[A-Za-z\\/!$](?:[^<>]|", 6) + Markdown.RepeatString(")*>)", 6) + " # match <tag> and </tag>", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._anchorRef = new Regex(string.Format("\r\n            (                               # wrap whole match in $1\r\n                \\[\r\n                    ({0})                   # link text = $2\r\n                \\]\r\n\r\n                [ ]?                        # one optional space\r\n                (?:\\n[ ]*)?                 # one optional newline followed by spaces\r\n\r\n                \\[\r\n                    (.*?)                   # id = $3\r\n                \\]\r\n            )", Markdown.GetNestedBracketsPattern()), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._anchorInline = new Regex(string.Format("\r\n                (                           # wrap whole match in $1\r\n                    \\[\r\n                        ({0})               # link text = $2\r\n                    \\]\r\n                    \\(                      # literal paren\r\n                        [ ]*\r\n                        ({1})               # href = $3\r\n                        [ ]*\r\n                        (                   # $4\r\n                        (['\"])           # quote char = $5\r\n                        (.*?)               # title = $6\r\n                        \\5                  # matching quote\r\n                        [ ]*                # ignore any spaces between closing quote and )\r\n                        )?                  # title is optional\r\n                    \\)\r\n                )", Markdown.GetNestedBracketsPattern(), Markdown.GetNestedParensPattern()), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._anchorRefShortcut = new Regex("\r\n            (                               # wrap whole match in $1\r\n              \\[\r\n                 ([^\\[\\]]+)                 # link text = $2; can't contain [ or ]\r\n              \\]\r\n            )", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._imagesRef = new Regex("\r\n                    (               # wrap whole match in $1\r\n                    !\\[\r\n                        (.*?)       # alt text = $2\r\n                    \\]\r\n\r\n                    [ ]?            # one optional space\r\n                    (?:\\n[ ]*)?     # one optional newline followed by spaces\r\n\r\n                    \\[\r\n                        (.*?)       # id = $3\r\n                    \\]\r\n\r\n                    )", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._imagesInline = new Regex(string.Format("\r\n              (                     # wrap whole match in $1\r\n                !\\[\r\n                    (.*?)           # alt text = $2\r\n                \\]\r\n                \\s?                 # one optional whitespace character\r\n                \\(                  # literal paren\r\n                    [ ]*\r\n                    ({0})           # href = $3\r\n                    [ ]*\r\n                    (               # $4\r\n                    (['\"])       # quote char = $5\r\n                    (.*?)           # title = $6\r\n                    \\5              # matching quote\r\n                    [ ]*\r\n                    )?              # title is optional\r\n                \\)\r\n              )", Markdown.GetNestedParensPattern()), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._headerSetext = new Regex("\r\n                ^(.+?)\r\n                [ ]*\r\n                \\n\r\n                (=+|-+)     # $1 = string of ='s or -'s\r\n                [ ]*\r\n                \\n+", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._headerAtx = new Regex("\r\n                ^(\\#{1,6})  # $1 = string of #'s\r\n                [ ]*\r\n                (.+?)       # $2 = Header text\r\n                [ ]*\r\n                \\#*         # optional closing #'s (not counted)\r\n                \\n+", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._horizontalRules = new Regex("\r\n            ^[ ]{0,3}         # Leading space\r\n                ([-*_])       # $1: First marker\r\n                (?>           # Repeated marker group\r\n                    [ ]{0,2}  # Zero, one, or two spaces.\r\n                    \\1        # Marker character\r\n                ){2,}         # Group repeated at least twice\r\n                [ ]*          # Trailing spaces\r\n                $             # End of line.\r\n            ", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._wholeList = string.Format("\r\n            (                               # $1 = whole list\r\n              (                             # $2\r\n                [ ]{{0,{1}}}\r\n                ({0})                       # $3 = first list item marker\r\n                [ ]+\r\n              )\r\n              (?s:.+?)\r\n              (                             # $4\r\n                  \\z\r\n                |\r\n                  \\n{{2,}}\r\n                  (?=\\S)\r\n                  (?!                       # Negative lookahead for another list item marker\r\n                    [ ]*\r\n                    {0}[ ]+\r\n                  )\r\n              )\r\n            )", string.Format("(?:{0}|{1})", "[*+-]", "\\d+[.]"), 3);
			Markdown._listNested = new Regex("^" + Markdown._wholeList, RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._listTopLevel = new Regex("(?:(?<=\\n\\n)|\\A\\n?)" + Markdown._wholeList, RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._codeBlock = new Regex(string.Format("\r\n                    (?:\\n\\n|\\A\\n?)\r\n                    (                        # $1 = the code block -- one or more lines, starting with a space\r\n                    (?:\r\n                        (?:[ ]{{{0}}})       # Lines must start with a tab-width of spaces\r\n                        .*\\n+\r\n                    )+\r\n                    )\r\n                    ((?=^[ ]{{0,{0}}}\\S)|\\Z) # Lookahead for non-space at line-start, or end of doc", 4), RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._codeSpan = new Regex("\r\n                    (?<!\\\\)   # Character before opening ` can't be a backslash\r\n                    (`+)      # $1 = Opening run of `\r\n                    (.+?)     # $2 = The code block\r\n                    (?<!`)\r\n                    \\1\r\n                    (?!`)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._bold = new Regex("(\\*\\*|__) (?=\\S) (.+?[*_]*) (?<=\\S) \\1", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._strictBold = new Regex("([\\W_]|^) (\\*\\*|__) (?=\\S) ([^\\r]*?\\S[\\*_]*) \\2 ([\\W_]|$)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._italic = new Regex("(\\*|_) (?=\\S) (.+?) (?<=\\S) \\1", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._strictItalic = new Regex("([\\W_]|^) (\\*|_) (?=\\S) ([^\\r\\*_]*?\\S) \\2 ([\\W_]|$)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._blockquote = new Regex("\r\n            (                           # Wrap whole match in $1\r\n                (\r\n                ^[ ]*>[ ]?              # '>' at the start of a line\r\n                    .+\\n                # rest of the first line\r\n                (.+\\n)*                 # subsequent consecutive lines\r\n                \\n*                     # blanks\r\n                )+\r\n            )", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._autolinkBare = new Regex("(^|\\s)(https?|ftp)(://[-A-Z0-9+&@#/%?=~_|\\[\\]\\(\\)!:,\\.;]*[-A-Z0-9+&@#/%=~_|\\[\\]])($|\\W)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			Markdown._outDent = new Regex("^[ ]{1," + 4 + "}", RegexOptions.Multiline | RegexOptions.Compiled);
			Markdown._codeEncoder = new Regex("&|<|>|\\\\|\\*|_|\\{|\\}|\\[|\\]", RegexOptions.Compiled);
			Markdown._amps = new Regex("&(?!(#[0-9]+)|(#[xX][a-fA-F0-9])|([a-zA-Z][a-zA-Z0-9]*);)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
			Markdown._angles = new Regex("<(?![A-Za-z/?\\$!])", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
			Markdown._unescapes = new Regex("\u001a\\d+\u001a", RegexOptions.Compiled);
			Markdown._problemUrlChars = "\"'*()[]$:".ToCharArray();
			Markdown._escapeTable = new Dictionary<string, string>();
			Markdown._invertedEscapeTable = new Dictionary<string, string>();
			Markdown._backslashEscapeTable = new Dictionary<string, string>();
			string text = "";
			string text2 = "\\`*_{}[]()>#+-.!";
			for (int i = 0; i < text2.Length; i++)
			{
				string text3 = text2[i].ToString();
				string hashKey = Markdown.GetHashKey(text3);
				Markdown._escapeTable.Add(text3, hashKey);
				Markdown._invertedEscapeTable.Add(hashKey, text3);
				Markdown._backslashEscapeTable.Add("\\" + text3, hashKey);
				text = text + Regex.Escape("\\" + text3) + "|";
			}
			Markdown._backslashEscapes = new Regex(text.Substring(0, text.Length - 1), RegexOptions.Compiled);
		}

		public string Transform(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "";
			}
			this.Setup();
			text = this.Normalize(text);
			text = this.HashHTMLBlocks(text);
			text = this.StripLinkDefinitions(text);
			text = this.RunBlockGamut(text);
			text = this.Unescape(text);
			this.Cleanup();
			return text + "\n";
		}

		private string RunBlockGamut(string text)
		{
			text = this.DoHeaders(text);
			text = this.DoHorizontalRules(text);
			text = this.DoLists(text);
			text = this.DoCodeBlocks(text);
			text = this.DoBlockQuotes(text);
			text = this.HashHTMLBlocks(text);
			text = this.FormParagraphs(text);
			return text;
		}

		private string RunSpanGamut(string text)
		{
			text = this.DoCodeSpans(text);
			text = this.EscapeSpecialCharsWithinTagAttributes(text);
			text = this.EscapeBackslashes(text);
			text = this.DoImages(text);
			text = this.DoAnchors(text);
			text = this.DoAutoLinks(text);
			text = this.EncodeAmpsAndAngles(text);
			text = this.DoItalicsAndBold(text);
			text = this.DoHardBreaks(text);
			return text;
		}

		private string FormParagraphs(string text)
		{
			string[] array = Markdown._newlinesMultiple.Split(Markdown._newlinesLeadingTrailing.Replace(text, ""));
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].StartsWith("\u001a"))
				{
					array[i] = this._htmlBlocks[array[i]];
				}
				else
				{
					array[i] = Markdown._leadingWhitespace.Replace(this.RunSpanGamut(array[i]), "<p>") + "</p>";
				}
			}
			return string.Join("\n\n", array);
		}

		private void Setup()
		{
			this._urls.Clear();
			this._titles.Clear();
			this._htmlBlocks.Clear();
			this._listLevel = 0;
		}

		private void Cleanup()
		{
			this.Setup();
		}

		private static string GetNestedBracketsPattern()
		{
			if (Markdown._nestedBracketsPattern == null)
			{
				Markdown._nestedBracketsPattern = Markdown.RepeatString("\r\n                    (?>              # Atomic matching\r\n                       [^\\[\\]]+      # Anything other than brackets\r\n                     |\r\n                       \\[\r\n                           ", 6) + Markdown.RepeatString(" \\]\r\n                    )*", 6);
			}
			return Markdown._nestedBracketsPattern;
		}

		private static string GetNestedParensPattern()
		{
			if (Markdown._nestedParensPattern == null)
			{
				Markdown._nestedParensPattern = Markdown.RepeatString("\r\n                    (?>              # Atomic matching\r\n                       [^()\\s]+      # Anything other than parens or whitespace\r\n                     |\r\n                       \\(\r\n                           ", 6) + Markdown.RepeatString(" \\)\r\n                    )*", 6);
			}
			return Markdown._nestedParensPattern;
		}

		private string StripLinkDefinitions(string text)
		{
			return Markdown._linkDef.Replace(text, new MatchEvaluator(this.LinkEvaluator));
		}

		private string LinkEvaluator(Match match)
		{
			string key = match.Groups[1].Value.ToLowerInvariant();
			this._urls[key] = this.EncodeAmpsAndAngles(match.Groups[2].Value);
			if (match.Groups[3] != null && match.Groups[3].Length > 0)
			{
				this._titles[key] = match.Groups[3].Value.Replace("\"", "&quot;");
			}
			return "";
		}

		private static string GetBlockPattern()
		{
			string newValue = "ins|del";
			string newValue2 = "p|div|h[1-6]|blockquote|pre|table|dl|ol|ul|address|script|noscript|form|fieldset|iframe|math";
			string text = "\r\n            (?>\t\t\t\t            # optional tag attributes\r\n              \\s\t\t\t            # starts with whitespace\r\n              (?>\r\n                [^>\"/]+\t            # text outside quotes\r\n              |\r\n                /+(?!>)\t\t            # slash not followed by >\r\n              |\r\n                \"[^\"]*\"\t\t        # text inside double quotes (tolerate >)\r\n              |\r\n                '[^']*'\t                # text inside single quotes (tolerate >)\r\n              )*\r\n            )?\t\r\n            ";
			string text2 = Markdown.RepeatString("\r\n                (?>\r\n                  [^<]+\t\t\t        # content without tag\r\n                |\r\n                  <\\2\t\t\t        # nested opening tag\r\n                    " + text + "       # attributes\r\n                  (?>\r\n                      />\r\n                  |\r\n                      >", 6) + ".*?" + Markdown.RepeatString("\r\n                      </\\2\\s*>\t        # closing nested tag\r\n                  )\r\n                  |\t\t\t\t\r\n                  <(?!/\\2\\s*>           # other tags with a different name\r\n                  )\r\n                )*", 6);
			string newValue3 = text2.Replace("\\2", "\\3");
			string text3 = "\r\n            (?>\r\n                  (?>\r\n                    (?<=\\n)     # Starting after a blank line\r\n                    |           # or\r\n                    \\A\\n?       # the beginning of the doc\r\n                  )\r\n                  (             # save in $1\r\n\r\n                    # Match from `\\n<tag>` to `</tag>\\n`, handling nested tags \r\n                    # in between.\r\n                      \r\n                        [ ]{0,$less_than_tab}\r\n                        <($block_tags_b_re)   # start tag = $2\r\n                        $attr>                # attributes followed by > and \\n\r\n                        $content              # content, support nesting\r\n                        </\\2>                 # the matching end tag\r\n                        [ ]*                  # trailing spaces\r\n                        (?=\\n+|\\Z)            # followed by a newline or end of document\r\n\r\n                  | # Special version for tags of group a.\r\n\r\n                        [ ]{0,$less_than_tab}\r\n                        <($block_tags_a_re)   # start tag = $3\r\n                        $attr>[ ]*\\n          # attributes followed by >\r\n                        $content2             # content, support nesting\r\n                        </\\3>                 # the matching end tag\r\n                        [ ]*                  # trailing spaces\r\n                        (?=\\n+|\\Z)            # followed by a newline or end of document\r\n                      \r\n                  | # Special case just for <hr />. It was easier to make a special \r\n                    # case than to make the other regex more complicated.\r\n                  \r\n                        [ ]{0,$less_than_tab}\r\n                        <(hr)                 # start tag = $2\r\n                        $attr                 # attributes\r\n                        /?>                   # the matching end tag\r\n                        [ ]*\r\n                        (?=\\n{2,}|\\Z)         # followed by a blank line or end of document\r\n                  \r\n                  | # Special case for standalone HTML comments:\r\n                  \r\n                      [ ]{0,$less_than_tab}\r\n                      (?s:\r\n                        <!-- .*? -->\r\n                      )\r\n                      [ ]*\r\n                      (?=\\n{2,}|\\Z)            # followed by a blank line or end of document\r\n                  \r\n                  | # PHP and ASP-style processor instructions (<? and <%)\r\n                  \r\n                      [ ]{0,$less_than_tab}\r\n                      (?s:\r\n                        <([?%])                # $2\r\n                        .*?\r\n                        \\2>\r\n                      )\r\n                      [ ]*\r\n                      (?=\\n{2,}|\\Z)            # followed by a blank line or end of document\r\n                      \r\n                  )\r\n            )";
			text3 = text3.Replace("$less_than_tab", 3.ToString());
			text3 = text3.Replace("$block_tags_b_re", newValue2);
			text3 = text3.Replace("$block_tags_a_re", newValue);
			text3 = text3.Replace("$attr", text);
			text3 = text3.Replace("$content2", newValue3);
			return text3.Replace("$content", text2);
		}

		private string HashHTMLBlocks(string text)
		{
			return Markdown._blocksHtml.Replace(text, new MatchEvaluator(this.HtmlEvaluator));
		}

		private string HtmlEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			string hashKey = Markdown.GetHashKey(value);
			this._htmlBlocks[hashKey] = value;
			return "\n\n" + hashKey + "\n\n";
		}

		private static string GetHashKey(string s)
		{
			return "\u001a" + Math.Abs(s.GetHashCode()).ToString() + "\u001a";
		}

		private List<Markdown.Token> TokenizeHTML(string text)
		{
			int num = 0;
			List<Markdown.Token> list = new List<Markdown.Token>();
			foreach (Match match in Markdown._htmlTokens.Matches(text))
			{
				int index = match.Index;
				if (num < index)
				{
					list.Add(new Markdown.Token(Markdown.TokenType.Text, text.Substring(num, index - num)));
				}
				list.Add(new Markdown.Token(Markdown.TokenType.Tag, match.Value));
				num = index + match.Length;
			}
			if (num < text.Length)
			{
				list.Add(new Markdown.Token(Markdown.TokenType.Text, text.Substring(num, text.Length - num)));
			}
			return list;
		}

		private string DoAnchors(string text)
		{
			text = Markdown._anchorRef.Replace(text, new MatchEvaluator(this.AnchorRefEvaluator));
			text = Markdown._anchorInline.Replace(text, new MatchEvaluator(this.AnchorInlineEvaluator));
			text = Markdown._anchorRefShortcut.Replace(text, new MatchEvaluator(this.AnchorRefShortcutEvaluator));
			return text;
		}

		private string AnchorRefEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			string value2 = match.Groups[2].Value;
			string text = match.Groups[3].Value.ToLowerInvariant();
			if (text == "")
			{
				text = value2.ToLowerInvariant();
			}
			string text3;
			if (this._urls.ContainsKey(text))
			{
				string text2 = this._urls[text];
				text2 = this.EncodeProblemUrlChars(text2);
				text2 = this.EscapeBoldItalic(text2);
				text3 = "<a href=\"" + text2 + "\"";
				if (this._titles.ContainsKey(text))
				{
					string text4 = this._titles[text];
					text4 = this.EscapeBoldItalic(text4);
					text3 = text3 + " title=\"" + text4 + "\"";
				}
				text3 = text3 + ">" + value2 + "</a>";
			}
			else
			{
				text3 = value;
			}
			return text3;
		}

		private string AnchorRefShortcutEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			string value2 = match.Groups[2].Value;
			string key = Regex.Replace(value2.ToLowerInvariant(), "[ ]*\\n[ ]*", " ");
			string text2;
			if (this._urls.ContainsKey(key))
			{
				string text = this._urls[key];
				text = this.EncodeProblemUrlChars(text);
				text = this.EscapeBoldItalic(text);
				text2 = "<a href=\"" + text + "\"";
				if (this._titles.ContainsKey(key))
				{
					string text3 = this._titles[key];
					text3 = this.EscapeBoldItalic(text3);
					text2 = text2 + " title=\"" + text3 + "\"";
				}
				text2 = text2 + ">" + value2 + "</a>";
			}
			else
			{
				text2 = value;
			}
			return text2;
		}

		private string AnchorInlineEvaluator(Match match)
		{
			string value = match.Groups[2].Value;
			string text = match.Groups[3].Value;
			string text2 = match.Groups[6].Value;
			text = this.EncodeProblemUrlChars(text);
			text = this.EscapeBoldItalic(text);
			if (text.StartsWith("<") && text.EndsWith(">"))
			{
				text = text.Substring(1, text.Length - 2);
			}
			string str = string.Format("<a href=\"{0}\"", text);
			if (!string.IsNullOrEmpty(text2))
			{
				text2 = text2.Replace("\"", "&quot;");
				text2 = this.EscapeBoldItalic(text2);
				str += string.Format(" title=\"{0}\"", text2);
			}
			return str + string.Format(">{0}</a>", value);
		}

		private string DoImages(string text)
		{
			text = Markdown._imagesRef.Replace(text, new MatchEvaluator(this.ImageReferenceEvaluator));
			text = Markdown._imagesInline.Replace(text, new MatchEvaluator(this.ImageInlineEvaluator));
			return text;
		}

		private string ImageReferenceEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			string text = match.Groups[2].Value;
			string text2 = match.Groups[3].Value.ToLowerInvariant();
			if (text2 == "")
			{
				text2 = text.ToLowerInvariant();
			}
			text = text.Replace("\"", "&quot;");
			string text4;
			if (this._urls.ContainsKey(text2))
			{
				string text3 = this._urls[text2];
				text3 = this.EncodeProblemUrlChars(text3);
				text3 = this.EscapeBoldItalic(text3);
				text4 = string.Format("<img src=\"{0}\" alt=\"{1}\"", text3, text);
				if (this._titles.ContainsKey(text2))
				{
					string text5 = this._titles[text2];
					text5 = this.EscapeBoldItalic(text5);
					text4 += string.Format(" title=\"{0}\"", text5);
				}
				text4 += this._emptyElementSuffix;
			}
			else
			{
				text4 = value;
			}
			return text4;
		}

		private string ImageInlineEvaluator(Match match)
		{
			string text = match.Groups[2].Value;
			string text2 = match.Groups[3].Value;
			string text3 = match.Groups[6].Value;
			text = text.Replace("\"", "&quot;");
			text3 = text3.Replace("\"", "&quot;");
			if (text2.StartsWith("<") && text2.EndsWith(">"))
			{
				text2 = text2.Substring(1, text2.Length - 2);
			}
			text2 = this.EncodeProblemUrlChars(text2);
			text2 = this.EscapeBoldItalic(text2);
			string str = string.Format("<img src=\"{0}\" alt=\"{1}\"", text2, text);
			if (!string.IsNullOrEmpty(text3))
			{
				text3 = this.EscapeBoldItalic(text3);
				str += string.Format(" title=\"{0}\"", text3);
			}
			return str + this._emptyElementSuffix;
		}

		private string DoHeaders(string text)
		{
			text = Markdown._headerSetext.Replace(text, new MatchEvaluator(this.SetextHeaderEvaluator));
			text = Markdown._headerAtx.Replace(text, new MatchEvaluator(this.AtxHeaderEvaluator));
			return text;
		}

		private string SetextHeaderEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			int num = match.Groups[2].Value.StartsWith("=") ? 1 : 2;
			return string.Format("<h{1}>{0}</h{1}>\n\n", this.RunSpanGamut(value), num);
		}

		private string AtxHeaderEvaluator(Match match)
		{
			string value = match.Groups[2].Value;
			int length = match.Groups[1].Value.Length;
			return string.Format("<h{1}>{0}</h{1}>\n\n", this.RunSpanGamut(value), length);
		}

		private string DoHorizontalRules(string text)
		{
			return Markdown._horizontalRules.Replace(text, "<hr" + this._emptyElementSuffix + "\n");
		}

		private string DoLists(string text)
		{
			if (this._listLevel > 0)
			{
				text = Markdown._listNested.Replace(text, new MatchEvaluator(this.ListEvaluator));
			}
			else
			{
				text = Markdown._listTopLevel.Replace(text, new MatchEvaluator(this.ListEvaluator));
			}
			return text;
		}

		private string ListEvaluator(Match match)
		{
			string text = match.Groups[1].Value;
			string text2 = Regex.IsMatch(match.Groups[3].Value, "[*+-]") ? "ul" : "ol";
			text = Regex.Replace(text, "\\n{2,}", "\n\n\n");
			string arg = this.ProcessListItems(text, (text2 == "ul") ? "[*+-]" : "\\d+[.]");
			return string.Format("<{0}>\n{1}</{0}>\n", text2, arg);
		}

		private string ProcessListItems(string list, string marker)
		{
			this._listLevel++;
			list = Regex.Replace(list, "\\n{2,}\\z", "\n");
			string pattern = string.Format("(\\n)?                      # leading line = $1\r\n                (^[ ]*)                    # leading whitespace = $2\r\n                ({0}) [ ]+                 # list marker = $3\r\n                ((?s:.+?)                  # list item text = $4\r\n                (\\n{{1,2}}))      \r\n                (?= \\n* (\\z | \\2 ({0}) [ ]+))", marker);
			list = Regex.Replace(list, pattern, new MatchEvaluator(this.ListItemEvaluator), RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
			this._listLevel--;
			return list;
		}

		private string ListItemEvaluator(Match match)
		{
			string text = match.Groups[4].Value;
			string value = match.Groups[1].Value;
			if (!string.IsNullOrEmpty(value) || Regex.IsMatch(text, "\\n{2,}"))
			{
				text = this.RunBlockGamut(this.Outdent(text) + "\n");
			}
			else
			{
				text = this.DoLists(this.Outdent(text));
				text = text.TrimEnd(new char[]
				{
					'\n'
				});
				text = this.RunSpanGamut(text);
			}
			return string.Format("<li>{0}</li>\n", text);
		}

		private string DoCodeBlocks(string text)
		{
			text = Markdown._codeBlock.Replace(text, new MatchEvaluator(this.CodeBlockEvaluator));
			return text;
		}

		private string CodeBlockEvaluator(Match match)
		{
			string text = match.Groups[1].Value;
			text = this.EncodeCode(this.Outdent(text));
			text = Markdown._newlinesLeadingTrailing.Replace(text, "");
			return "\n\n<pre><code>" + text + "\n</code></pre>\n\n";
		}

		private string DoCodeSpans(string text)
		{
			return Markdown._codeSpan.Replace(text, new MatchEvaluator(this.CodeSpanEvaluator));
		}

		private string CodeSpanEvaluator(Match match)
		{
			string text = match.Groups[2].Value;
			text = Regex.Replace(text, "^[ ]*", "");
			text = Regex.Replace(text, "[ ]*$", "");
			text = this.EncodeCode(text);
			return "<code>" + text + "</code>";
		}

		private string DoItalicsAndBold(string text)
		{
			if (this._strictBoldItalic)
			{
				text = Markdown._strictBold.Replace(text, "$1<strong>$3</strong>$4");
				text = Markdown._strictItalic.Replace(text, "$1<em>$3</em>$4");
			}
			else
			{
				text = Markdown._bold.Replace(text, "<strong>$2</strong>");
				text = Markdown._italic.Replace(text, "<em>$2</em>");
			}
			return text;
		}

		private string DoHardBreaks(string text)
		{
			if (this._autoNewlines)
			{
				text = Regex.Replace(text, "\\n", string.Format("<br{0}\n", this._emptyElementSuffix));
			}
			else
			{
				text = Regex.Replace(text, " {2,}\\n", string.Format("<br{0}\n", this._emptyElementSuffix));
			}
			return text;
		}

		private string DoBlockQuotes(string text)
		{
			return Markdown._blockquote.Replace(text, new MatchEvaluator(this.BlockQuoteEvaluator));
		}

		private string BlockQuoteEvaluator(Match match)
		{
			string text = match.Groups[1].Value;
			text = Regex.Replace(text, "^[ ]*>[ ]?", "", RegexOptions.Multiline);
			text = Regex.Replace(text, "^[ ]+$", "", RegexOptions.Multiline);
			text = this.RunBlockGamut(text);
			text = Regex.Replace(text, "^", "  ", RegexOptions.Multiline);
			text = Regex.Replace(text, "(\\s*<pre>.+?</pre>)", new MatchEvaluator(this.BlockQuoteEvaluator2), RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			return string.Format("<blockquote>\n{0}\n</blockquote>\n\n", text);
		}

		private string BlockQuoteEvaluator2(Match match)
		{
			return Regex.Replace(match.Groups[1].Value, "^  ", "", RegexOptions.Multiline);
		}

		private string DoAutoLinks(string text)
		{
			if (this._autoHyperlink)
			{
				text = Markdown._autolinkBare.Replace(text, "$1<$2$3>$4");
			}
			text = Regex.Replace(text, "<((https?|ftp):[^'\">\\s]+)>", new MatchEvaluator(this.HyperlinkEvaluator));
			if (this._linkEmails)
			{
				string pattern = "<\r\n                      (?:mailto:)?\r\n                      (\r\n                        [-.\\w]+\r\n                        \\@\r\n                        [-a-z0-9]+(\\.[-a-z0-9]+)*\\.[a-z]+\r\n                      )\r\n                      >";
				text = Regex.Replace(text, pattern, new MatchEvaluator(this.EmailEvaluator), RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
			}
			return text;
		}

		private string HyperlinkEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			return string.Format("<a href=\"{0}\">{0}</a>", value);
		}

		private string EmailEvaluator(Match match)
		{
			string text = this.Unescape(match.Groups[1].Value);
			text = "mailto:" + text;
			text = this.EncodeEmailAddress(text);
			text = string.Format("<a href=\"{0}\">{0}</a>", text);
			return Regex.Replace(text, "\">.+?:", "\">");
		}

		private string Outdent(string block)
		{
			return Markdown._outDent.Replace(block, "");
		}

		private string EncodeEmailAddress(string addr)
		{
			StringBuilder stringBuilder = new StringBuilder(addr.Length * 5);
			Random random = new Random();
			for (int i = 0; i < addr.Length; i++)
			{
				char c = addr[i];
				int num = random.Next(1, 100);
				if ((num > 90 || c == ':') && c != '@')
				{
					stringBuilder.Append(c);
				}
				else if (num < 45)
				{
					stringBuilder.AppendFormat("&#x{0:x};", (int)c);
				}
				else
				{
					stringBuilder.AppendFormat("&#{0};", (int)c);
				}
			}
			return stringBuilder.ToString();
		}

		private string EncodeCode(string code)
		{
			return Markdown._codeEncoder.Replace(code, new MatchEvaluator(this.EncodeCodeEvaluator));
		}

		private string EncodeCodeEvaluator(Match match)
		{
			string value;
			if ((value = match.Value) != null)
			{
				if (value == "&")
				{
					return "&amp;";
				}
				if (value == "<")
				{
					return "&lt;";
				}
				if (value == ">")
				{
					return "&gt;";
				}
			}
			return Markdown._escapeTable[match.Value];
		}

		private string EncodeAmpsAndAngles(string s)
		{
			s = Markdown._amps.Replace(s, "&amp;");
			s = Markdown._angles.Replace(s, "&lt;");
			return s;
		}

		private string EscapeBackslashes(string s)
		{
			return Markdown._backslashEscapes.Replace(s, new MatchEvaluator(this.EscapeBackslashesEvaluator));
		}

		private string EscapeBackslashesEvaluator(Match match)
		{
			return Markdown._backslashEscapeTable[match.Value];
		}

		private string Unescape(string s)
		{
			return Markdown._unescapes.Replace(s, new MatchEvaluator(this.UnescapeEvaluator));
		}

		private string UnescapeEvaluator(Match match)
		{
			return Markdown._invertedEscapeTable[match.Value];
		}

		private string EscapeBoldItalic(string s)
		{
			s = s.Replace("*", Markdown._escapeTable["*"]);
			s = s.Replace("_", Markdown._escapeTable["_"]);
			return s;
		}

		private string EncodeProblemUrlChars(string url)
		{
			if (!this._encodeProblemUrlCharacters)
			{
				return url;
			}
			StringBuilder stringBuilder = new StringBuilder(url.Length);
			for (int i = 0; i < url.Length; i++)
			{
				char c = url[i];
				bool flag = Array.IndexOf<char>(Markdown._problemUrlChars, c) != -1;
				if (flag && c == ':' && i < url.Length - 1)
				{
					flag = (url[i + 1] != '/' && (url[i + 1] < '0' || url[i + 1] > '9'));
				}
				if (flag)
				{
					stringBuilder.Append("%" + string.Format("{0:x}", (byte)c));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		private string EscapeSpecialCharsWithinTagAttributes(string text)
		{
			List<Markdown.Token> list = this.TokenizeHTML(text);
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			foreach (Markdown.Token current in list)
			{
				string text2 = current.Value;
				if (current.Type == Markdown.TokenType.Tag)
				{
					text2 = text2.Replace("\\", Markdown._escapeTable["\\"]);
					text2 = Regex.Replace(text2, "(?<=.)</?code>(?=.)", Markdown._escapeTable["`"]);
					text2 = this.EscapeBoldItalic(text2);
				}
				stringBuilder.Append(text2);
			}
			return stringBuilder.ToString();
		}

		private string Normalize(string text)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			StringBuilder stringBuilder2 = new StringBuilder();
			bool flag = false;
			int i = 0;
			while (i < text.Length)
			{
				char c = text[i];
				switch (c)
				{
				case '\t':
				{
					int num = 4 - stringBuilder2.Length % 4;
					for (int j = 0; j < num; j++)
					{
						stringBuilder2.Append(' ');
					}
					break;
				}
				case '\n':
					if (flag)
					{
						stringBuilder.Append(stringBuilder2);
					}
					stringBuilder.Append('\n');
					stringBuilder2.Length = 0;
					flag = false;
					break;
				case '\v':
				case '\f':
					goto IL_CB;
				case '\r':
					if (i < text.Length - 1 && text[i + 1] != '\n')
					{
						if (flag)
						{
							stringBuilder.Append(stringBuilder2);
						}
						stringBuilder.Append('\n');
						stringBuilder2.Length = 0;
						flag = false;
					}
					break;
				default:
					if (c != '\u001a')
					{
						goto IL_CB;
					}
					break;
				}
				IL_E9:
				i++;
				continue;
				IL_CB:
				if (!flag && text[i] != ' ')
				{
					flag = true;
				}
				stringBuilder2.Append(text[i]);
				goto IL_E9;
			}
			if (flag)
			{
				stringBuilder.Append(stringBuilder2);
			}
			stringBuilder.Append('\n');
			return stringBuilder.Append("\n\n").ToString();
		}

		private static string RepeatString(string text, int count)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length * count);
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}
	}
}
