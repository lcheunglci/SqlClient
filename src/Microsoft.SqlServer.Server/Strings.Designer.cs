﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.SqlServer.Server {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.SqlServer.Server.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} enumeration value, {1}, is invalid..
        /// </summary>
        internal static string ADP_InvalidEnumerationValue {
            get {
                return ResourceManager.GetString("ADP_InvalidEnumerationValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} enumeration value, {1}, is not supported by the {2} method..
        /// </summary>
        internal static string ADP_NotSupportedEnumerationValue {
            get {
                return ResourceManager.GetString("ADP_NotSupportedEnumerationValue", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is an invalid user defined type, reason: {1}..
        /// </summary>
        internal static string SqlUdt_InvalidUdtMessage
        {
            get {
                return ResourceManager.GetString("SqlUdt_InvalidUdtMessage", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to range: 0-8000.
        /// </summary>
        internal static string SQLUDT_MaxByteSizeValue
        {
            get
            {
                return ResourceManager.GetString("SQLUDT_MaxByteSizeValue", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to no UDT attribute.
        /// </summary>
        internal static string SqlUdtReason_NoUdtAttribute
        {
            get {
                return ResourceManager.GetString("SqlUdtReason_NoUdtAttribute", resourceCulture);
            }
        }
    }
}
