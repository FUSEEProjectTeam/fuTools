﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion: 10.0.0.0
//  
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace fuHTMLGen
{
    using System;
    
    
    #line 1 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public partial class WebPage : WebPageBase
    {
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
        public virtual string TransformText()
        {
            this.GenerationEnvironment = null;
            this.Write("<!DOCTYPE html> \r\n<html>\r\n<head>\r\n\t<title>");
            
            #line 5 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_fileNameWOext));
            
            #line default
            #line hidden
            this.Write("</title>\r\n\r\n\t<link rel=\"stylesheet\" type=\"text/css\" href=\"browser.css\">\r\n\t");
            
            #line 8 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
 if (_customCSS != "") { 
            
            #line default
            #line hidden
            this.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"");
            
            #line 9 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_customCSS));
            
            #line default
            #line hidden
            this.Write("\">\r\n\t");
            
            #line 10 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=windows-1252"">
</head>

<body onload=""onLoad()"">
	<script>
		var jsilConfig = {
			libraryRoot: ""./"",
			localStorage: true,
			readOnlyStorage: true,
			contentRoot: ""./"",
			xna: ""3"",

			manifests: [
				""");
            
            #line 24 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_fileName));
            
            #line default
            #line hidden
            this.Write("\",\r\n\t\t\t");
            
            #line 25 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
 if (_customManifest != "") { 
            
            #line default
            #line hidden
            this.Write("\t\"");
            
            #line 26 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_customManifest));
            
            #line default
            #line hidden
            this.Write("\",\r\n\t\t\t");
            
            #line 27 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
 } 
            
            #line default
            #line hidden
            this.Write("],\r\n\t\t};\r\n\t</script>\r\n\r\n\t<script src=\"JSIL.js\" type=\"text/javascript\"></script>\r\n" +
                    "\t<script src=\"");
            
            #line 33 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_fileName));
            
            #line default
            #line hidden
            this.Write(".manifest.js\" type=\"text/javascript\"></script>\r\n\r\n\t<canvas id=\"canvas\"></canvas>\r" +
                    "\n\r\n\t<script type=\"text/javascript\">\r\n\t\tfunction runMain () {\r\n\t\t\t$asm00.");
            
            #line 39 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_fileNameWOext));
            
            #line default
            #line hidden
            
            #line 39 "C:\Users\Fabian\Dropbox\HS Furtwangen\4. Semester\FUSEE\fuHTMLGen\fuHTMLGen\WebPage.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_nameSpace));
            
            #line default
            #line hidden
            this.Write(@".Main([]);
		};

		window.onresize = function(event) {
			document.getElementById(""canvas"").setAttribute('width', window.innerWidth);
			document.getElementById(""canvas"").setAttribute('height', window.innerHeight);
		}

		window.onresize();
	</script>
</body>
</html>");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public class WebPageBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
    }
    #endregion
}
